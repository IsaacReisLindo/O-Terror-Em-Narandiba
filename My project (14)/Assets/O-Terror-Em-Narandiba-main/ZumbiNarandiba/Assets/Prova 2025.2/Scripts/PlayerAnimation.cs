using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;

    public string[] idleDirections = { "N idle", "NE idle", "O idle", "SO idle", "S idle", "SE idle", "L idle", "NE idle" };
    public string[] walkDirections = { "N walk", "NE walk", "O walk", "SO walk", "S walk", "SE walk", "L walk", "NE walk" };
    public string[] attackDirections = { "N attack", "NE attack", "O attack", "SO attack", "S attack", "SE attack", "L attack", "NE attack" };

    private int lastDirection;
    private bool isAttacking = false;
    public bool IsAttacking => isAttacking;

    public Arma arma;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetDirection(Vector2 _direction)
    {
        // não atualiza direção enquanto está atacando
        if (isAttacking) return;

        string[] directionArray;

        if (_direction.magnitude < 0.1f)
        {
            directionArray = idleDirections;
        }
        else
        {
            directionArray = walkDirections;
            lastDirection = DirectionToIndex(_direction);
        }

        animator.Play(directionArray[lastDirection]);
    }

    public void Attack()
    {
        if (isAttacking) return;
        isAttacking = true;

        animator.Play(attackDirections[lastDirection]);

        // Ativa a HitboxArma, na direção atual
        if (arma != null)
        {
            Vector2 dir = IndexToDirection(lastDirection);
            arma.Activate(dir);
        }

        StartCoroutine(AttackRoutine());
    }


    private IEnumerator AttackRoutine()
    {
        float attackDuration = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(attackDuration);
        isAttacking = false;
    }


    private int DirectionToIndex(Vector2 _direction)
    {
        Vector2 norDir = _direction.normalized;

        float step = 360f / 8;
        float offset = step / 2;

        float angle = Vector2.SignedAngle(Vector2.up, norDir);
        angle += offset;
        if (angle < 0) angle += 360;

        float stepCount = angle / step;
        return Mathf.FloorToInt(stepCount);
    }
    private Vector2 IndexToDirection(int index)
    {
        switch (index)
        {
            case 0: return Vector2.up;
            case 1: return new Vector2(-1, 1).normalized;
            case 2: return Vector2.left;
            case 3: return new Vector2(-1, -1).normalized;
            case 4: return Vector2.down;
            case 5: return new Vector2(1, -1).normalized;
            case 6: return Vector2.right;
            case 7: return new Vector2(1, 1).normalized;
            default: return Vector2.down;
        }
    }

}
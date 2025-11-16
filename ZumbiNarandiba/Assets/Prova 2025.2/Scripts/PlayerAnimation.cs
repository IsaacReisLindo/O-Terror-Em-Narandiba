using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;

    public string[] idleDirections = { "N Idle", "NO Idle", "O Idle", "SO Idle", "S Idle", "SE Idle", "L Idle", "NO Idle" };
    public string[] walkDirections = { "N Walk", "NO Walk", "O Walk", "SO Walk", "S Walk", "SE Walk", "L Walk", "NO Walk" };
    public string[] attackDirections = { "N Atack", "NO Atack", "O Atack", "SO Atack", "S Atack", "SE Atack", "L Atack", "NO Atack" };
    public string[] deathDirections = { "N Die", "NO Die", "O Die", "SO Die", "S Die", "SE Die", "L Die", "NO Die" };

    private int lastDirection;
    private bool isAttacking = false;
    public bool IsAttacking => isAttacking;

    private bool isDead = false;
    public bool IsDead => isDead;

    public Hitbox hitbox;

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

        // Ativa a hitbox, na direção atual
        if (hitbox != null)
        {
            Vector2 dir = IndexToDirection(lastDirection);
            hitbox.Activate(dir);
        }

        StartCoroutine(AttackRoutine());
    }


    private IEnumerator AttackRoutine()
    {
        float attackDuration = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(attackDuration);
        isAttacking = false;
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;

        animator.Play(deathDirections[lastDirection]);

        StartCoroutine(FreezeDeathFrame());
    }

    private IEnumerator FreezeDeathFrame()
    {
        float duration = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(duration);

        animator.speed = 0f;
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

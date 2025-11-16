using UnityEngine;
using System.Collections;

public class EnemyAnimation : MonoBehaviour
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

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetDirection(Vector2 direction)
    {
        if (isAttacking) return;

        string[] array;

        if (direction.magnitude < 0.1f)
        {
            array = idleDirections;
        }
        else
        {
            array = walkDirections;
            lastDirection = DirectionToIndex(direction);
        }

        animator.Play(array[lastDirection]);
    }

    public void Attack()
    {
        if (isAttacking) return;

        isAttacking = true;

        animator.Play(attackDirections[lastDirection]);

        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        float time = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(time);
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

    private int DirectionToIndex(Vector2 direction)
    {
        Vector2 n = direction.normalized;

        float step = 360f / 8f;
        float offset = step / 2f;

        float angle = Vector2.SignedAngle(Vector2.up, n);
        angle += offset;

        if (angle < 0)
        {
            angle += 360f;
        }

        return Mathf.FloorToInt(angle / step);
    }
}

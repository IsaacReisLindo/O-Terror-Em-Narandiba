using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public float attackRange = 1f;

    private Transform player;
    private EnemyAnimation anim;

    private void Awake()
    {
        anim = GetComponent<EnemyAnimation>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (anim.IsDead) return;

        Vector2 dir = player.position - transform.position;
        float dist = dir.magnitude;

        // Se estiver perto, ataca
        if (dist <= attackRange)
        {
            anim.Attack();
            anim.SetDirection(dir);
            return;
        }

        // Movimento normal
        Vector2 moveDir = dir.normalized;
        transform.position += (Vector3)moveDir * speed * Time.deltaTime;

        anim.SetDirection(moveDir);
    }

    public void Kill()
    {
        anim.Die();
    }
}

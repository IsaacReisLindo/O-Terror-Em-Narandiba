using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public float attackRange = 1f;

    private Transform player;
    private EnemyAnimation anim;
    public int health = 100;

    private SpriteRenderer sprite;

    public int damage = 10;
    public float attackDelay = 0.3f;

    // NOVO: controla se o inimigo já está atacando
    private bool isAttacking = false;

    private void Awake()
    {
        anim = GetComponent<EnemyAnimation>();
        sprite = GetComponent<SpriteRenderer>();
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

            if (!isAttacking) // só inicia ataque se ainda não estiver atacando
            {
                StartCoroutine(DealDamageToPlayer());
            }

            return;
        }

        // Movimento normal
        Vector2 moveDir = dir.normalized;
        transform.position += (Vector3)moveDir * speed * Time.deltaTime;

        anim.SetDirection(moveDir);
    }

    IEnumerator DealDamageToPlayer()
    {
        isAttacking = true; // marca que está atacando
        yield return new WaitForSeconds(attackDelay);

        if (player != null)
        {
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);

                // CHECA SE O PLAYER MORREU E TOCA A ANIMAÇÃO
                if (playerHealth.currentHealth <= 0)
                {
                    PlayerAnimation playerAnim = player.GetComponent<PlayerAnimation>();
                    if (playerAnim != null)
                    {
                        playerAnim.Die();
                    }
                }
            }
        }

        // Delay para evitar ataque contínuo
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }
    public void Kill()
    {
        anim.Die();
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log("Enemy tomou dano: " + amount);
        StartCoroutine(Damagee());

        if (health <= 0)
        {
            EnemyAnimation enemyAnim = GetComponent<EnemyAnimation>();
            if (enemyAnim != null)
            {
                enemyAnim.Die();
            }
            else
            {
                Debug.LogWarning("EnemyAnimation não encontrado!");
            }
        }
    }

    IEnumerator Damagee()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }
}

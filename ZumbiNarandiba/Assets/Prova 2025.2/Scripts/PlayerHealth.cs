using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    private Rigidbody2D rb;
    private PlayerAnimation playerAnim;
    private PlayerMovement playerMove;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public float hitFlashDuration = 0.1f;

    public Transform HealthBar;
    public GameObject healthBarObject;
    private Vector3 healthBarScale;
    private float healthPercent;

    private bool isDead = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<PlayerAnimation>();
        playerMove = GetComponent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();


    }

    private void Start()
    {
        currentHealth = maxHealth;

        healthBarScale = HealthBar.localScale;
        healthPercent = healthBarScale.x / currentHealth;
    }

    void UpdateHealthBar()
    {
        healthBarScale.x = healthPercent * currentHealth;
        HealthBar.localScale = healthBarScale;
    }

    public void TakeDamage(int dmg)
    {
        if (isDead) return;

        currentHealth -= dmg;
        UpdateHealthBar();

        if (GameManager.instance != null)

            StartCoroutine(FlashRed());

        Debug.Log("Player took " + dmg + " damage. Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(hitFlashDuration);
        spriteRenderer.color = Color.white;
    }

    private void Die()
    {
        isDead = true;

        rb.velocity = Vector2.zero;

        if (playerAnim != null)
            playerAnim.Die();

        if (playerMove != null)
            playerMove.enabled = false;
    }
}
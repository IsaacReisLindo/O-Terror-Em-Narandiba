using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 30;

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log($"{name} levou {amount} de dano! HP: {health}");
        if (health <= 0) Destroy(gameObject);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerAnimation playerAnim;

    private float moveH, moveV;
    [SerializeField] private float moveSpeed = 1.0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<PlayerAnimation>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerAnim.Attack();
            rb.velocity = Vector2.zero;
            return;
        }
    }

    void FixedUpdate()
    {

        if (playerAnim.IsAttacking)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        moveH = Input.GetAxis("Horizontal") * moveSpeed;
        moveV = Input.GetAxis("Vertical") * moveSpeed;
        rb.velocity = new Vector2(moveH, moveV);

        Vector2 direction = new Vector2(moveH, moveV);
        playerAnim.SetDirection(direction);
    }

}
using UnityEngine;
using System.Collections;

public class SBTransform : MonoBehaviour
{
    public int killsToTransform = 23;  // QUANTIDADE DE KILLS NECESSÁRIA

    public RuntimeAnimatorController normalController;
    public RuntimeAnimatorController superController;

    private PlayerAnimation anim;
    private Hitbox hitbox;
    private PlayerMovement movement;

    private bool isTransformed = false;

    void Awake()
    {
        anim = GetComponent<PlayerAnimation>();
        hitbox = GetComponentInChildren<Hitbox>();
        movement = GetComponent<PlayerMovement>();
    }

   
    public void CheckKillCount()
    {
        if (!isTransformed && GameManager.instance.killCount >= killsToTransform)
            StartTransformation();
    }

    private void StartTransformation()
    {
        if (isTransformed) return;
        isTransformed = true;

        hitbox.damage *= 3;

        StartCoroutine(SwapAnimatorNextFrame());

        movement.moveSpeed /= 1.3f;

        transform.localScale *= 1.3f;

        
        GameManager.instance.killCount = 0;

    }

    private IEnumerator SwapAnimatorNextFrame()
    {
        yield return null;
        anim.SwitchAnimatorController(superController);
    }

    private void EndTransformation()
    {
        anim.SwitchAnimatorController(normalController);

        hitbox.damage /= 3;
        movement.moveSpeed *= 1.3f;
        transform.localScale /= 1.3f;

        isTransformed = false;
    }
}

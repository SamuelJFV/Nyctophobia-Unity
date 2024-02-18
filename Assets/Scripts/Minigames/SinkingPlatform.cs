using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkingPlatform : MonoBehaviour
{
    //Components
    [SerializeField] Animator spriteAnimator;
    private Animator colliderAnimator;
    private Collider2D Collider;
    
    private void Start()
    {
        //Prepare Components
        colliderAnimator = GetComponent<Animator>();
        spriteAnimator = GetComponent<Animator>();
        Collider = GetComponent<Collider2D>();
    }

    //Character tag needs to be "Player"
    void OnCollisionEnter2D(Collision2D collision)
    {
        // only runs if touched by the player            
        if (!collision.gameObject.CompareTag("Player")) return;

        //Play Sinking animation, then destroy collider and the plataform
        colliderAnimator.SetTrigger("Sink");
        spriteAnimator.SetTrigger("Sink");
        Destroy(Collider, 1);
        Destroy(gameObject, 1.5f);
    }
}

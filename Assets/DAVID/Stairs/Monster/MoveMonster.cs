using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMonster : MonoBehaviour
{

    [SerializeField] private SpriteRenderer player;
    private SpriteRenderer monster;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        monster = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (monster.flipX != player.flipX) monster.flipX = player.flipX;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        { if (animator.GetBool("Move") == false) animator.SetBool("Move", true); }
        else if (animator.GetBool("Move") == true) animator.SetBool("Move", false);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawController : MonoBehaviour
{
    [SerializeField] Animator darkArmAnimatior;
    public bool diagonalAttack;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            darkArmAnimatior.SetTrigger("Attack");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stair_level : MonoBehaviour
{

    [SerializeField] private GameObject stairs;

    [SerializeField] private GameObject prevMonster;
    [SerializeField] private GameObject nextMonster;

    [SerializeField] private Transform reflection;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // only runs if touched by the player
        if (!collision.gameObject.CompareTag("Player")) return;

        stairs.SetActive(false);

        if (prevMonster != null) prevMonster.SetActive(false);
        if (nextMonster != null) nextMonster.SetActive(true);

        Vector3 newPosition = new Vector3(-reflection.localPosition.x, reflection.localPosition.y, reflection.localPosition.z);
        reflection.localPosition = newPosition;
    }
}
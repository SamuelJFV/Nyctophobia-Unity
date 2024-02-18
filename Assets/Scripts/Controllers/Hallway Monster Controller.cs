using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayMonsterController : MonoBehaviour
{
    [SerializeField] float maxSpeed;
    [SerializeField] float currentSpeed;
    [SerializeField] float smoothTime;
    [SerializeField] Animator monsterAnimator;
    [SerializeField] Collider2D deathCollider;
    float destroyPosition = 22.00f;
    public bool isChasing;

    public void SpawnMonster()
    {
        monsterAnimator.enabled = true;
    }

    void Update()
    {
        if (isChasing)
        {
            Move();
            
            if (GameManager.pausedGame)
            {
                Despawn();
            }
        }
        if (transform.position.x < destroyPosition)
        {
            Despawn();
        }
    }

    public void StartChasing()
    {
        isChasing = true;
        deathCollider.enabled = true;
        monsterAnimator.SetTrigger("Walk");
    }

    void Move()
    {
        float currentVelocity = 0.00f;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, maxSpeed, ref currentVelocity, smoothTime);
        transform.Translate(- currentSpeed * Time.deltaTime, 0, 0);
    }

    void Despawn()
    {
        Destroy(gameObject);
    }
}

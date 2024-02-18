using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class TvMonsterController : MonoBehaviour
{
    public Vector3 playerPosition;
    public bool isHunting;
    public float huntingTime;
    [SerializeField] bool lockY;
    Animator eyeAnimator;
    Collider2D deathTrigger;
    Vector3 initialPosition;
    Vector3 positionOffset = new Vector3(1.03f, 0.86f, 0.00f);

    void Start()
    {
        eyeAnimator = GetComponent<Animator>();
        deathTrigger = GetComponent<Collider2D>();
        initialPosition = transform.position;
    }

    void Update()
    {
        if (isHunting && playerPosition != Vector3.zero)
        {
            HuntPlayer(playerPosition);
        }
    }

    public void HuntPlayer(Vector3 playerPosition)
    {
        transform.position = playerPosition + (initialPosition - playerPosition - positionOffset) * GameManager.Instance.timer / huntingTime + positionOffset;
        
        if (lockY)
        {
            transform.position = new Vector3(transform.position.x, initialPosition.y);
        }

        if (transform.position.x > initialPosition.x)
        {
            transform.position = new Vector2(initialPosition.x, transform.position.y);
        }
    }
    public void OpenEyes(bool value)
    {
        eyeAnimator.SetBool("Open Eyes", value);
        if (deathTrigger != null)
        {
            deathTrigger.enabled = value;
        }
    }
}

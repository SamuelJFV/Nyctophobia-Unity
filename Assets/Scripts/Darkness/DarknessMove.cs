using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarknessMove : MonoBehaviour
{
    [SerializeField] float maxSpeed;
    [SerializeField] float currentSpeed;
    float targetSpeed;
    float currentVelocity;
    float smoothTime = 1.00f;

    void Update()
    {
        if (GameManager.pausedGame)
        {
            targetSpeed = 0.00f;
        }
        else
        {
            targetSpeed = maxSpeed;
        }

        
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref currentVelocity, smoothTime);
        transform.Translate(currentSpeed * Time.deltaTime, 0, 0);
    }
}

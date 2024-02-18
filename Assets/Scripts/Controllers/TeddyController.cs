using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeddyController : MonoBehaviour
{
    [SerializeField] private float speed;
    float destroyPosition = 4.20f;
    void Update()
    {
        transform.Translate(speed * Time.deltaTime, 0, 0);
        if (transform.position.x > destroyPosition)
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [SerializeField] bool playerCanJumpCrouch = true;
    Animator fadeAnimator;
    [SerializeField] SpriteRenderer roomLight;
    [SerializeField] Collider2D doorCollider;

    void Start()
    {
        fadeAnimator = gameObject.GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShowRoom(true);
            CloseDoor();
            if (!playerCanJumpCrouch)
            {
                GameManager.Instance.SetPlayerJumpCrouch(false);
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShowRoom(false);
            if (!playerCanJumpCrouch)
            {
                GameManager.Instance.SetPlayerJumpCrouch(true);
            }
        }
    }

    void ShowRoom(bool isShowing)
    {
        if (!fadeAnimator.enabled)
        {
            fadeAnimator.enabled = true;
        }
        fadeAnimator.SetBool("Hide Room", !isShowing);
    }

    void CloseDoor()
    {
        if (doorCollider == null)
        {
            return;
        }
        doorCollider.enabled = true;
    }

    public void SetLight(float value)
    {
        roomLight.color = new Color(0.00f, 0.00f, 0.00f, 1.00f - value);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraMode
{
    FollowX,
    FollowY,
    Focus,
    Free
}

public class CameraController : MonoBehaviour
{
    public CameraMode cameraMode;
    public Transform room;
    [SerializeField] Transform player;
    [SerializeField] float smoothTime;
    Vector3 offset = Vector3.back;
    Vector3 targetPosition;
    Vector3 focusPosition;
    void LateUpdate()
    {
        switch (cameraMode)
        {
            case CameraMode.FollowX:
                focusPosition = new Vector3(player.position.x, room.position.y);
                break;

            case CameraMode.FollowY:
                focusPosition = new Vector3(room.position.x, player.position.y);
                break;
            
            case CameraMode.Focus:
                focusPosition = room.position;
                break;
            
            default:
                break;
        }
        
        FocusOn(focusPosition);
    }

    void FocusOn(Vector3 position)
    {
        targetPosition = position + offset;

        Vector3 currentVelocity = Vector3.zero;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime * Time.deltaTime);
    }
}

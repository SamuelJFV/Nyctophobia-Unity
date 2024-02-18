using UnityEngine;
using System.Collections;
public class Death : MonoBehaviour
{
    public PlayerDeath type;
    public int time;
    public bool freezeDirection;
    
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Vector2 colliderSize = GetComponent<Collider2D>().bounds.size;
        Vector3 colliderOffset = GetComponent<Collider2D>().offset;
        Gizmos.DrawCube(transform.position + colliderOffset, colliderSize);
    }
}

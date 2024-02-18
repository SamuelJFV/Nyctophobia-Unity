using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class StairMonsterAttack : MonoBehaviour
{

    private Animator animator;

    [SerializeField] private float distance;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private PlayerController playerController;
    [SerializeField] Vector3 position;

    private float currentDistance;


    private bool startSmashing;


    [SerializeField] private float smash;
    [SerializeField] private float aceleration;
    [SerializeField] private Vector2 smashLimit;

    [SerializeField] private float keyBonus;



    [SerializeField] private GameObject WASD;

    private void OnDrawGizmos()
    {

        Vector3 direction = playerTransform.position - position;
        direction.Normalize();

        Vector3 endPoint = position + direction * distance;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(endPoint, position);
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currentDistance = Vector3.Distance(position, playerTransform.position);

        if (currentDistance < distance && !startSmashing)
        {
            startSmashing = true;
            // playerTransform.gameObject.SetActive(false);
            GameManager.Instance.HidePlayer(true);
            GameManager.Instance.FreezePlayer(true, false);
            
            WASD.SetActive(true);
            animator.SetTrigger("Attack");
        }
        if (startSmashing) SmashButton();


    }

    void SmashButton()
    {
        smash -= aceleration * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            smash += keyBonus;
        }

        if (smash < smashLimit.x) 
        {
            animator.SetTrigger("Kill");
            playerController.Die(PlayerDeath.MirrorMonster, 90);
            Destroy(this);
        }

        if (smash > smashLimit.y)
        {
            animator.SetTrigger("Run");
            // playerTransform.gameObject.SetActive(true);
            GameManager.Instance.HidePlayer(false);
            GameManager.Instance.FreezePlayer(false);
            WASD.SetActive(false);
            Destroy(this);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class playerMovement : MonoBehaviour
{
    //TODO FIX ATTACK ANIMATION LOOP.
    Camera viewCam;
    [SerializeField] float moveSpeed = 10;
    public Image healthBar;
    float currentHealth, maxHealth = 100f;

    public Animator animController;

    NavMeshAgent navMesh;

    LayerMask floorMask, enemyMask, itemMask, environmentMask;
    Vector3 playerToMousePos, playerToMouseRot;
    Rigidbody rb;

    bool isWalking, isAttacking;

    Vector3 enemyPos;

    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        navMesh.speed = 10;

        viewCam = Camera.main;
        enemyMask = 8 << LayerMask.GetMask("Enemy");
        floorMask = 9 << LayerMask.GetMask("Walkable");
        itemMask = 10 << LayerMask.GetMask("Item");
        environmentMask = 11 << LayerMask.GetMask("Environment");

        animController = GetComponent<Animator>();

        currentHealth = maxHealth;
        healthBar.fillAmount = currentHealth;

        rb = GetComponent<Rigidbody>();
        moveSpeed = moveSpeed * Time.deltaTime;
    }

    void playerHealth() 
    {
        if (Input.GetMouseButtonDown(1)) // TODO CHANGE THIS TO WORK WITH ENEMY HITTING THE PLAYER
        {
            currentHealth -= 10;
            float calc_health = currentHealth / maxHealth;
            healthBar.fillAmount = calc_health;
        }
        if (Input.GetKeyDown(KeyCode.Space)) // TODO CHANGE THIS TO WORK WITH POTIONS/SPELLS
        {
            currentHealth += 10;
            float calc_health = currentHealth / maxHealth;
            healthBar.fillAmount = calc_health; 
        }
    }

    void RayCastController()
    {
        Ray rayCam = viewCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;

        if(Physics.Raycast(rayCam, out rayHit, Mathf.Infinity))
        {
            playerToMousePos = rayHit.point;
            playerToMousePos.y = 0;
            playerToMouseRot = rayHit.point - transform.position;
            playerToMouseRot.y = 0;
            Quaternion playerRot = Quaternion.LookRotation(playerToMouseRot);
            rb.MoveRotation(playerRot);

            if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
            {
                if (rayHit.transform.gameObject.layer == floorMask)
                {
                    //rb.transform.position = Vector3.MoveTowards(transform.position, rayHit.point, moveSpeed); // OLD MOVEMENT. SMOOTHER THAN CURRENT
                    navMesh.SetDestination(rayHit.point);
                    isWalking = true;
                }
            }
            else
            {
                isWalking = false;
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (rayHit.transform.gameObject.layer == enemyMask)
                {
                    enemyPos = Vector3.MoveTowards(transform.position, rayHit.transform.position, 10);
                    navMesh.SetDestination(enemyPos);
                }
            }
        }
    }
    // Update is called once per frame

    void attackEnemy()
    {
        if (Vector3.Distance(transform.position, enemyPos) <= 4 && !isWalking)
        {
            isAttacking = true;
            Debug.Log("This is working");
        }

        if (isAttacking)
        {
            animController.Play("Attack");
            isAttacking = false;
        }
    }
    void FixedUpdate()
    {
        if (isWalking)
        {
            animController.speed = 3;
            animController.Play("Walking");
        }
        else
        {
            animController.speed = 1;
            animController.Play("Idle");
        }

        RayCastController();
        playerHealth();
        attackEnemy();
    }
}

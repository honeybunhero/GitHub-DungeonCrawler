using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerMovement : MonoBehaviour
{
    Camera viewCam;
    [SerializeField] float moveSpeed = 10;
    public Image healthBar;
    float currentHealth, maxHealth = 100f;

    LayerMask floorMask, enemyMask, itemMask, environmentMask;
    Vector3 playerToMousePos, playerToMouseRot;
    Rigidbody rb;

    void Start()
    {
        viewCam = Camera.main;
        enemyMask = 8 << LayerMask.GetMask("Enemy");
        floorMask = 9 << LayerMask.GetMask("Walkable");
        itemMask = 10 << LayerMask.GetMask("Item");
        environmentMask = 11 << LayerMask.GetMask("Environment");

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

            if(Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
            {
                if(rayHit.transform.gameObject.layer == floorMask)
                {
                    rb.transform.position = Vector3.MoveTowards(transform.position, playerToMousePos, moveSpeed);
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                if(rayHit.transform.gameObject.layer == enemyMask)
                {
                    
                }
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        RayCastController();
        playerHealth();
        Debug.Log("my current health is: " + currentHealth);
    }
}




/*
 * 
 * 
 * 
 * 
 * 
 * 
 *         Ray rayCam = viewCam.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;
        Debug.DrawRay(rayCam.origin, rayCam.direction * distanceToBackground, Color.red);
        if (Physics.Raycast(rayCam, out rayHit, Mathf.Infinity)) 
        {
            playerToMousePos = rayHit.point;
            playerToMouseRot = rayHit.point - transform.position;
            playerToMouseRot.y = 0;
            playerToMousePos.y = 0;
            Quaternion playerRotation = Quaternion.LookRotation(playerToMouseRot);
            //playerRotation.x = 0;
            rb.MoveRotation(playerRotation);
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
            {
                if(rayHit.transform.gameObject.layer == floorMask)
                {
                    Debug.Log(rayHit.point);
                    //playerToMousePos = rayHit.point;
                    rb.transform.position = Vector3.MoveTowards(rb.transform.position, playerToMousePos, moveSpeed);
                }
            }
            if(rayHit.transform.gameObject.layer == floorMask)
            {
                Debug.Log("This is floorMask");
            }
            else if(rayHit.transform.gameObject.layer == itemMask)
            {
                Debug.Log("This is itemMask!");
            }
            else if(rayHit.transform.gameObject.layer == environmentMask)
            {
                Debug.Log("This is the environmentMask!");
            }*/
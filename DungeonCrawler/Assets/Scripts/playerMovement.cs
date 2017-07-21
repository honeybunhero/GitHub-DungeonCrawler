using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    Camera viewCam;
    [SerializeField] float moveSpeed = 10;
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

        rb = GetComponent<Rigidbody>();
        moveSpeed = moveSpeed * Time.deltaTime;
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
                    Debug.Log("Fuck this nigga!");      
                }
            }
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        RayCastController();
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
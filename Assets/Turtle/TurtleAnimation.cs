using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleAnimation : MonoBehaviour
{
    [HideInInspector] public bool canMove = true;

    public bool isMoving = false;
    bool rotatingUpFins = true;

    public float waitTime;

    public float finSpeed;
    public float finRotationExtremes;
    public float footSpeed;
    public float footRotationExtremes;
    float changeCooldown;
    public float changeCooldownStart;

    public GameObject finL;
    public GameObject finR;
    public GameObject footL;
    public GameObject footR;

    public Rigidbody2D rb;
    public Transform turtleTransform;
    public float speed;
    
    public float movingDrag;
    public float stoppingDrag;

    void Update(){
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 displacement = transform.position - mousePos;
        if (canMove){
            float angle = Mathf.Atan2(displacement.y, displacement.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle + 90f);
            changeCooldown -= Time.deltaTime;

            if (Input.GetKey(KeyCode.W)){
                if (changeCooldown <= 0){
                    isMoving = true;

                    changeCooldown = changeCooldownStart;

                    StartCoroutine("moveFinsForward");
                    StopCoroutine("moveFinsBackward");
                }            
            }
            if (Input.GetKey(KeyCode.S)){
                if (changeCooldown <= 0){
                    isMoving = true;

                    changeCooldown = changeCooldownStart;

                    StopCoroutine("moveFinsForward");
                    StartCoroutine("moveFinsBackward");
                }            
            }
        } 
    }
    void FixedUpdate(){
        if (isMoving){
            if (rotatingUpFins){
                float changeL = Mathf.LerpAngle(finL.transform.localEulerAngles.z, -finRotationExtremes + 360, finSpeed);
                finL.transform.localEulerAngles = new Vector3(0, 0, changeL);
                float changeR = Mathf.LerpAngle(finR.transform.localEulerAngles.z, finRotationExtremes, finSpeed);
                finR.transform.localEulerAngles = new Vector3(0, 0, changeR);
                
                float fchangeL = Mathf.LerpAngle(footL.transform.localEulerAngles.z, -footRotationExtremes + 360, footSpeed);
                footL.transform.localEulerAngles = new Vector3(0, 0, fchangeL);
                float fchangeR = Mathf.LerpAngle(footR.transform.localEulerAngles.z, footRotationExtremes, footSpeed);
                footR.transform.localEulerAngles = new Vector3(0, 0, fchangeR);
            }
            if (!rotatingUpFins){
                float changeL = Mathf.LerpAngle(finL.transform.localEulerAngles.z, finRotationExtremes, finSpeed);
                finL.transform.localEulerAngles = new Vector3(0, 0, changeL);
                float changeR = Mathf.LerpAngle(finR.transform.localEulerAngles.z, -finRotationExtremes + 360, finSpeed);
                finR.transform.localEulerAngles = new Vector3(0, 0, changeR);
                
                float fchangeL = Mathf.LerpAngle(footL.transform.localEulerAngles.z, footRotationExtremes, footSpeed);
                footL.transform.localEulerAngles = new Vector3(0, 0, fchangeL);
                float fchangeR = Mathf.LerpAngle(footR.transform.localEulerAngles.z, -footRotationExtremes + 360, footSpeed);
                footR.transform.localEulerAngles = new Vector3(0, 0, fchangeR);
            }
        }
    }
    IEnumerator moveFinsForward(){
        if (!rotatingUpFins){   
            rotatingUpFins = true;

            yield return new WaitForSeconds(waitTime/2f);
        }

        rotatingUpFins = false;
        rb.drag = movingDrag;
        rb.AddForce(turtleTransform.up * speed, ForceMode2D.Impulse);


        yield return new WaitForSeconds(waitTime/4);

        rb.drag = stoppingDrag;

        yield return new WaitForSeconds(waitTime/4);

        isMoving = false;
        
    }
    IEnumerator moveFinsBackward(){
        if (rotatingUpFins){   
            rotatingUpFins = false;

            yield return new WaitForSeconds(waitTime/2);
        }

        rotatingUpFins = true;
        rb.drag = movingDrag;
        rb.AddForce(turtleTransform.up * -(speed/2), ForceMode2D.Impulse);

        yield return new WaitForSeconds(waitTime/4);

        rb.drag = stoppingDrag;

        yield return new WaitForSeconds(waitTime/4);

        isMoving = false;        
    }
}

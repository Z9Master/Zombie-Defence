using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // COmment zitra
    public CharacterController playerController;

    public float PlayerSpeed = 3f;
    private float speed;
    private float SideSpeed;


    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    public float detectorDistance = 10f;

    public GameObject[] targetComplex;

    private GameObject target;

    private bool isLooking = false;

    void Start()
    {
        speed = PlayerSpeed;
        SideSpeed = speed - 0.8f;
        targetComplex = GameObject.FindGameObjectsWithTag("Enemy");
    }
    void Update()
    {
        selectNearTarget();
        PlayerMove();
        LookAtEnemy();
    }

    private void selectNearTarget()
    {
        float lenght;
        float nearest = Mathf.Infinity;
        foreach (GameObject tg in targetComplex)
        {
            lenght = Vector3.Distance(tg.transform.position, transform.position);
            if(lenght < nearest)
            {
                nearest = lenght;
                target = tg;
            }
        }
    }

    private void PlayerMove()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical);
        if (!isLooking)
        {
            if (horizontal == 1 && vertical == 1)
            {
                speed = SideSpeed;
            }
            else if (horizontal == -1 && vertical == -1)
            {
                speed = SideSpeed;
            }
            else if (horizontal == 1 && vertical == -1)
            {
                speed = SideSpeed;
            }
            else if (horizontal == -1 && vertical == 1)
            {
                speed = SideSpeed;
            }
            else
            {
                speed = PlayerSpeed;
            }
        }

        

        if (direction.magnitude >= 0.1f)
        {
            //if(Vector3.Distance(transform.position, target.transform.position) > detectorDistance)
            //{
            //    float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            //    float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            //    transform.rotation = Quaternion.Euler(0f, angle, 0f);
            //}
            //else
            //{
            //    Vector3 direction2 = target.transform.position - transform.position;
            //    float targetAngle2 = Mathf.Atan2(direction2.x, direction2.z) * Mathf.Rad2Deg;
            //    float angle2 = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle2, ref turnSmoothVelocity, turnSmoothTime);
            //    transform.rotation = Quaternion.Euler(0f, angle2, 0f);
            //}

            if (!isLooking)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }

            playerController.Move(direction * speed * Time.deltaTime);
        }
    }

    private void LookAtEnemy()
    {
        if (Input.GetKey("space") && Vector3.Distance(transform.position, target.transform.position) < detectorDistance)
        {
            isLooking = true;
            Vector3 direction2 = target.transform.position - transform.position;
            float targetAngle2 = Mathf.Atan2(direction2.x, direction2.z) * Mathf.Rad2Deg;
            float angle2 = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle2, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle2, 0f);
            speed = 0;
        }
        else
        {
            isLooking = false;
            speed = PlayerSpeed;
        }
    }
}

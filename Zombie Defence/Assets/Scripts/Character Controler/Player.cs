using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Variables
    public CharacterController playerController;

    public float PlayerSpeed = 2.6f;

    // Cache speed variable for correct speed control
    private float speed;
    private float SideSpeed;

    // Variables for smooth charakter rotating
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    // Player have a detector zone, that can detect enemy
    public float detectorRange = 10f;

    // Variable to store all enemies and then, we'll use that to find the nearest enemy
    public GameObject[] targetComplex;

    // The nearest enemy will be stored here
    private GameObject target;

    // Boolen that store if the player is holding space, which is focusing on enemies and shot
    private bool isLooking = false;
    #endregion

    #region Methods
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
        FocusAtEnemy();
        
    }

    // This method will search for the nearest enemy from targetComplex collection
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

    // Moving and rotating the player with WASD and arrow keys
    private void PlayerMove()
    {
        // Getting Axit input which is forexample 1,0 and with this we can identify the direction
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // when the player is moving forexample foward and right at the moment, he is moving faster than only foward due to vector sum logic
        // so we need to fix it with .normized
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // This check that we are moving on any direction
        if (direction.magnitude >= 0.1f)
        {
            if (!isLooking)
            {
                // Atan2 is a method that can get a number from 2 diferent point(x,z) and we can times it with Rad2Deg to get a degree
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }
            playerController.Move(direction * speed * Time.deltaTime);
        }
    }

    // Method that when we are holding space, the player will focus on the nearest enemy, but during this, he can't move
    private void FocusAtEnemy()
    {
        if (Input.GetKey("space") && Vector3.Distance(transform.position, target.transform.position) < detectorRange)
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

    //private void Run()
    //{
    //    if (Input.GetKey(KeyCode.LeftShift) && Vector3.Distance(transform.position, target.transform.position) < detectorRange && !isLooking)
    //    {
    //        speed = 3f;
    //    }
    //    else if (Input.GetKeyUp(KeyCode.LeftShift))
    //    {
    //        speed = PlayerSpeed;
    //    }
    //}
    #endregion

    #region old_codes
    //if(Vector3.Distance(transform.position, target.transform.position) > detectorRange)
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

    // when the player is moving forexample foward and right at the moment, he is moving faster than only foward due to vector sum logic
    // so we need to decreast the speed, when he is moving forexample foward and right and so
    //if (!isLooking)
    //{
    //    if (horizontal == 1 && vertical == 1)
    //    {
    //        speed = SideSpeed;
    //    }
    //    else if (horizontal == -1 && vertical == -1)
    //    {
    //        speed = SideSpeed;
    //    }
    //    else if (horizontal == 1 && vertical == -1)
    //    {
    //        speed = SideSpeed;
    //    }
    //    else if (horizontal == -1 && vertical == 1)
    //    {
    //        speed = SideSpeed;
    //    }
    //    else
    //    {
    //        speed = PlayerSpeed;
    //    }
    //}
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuretControl : MonoBehaviour
{
    #region variable
    // Variable to store all enemies and then, we'll use that to find the nearest enemy
    public GameObject[] targetComplex;

    // The nearest enemy will be stored here
    private GameObject target;

    // Variable for future turet rotating
    public GameObject turetHead;

    // Variables for smooth turet rotating
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    public float turetRange = 10f;
    #endregion

    #region methods
    void Start()
    {
        targetComplex = GameObject.FindGameObjectsWithTag("Enemy");
    }

    void Update()
    {
        selectNearTarget();
    }

    // This method will select the nearest enemy
    private void selectNearTarget()
    {
        float lenght;
        float nearest = Mathf.Infinity;
        foreach (GameObject tg in targetComplex)
        {
            lenght = Vector3.Distance(tg.transform.position, turetHead.transform.position);
            if (lenght < nearest)
            {
                nearest = lenght;
                target = tg;
                TuretRotate();
            }
        }
    }

    // This method will rotate the turet to the nearest enemy
    void TuretRotate()
    {
        if(Vector3.Distance(turetHead.transform.position, target.transform.position) <= turetRange)
        {
            Vector3 direction = target.transform.position - turetHead.transform.position;
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(turetHead.transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            turetHead.transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }
    }
    #endregion
}
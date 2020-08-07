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

    public Transform shotPoint;

    // Variables for smooth turet rotating
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    public float turetRange = 10f;

    public Rigidbody granade;
    #endregion

    #region methods
    void Start()
    {
        targetComplex = GameObject.FindGameObjectsWithTag("Enemy");
    }

    void Update()
    {
        selectNearTarget();
        GranadeShot();
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

    Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time)
    {
        Vector3 distance = target - origin;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0f;

        float Sy = distance.y;
        float Sxz = distanceXZ.magnitude;

        float Vxz = Sxz / time;
        float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        Vector3 result = distanceXZ.normalized;
        result *= Vxz;
        result.y = Vy;

        return result;
    }

    private void GranadeShot()
    {
        Vector3 Vo = CalculateVelocity(target.transform.position, shotPoint.transform.position, 1f);

        if (Input.GetKeyDown(KeyCode.R))
        {
            Rigidbody obj = Instantiate(granade, shotPoint.transform.position, Quaternion.identity);
            obj.velocity = Vo;
        }
        
    }
    #endregion
}

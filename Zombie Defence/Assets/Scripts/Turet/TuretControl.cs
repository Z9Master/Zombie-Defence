using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuretControl : MonoBehaviour
{
    public GameObject[] targetComplex;

    private GameObject target;

    public GameObject turetHead;

    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    public float turetRange = 10f;

    // Start is called before the first frame update
    void Start()
    {
        targetComplex = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        selectNearTarget();
        
    }

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

}
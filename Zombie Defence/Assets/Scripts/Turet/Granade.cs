using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : MonoBehaviour
{
    public GameObject explosionEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals("Ground") || collision.collider.tag.Equals("Enemy"))
        {
            ExplodeGranade();
        }
        
    }

    private void ExplodeGranade()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject, 0.5f);
    }
}

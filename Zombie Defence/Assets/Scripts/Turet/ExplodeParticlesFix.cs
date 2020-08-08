using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeParticlesFix : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 1.4f);
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesSelfDestruct : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, GetComponent<ParticleSystem>().main.startLifetime.constant);
    }

}

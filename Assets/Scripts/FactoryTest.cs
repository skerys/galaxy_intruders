using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryTest : MonoBehaviour
{
    [SerializeField]
    ShipFactory factory;

    void Start()
    {
        factory.Get();
    }
}

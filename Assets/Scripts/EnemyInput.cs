using System;
using UnityEngine;

public class EnemyInput : MonoBehaviour, IShipInput
{
    public float Horizontal{get; set;}
    public float Vertical{get; set;}

    public event Action OnPrimaryFire = delegate{};
    public event Action OnSecondaryFire = delegate{};

    private void Start()
    {
        Horizontal = 1.0f;
        Vertical = 0.0f;
    }

    private void Update()
    {
        if(transform.position.x >= 10.0f || transform.position.x <= -10.0f){
            Horizontal = -Horizontal;
        }
    }
}

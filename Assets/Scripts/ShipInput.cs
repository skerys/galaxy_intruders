using System;
using UnityEngine;

public class ShipInput : MonoBehaviour,  IShipInput
{   
    public float Horizontal{get; set;}
    public float Vertical{get; set;}

    public event Action OnPrimaryFire = delegate{};
    public event Action OnSecondaryFire = delegate{};

    void Update()
    {
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");

        if(Input.GetKeyDown(KeyCode.K)){
            OnPrimaryFire();
        }
        if(Input.GetKeyDown(KeyCode.L)){
            OnSecondaryFire();
        }
    }
}

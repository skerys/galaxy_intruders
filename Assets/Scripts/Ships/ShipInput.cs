using System;
using UnityEngine;

public class ShipInput : MonoBehaviour,  IShipInput
{   
    public float Horizontal{get; set;}
    public float Vertical{get; set;}

    public event Action OnPrimaryFire = delegate{};
 

    [SerializeField] private float shotDelay = 1.0f;
    private float shotTimer = 0.0f;

    public bool canShoot = true;

    public void SetEnabled(bool status)
    {
        enabled = status;
    }

    void Update()
    {
        Horizontal = Input.GetAxis("Horizontal");
        //Vertical = Input.GetAxis("Vertical");
        Vertical = 0;

        if (canShoot)
        {
            if(shotTimer >= shotDelay)
            {
                if(Input.GetKey(KeyCode.Space)){
                    OnPrimaryFire();
                    shotTimer = 0.0f;
                }
            }
            else
            {
                shotTimer += Time.deltaTime;
            }
        }
        
    }

    public void ChangeShotCooldown(float modifier)
    {
        shotDelay *= modifier;
    }
}

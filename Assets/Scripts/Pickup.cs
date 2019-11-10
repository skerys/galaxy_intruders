using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PickupType
{
    IncreasedFireRate,
    IncreasedMoveSpeed,
    AdditionalProjectile
}

public class Pickup : MonoBehaviour
{
    [SerializeField] public PickupType pickupType;
    [SerializeField] public float fallSpeed;

    [SerializeField] public float value;
    [SerializeField] public ProjectileType projectileType;
    private Rigidbody2D rb;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = -transform.up * fallSpeed;
    }

}

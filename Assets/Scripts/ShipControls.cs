using System;
using System.Collections;
using UnityEngine;

public class ShipControls : MonoBehaviour{

    [SerializeField] float moveSpeed;

    IShipInput input;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<IShipInput>();
    }

    private void Update(){
        Vector2 direction = new Vector2(input.Horizontal, input.Vertical);
        if(direction.magnitude > 1) direction = direction.normalized;

        rb.velocity = direction * moveSpeed;

    }

}
using System;
using System.Collections;
using UnityEngine;

public class ShipEngine : MonoBehaviour{

    [SerializeField] float moveSpeed;

    private ShipFactory originFactory;
    public ShipFactory OriginFactory{
        get => originFactory;
        set {
            Debug.Assert(originFactory == null, "Redefined origin factory");
            originFactory = value;
        }
    }

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
using System;
using System.Collections;
using UnityEngine;

public class ShipEngine : MonoBehaviour{

    [SerializeField] float moveSpeed = 5.0f;

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
        input = GetComponent<IShipInput>();
    }

    private void FixedUpdate(){
        Vector2 direction = new Vector2(input.Horizontal, input.Vertical);
        if(direction.magnitude > 1) direction = direction.normalized;

        transform.Translate(direction * moveSpeed * Time.fixedDeltaTime);

    }

}
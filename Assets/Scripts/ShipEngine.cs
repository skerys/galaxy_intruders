using System;
using System.Collections;
using UnityEngine;

public class ShipEngine : MonoBehaviour{

    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] GameObject explosionEffect;
    [SerializeField] int health = 1;
    public ShipType type;

    private ShipFactory originFactory;
    public ShipFactory OriginFactory{
        get => originFactory;
        set {
            Debug.Assert(originFactory == null, "Redefined origin factory");
            originFactory = value;
        }
    }

    IShipInput input;
 
    private void Start()
    {
        input = GetComponent<IShipInput>();
    }

    private void FixedUpdate(){
        Vector2 direction = new Vector2(input.Horizontal, input.Vertical);
        if(direction.magnitude > 1) direction = direction.normalized;

        transform.Translate(direction * moveSpeed * Time.fixedDeltaTime);

    }

    public void ChangeSpeed(float modifier)
    {
        moveSpeed *= modifier;
    }

    public void Kill()
    {
        health--;
        if(health <= 0)
        {
            if(explosionEffect) Instantiate(explosionEffect, transform.position, Quaternion.identity);
            if (originFactory)
            {
                originFactory.Reclaim(this);
            }
            else
            {
                Destroy(this.gameObject);
            }
            SoundManager.Instance.PlayExplosion();
        }
        
    }

    public int GetHealth()
    {
        return health;
    }

}
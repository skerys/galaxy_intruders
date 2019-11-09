using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float destroyTimer = 1.0f;
    [SerializeField] private float projectileSpeed = 20;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * projectileSpeed;
        Destroy(this.gameObject, destroyTimer);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        ShipEngine engine = other.gameObject.GetComponent<ShipEngine>();
        if(engine){
            if(engine.OriginFactory){
                engine.OriginFactory.Reclaim(engine);
            }else{
                Destroy(engine.gameObject);
            }
        }
        Destroy(this.gameObject);
    }

}

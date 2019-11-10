using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : BaseProjectile
{
    [SerializeField] private float destroyTimer = 1.0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ResetVelocity();
        type = ProjectileType.Simple;
        StartCoroutine(ReclaimAfter(destroyTimer));
    }

    private void OnEnable()
    {
        StartCoroutine(ReclaimAfter(destroyTimer));

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
        OriginFactory.Reclaim(this);
    }

}

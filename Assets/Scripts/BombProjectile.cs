using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectile : BaseProjectile
{
    [SerializeField] float destroyTimer = 3.0f;
    [SerializeField] float explosionRadius = 1.0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ResetVelocity();
        type = ProjectileType.Bomb;
    }

    private void DoExplosion()
    {
        var hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach(var hit in hits)
        {
            ShipEngine engine = hit.gameObject.GetComponent<ShipEngine>();
            if (engine)
            {
                if (engine.OriginFactory)
                {
                    engine.OriginFactory.Reclaim(engine);
                }
                else
                {
                    Destroy(engine.gameObject);
                }
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ShipEngine engine = collision.gameObject.GetComponent<ShipEngine>();

        if (engine)
        {
            if (engine.OriginFactory)
            {
                engine.OriginFactory.Reclaim(engine);
            }
            else
            {
                Destroy(engine.gameObject);
            }
            DoExplosion();
        }
        OriginFactory.Reclaim(this);
    }
}

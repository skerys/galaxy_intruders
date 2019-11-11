using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectile : BaseProjectile
{
    [SerializeField] float destroyTimer = 3.0f;
    [SerializeField] float explosionRadius = 1.0f;
    [SerializeField] GameObject bombExplosion;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ResetVelocity();
        type = ProjectileType.Bomb;
    }

    private void OnEnable()
    { 
        SoundManager.Instance.PlayShootBomb();
    }

    private void DoExplosion(Collision2D firstHit)
    {
        SoundManager.Instance.PlayExplosion();
        var hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        Instantiate(bombExplosion, transform.position, Quaternion.identity);
        foreach(var hit in hits)
        {
            if (hit == firstHit.collider) continue;
            ShipEngine engine = hit.gameObject.GetComponent<ShipEngine>();
            if (engine)
            {
                engine.Kill();
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ShipEngine engine = collision.gameObject.GetComponent<ShipEngine>();

        if (engine)
        {
            engine.Kill();
            DoExplosion(collision);
        }
        OriginFactory.Reclaim(this);
    }
}

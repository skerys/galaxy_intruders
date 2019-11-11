using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineProjectile : BaseProjectile
{
    [SerializeField] private float destroyTimer = 1.0f;
    [SerializeField] private float rateModifier = 5.0f;
    [SerializeField] private float amplitudeModifier = 10.0f;

    private float sinTimer;
    private TrailRenderer tr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<TrailRenderer>();
        ResetVelocity();
        type = ProjectileType.Sine;
        StartCoroutine(ReclaimAfter(destroyTimer));
        sinTimer = 0.0f;
    }

    private void OnEnable()
    {
        StartCoroutine(ReclaimAfter(destroyTimer));
        
    }

    private void OnDisable()
    {
        tr.Clear();
    }

    private void Update()
    {
        rb.velocity = new Vector2(amplitudeModifier*Mathf.Cos(sinTimer*rateModifier*projectileSpeed), rb.velocity.y);
        sinTimer += Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        ShipEngine engine = other.gameObject.GetComponent<ShipEngine>();
        if (engine)
        {
            engine.Kill();
        }
        OriginFactory.Reclaim(this);
    }
}

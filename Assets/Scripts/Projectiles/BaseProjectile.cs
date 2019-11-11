using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseProjectile : MonoBehaviour
{
    private ProjectileFactory originFactory;
    public ProjectileFactory OriginFactory
    {
        get => originFactory;
        set
        {
            Debug.Assert(originFactory == null, "Redefined origin factory");
            originFactory = value;
        }
    }

    [SerializeField] protected float projectileSpeed = 20;
    protected Rigidbody2D rb;

    [HideInInspector]
    public ProjectileType type;

    protected IEnumerator ReclaimAfter(float t)
    {
        yield return new WaitForSeconds(t);
        originFactory.Reclaim(this);
    }

    public void ResetVelocity()
    {
        rb.velocity = transform.up * projectileSpeed;
    }

}

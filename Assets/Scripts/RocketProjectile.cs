using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketProjectile : BaseProjectile
{
    [SerializeField] private float destroyTimer = 1.0f;
    [SerializeField] private float rotationSpeed = 5.0f;

    private ShipEngine target;



    [SerializeField] private LayerMask enemyMask;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ResetVelocity();
        type = ProjectileType.HomingRocket;
    }

    private void OnEnable()
    {
        SoundManager.Instance.PlayShootMissile();
    }

    void Update()
    {
        if(!target || !target.gameObject.activeInHierarchy){
            var hit = Physics2D.OverlapCircle(transform.position, 2.0f, enemyMask);
            if(hit){
                target = hit.gameObject.GetComponent<ShipEngine>();
            }
        }else{
            Vector3 vectorToTarget = target.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90.0f;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationSpeed);
            ResetVelocity();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        ShipEngine engine = other.gameObject.GetComponent<ShipEngine>();
        if(engine){
            engine.Kill();
        }
        OriginFactory.Reclaim(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if(target)
        Gizmos.DrawSphere(target.transform.position, 0.5f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketProjectile : MonoBehaviour
{
    [SerializeField] private float destroyTimer = 1.0f;
    [SerializeField] private float projectileSpeed = 20;
    [SerializeField] private float rotationSpeed = 5.0f;

    private Rigidbody2D rb;
    private ShipEngine target;

    private LayerMask layerMask;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * projectileSpeed;
        //Destroy(this.gameObject, destroyTimer);
        layerMask = ~(1 << gameObject.layer);//getting inverse layer of this
    }

    void Update()
    {
        if(!target){
            var hit = Physics2D.OverlapCircle(transform.position, 3.0f, layerMask);
            if(hit){
                target = hit.gameObject.GetComponent<ShipEngine>();
            }
        }else{
            Vector3 vectorToTarget = target.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, Time.deltaTime * rotationSpeed);
            rb.velocity = transform.up * projectileSpeed;
        }
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JetInput : MonoBehaviour, IShipInput
{
    public float Horizontal { get; set; }
    public float Vertical { get; set; }

    public event Action OnPrimaryFire;
    public event Action OnSecondaryFire;

    [SerializeField] float checkRange = 1.0f;
    [SerializeField] LayerMask checkMask = default;

    Vector2 direction;
    Collider2D myCollider;

    [SerializeField] LayerMask hitMask;

    [SerializeField] float minCooldown = 1.0f;
    [SerializeField] float maxCooldown = 8.0f;
    float cooldownTimer;


    private void Start()
    {
        direction = UnityEngine.Random.insideUnitCircle;
        myCollider = GetComponent<Collider2D>();
        cooldownTimer = UnityEngine.Random.Range(minCooldown, maxCooldown);
    }

    private void Update()
    {
        Horizontal = direction.x;
        Vertical = direction.y;

        var hits = Physics2D.OverlapCircleAll(transform.position, checkRange, checkMask);

        Collider2D closestCollider = null;
        float minDistance = Mathf.Infinity;

        foreach(var col in hits)
        {
            if (col == myCollider) continue ;
            float dist = (col.ClosestPoint(transform.position) - (Vector2)transform.position).magnitude;
            if (dist < minDistance){
                closestCollider = col;
                minDistance = dist;
            }
        }

        if (closestCollider)
        {
            Vector2 hitDirection = (closestCollider.ClosestPoint(transform.position) - (Vector2)transform.position);
            Debug.Log(hitDirection);
            if(hitDirection.magnitude <= 0.001f)
            {
                direction = closestCollider.gameObject.transform.position - transform.position;
            }
            else
            {
                direction += 0.1f / hitDirection.magnitude * hitDirection;

            }
            direction = direction.normalized;
        }

        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0.0f)
        {
            var hit = Physics2D.Raycast(transform.position, Vector2.down, 1.0f, hitMask);
            if (!hit)
            {
                Debug.Log("test");
                OnPrimaryFire();
            }
            cooldownTimer = UnityEngine.Random.Range(minCooldown, maxCooldown);
        }

    }


}

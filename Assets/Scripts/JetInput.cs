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

    private void Start()
    {
        direction = UnityEngine.Random.insideUnitCircle;
        myCollider = GetComponent<Collider2D>();
                
    }

    private void Update()
    {
        Horizontal = direction.x;
        Vertical = direction.y;

        var hit = Physics2D.OverlapCircleAll(transform.position, checkRange, checkMask);

        Collider2D closestCollider = null;
        float minDistance = Mathf.Infinity;

        foreach(var col in hit)
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
    }
}

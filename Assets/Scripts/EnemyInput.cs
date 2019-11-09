using System;
using UnityEngine;

public class EnemyInput : MonoBehaviour, IShipInput
{
    public float Horizontal{get; set;}
    public float Vertical{get; set;}

    public event Action OnPrimaryFire = delegate{};
    public event Action OnSecondaryFire = delegate{};

    [SerializeField] LayerMask hitMask;

    [SerializeField] float minCooldown = 1.0f;
    [SerializeField] float maxCooldown = 8.0f;
    float cooldownTimer;

    private void Start()
    {
        cooldownTimer = UnityEngine.Random.Range(minCooldown, maxCooldown);
    }

    private void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if(cooldownTimer <= 0.0f){
            var hit = Physics2D.Raycast(transform.position, Vector2.down, 1.0f, hitMask);
            if(!hit){
                Debug.Log("test");
                OnPrimaryFire();
            }
            cooldownTimer = UnityEngine.Random.Range(minCooldown, maxCooldown);
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, Vector2.down);
    }

}

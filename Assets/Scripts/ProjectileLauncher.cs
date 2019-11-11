using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField] private ProjectileFactory factory;
    [SerializeField] public ProjectileType type;
    [SerializeField] private int projectileLayer;
    [SerializeField] private Vector3 launchOffset;

    [SerializeField] bool childOfEngine = false;
    private IShipInput input;


    private void Awake()
    {
        if (childOfEngine)
        {
            input = GetComponentInParent<IShipInput>();
        }
        else
        {
            input = GetComponent<IShipInput>();
        }
    }

    private void OnEnable() {
        Debug.Log(input);
        input.OnPrimaryFire += ShootProjectile;
    }
    
    private void OnDisable(){
        input.OnPrimaryFire -= ShootProjectile;
    }

    private void ShootProjectile(){
        var projectile = factory.Get(type);
        projectile.transform.rotation = transform.rotation;
        projectile.gameObject.layer = projectileLayer;
        projectile.transform.position = transform.position + launchOffset;
        projectile.ResetVelocity();
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + launchOffset, 0.05f);
    }
}

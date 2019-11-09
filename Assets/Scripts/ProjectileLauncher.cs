using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab = default;
    [SerializeField] private int projectileLayer;
    [SerializeField] private Vector3 launchOffset;

    private IShipInput input;
    private Collider2D thisCollider;

    private void Awake() {
        input = GetComponent<IShipInput>();
        thisCollider = GetComponent<Collider2D>();
    }

    private void OnEnable() {
        input.OnPrimaryFire += ShootProjectile;
    }
    
    private void OnDisable(){
        input.OnPrimaryFire -= ShootProjectile;
    }

    private void ShootProjectile(){
        var projectile = Instantiate(projectilePrefab, transform.position + launchOffset, Quaternion.identity);
        projectile.layer = projectileLayer;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + launchOffset, 0.05f);
    }
}

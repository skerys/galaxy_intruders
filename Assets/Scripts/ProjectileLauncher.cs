using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab = default;

    [SerializeField] private Vector3 launchOffset;

    private IShipInput input;

    private void Awake() {
        input = GetComponent<IShipInput>();
    }

    private void OnEnable() {
        input.OnPrimaryFire += ShootProjectile;
    }
    
    private void OnDisable(){
        input.OnPrimaryFire -= ShootProjectile;
    }

    private void ShootProjectile(){
        Instantiate(projectilePrefab, transform.position + launchOffset, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + launchOffset, 0.05f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHandler : MonoBehaviour
{
    private ShipInput playerInput;
    private ShotSequence shotSequence;
    private ShipEngine engine;

    private void Awake()
    {
        playerInput = GetComponent<ShipInput>();
        shotSequence = GetComponent<ShotSequence>();
        engine = GetComponent<ShipEngine>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Pickup pickup = collision.gameObject.GetComponent<Pickup>();
        if (pickup)
        {
            switch (pickup.pickupType)
            {
                case PickupType.AdditionalProjectile:
                {
                        shotSequence.AddToSequence(pickup.projectileType);
                    break;
                }
                case PickupType.IncreasedFireRate:
                {
                        playerInput.ChangeShotCooldown(pickup.value);
                    break;
                }
                case PickupType.IncreasedMoveSpeed:
                {
                        engine.ChangeSpeed(pickup.value);
                    break;
                }
            }
            Destroy(pickup.gameObject);
        }
    }
}

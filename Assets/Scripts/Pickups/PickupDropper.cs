using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupDropper : MonoBehaviour
{

    [SerializeField] PickupList pickups;
    [SerializeField] float dropChance;
    // Start is called before the first frame update


    // Update is called once per frame
    public void DropPickup()
    {

        if (Random.Range(0.0f, 1.0f) <= dropChance)
        {
            Instantiate(pickups.ChoosePickup(), transform.position, Quaternion.identity);
        }
          
    }
}

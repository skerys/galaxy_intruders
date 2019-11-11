using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PickupWeight
{
    public GameObject pickup;
    public int weight;
}

[CreateAssetMenu]
public class PickupList : ScriptableObject
{
    public List<PickupWeight> pickups;
    private int staticWeightSum;
    private void OnEnable()
    {
        staticWeightSum = 0;
        foreach(var p in pickups)
        {
            staticWeightSum += p.weight;
        }
       
  
    }

    public GameObject ChoosePickup()
    {
        int weightSum = staticWeightSum;
        int index = 0;
        int lastIndex = pickups.Count - 1;
        Debug.Log(weightSum);
        while (index < lastIndex)
        {
            if (Random.Range(0, weightSum) < pickups[index].weight)
            {
                return pickups[index].pickup;
            }
            weightSum -= pickups[index++].weight;
        }
        return pickups[index].pickup;
    }
}

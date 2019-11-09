using System;
using UnityEngine;

public class EnemyInput : MonoBehaviour, IShipInput
{
    public float Horizontal{get; set;}
    public float Vertical{get; set;}

    public event Action OnPrimaryFire = delegate{};
    public event Action OnSecondaryFire = delegate{};

}

using System;

public interface IShipInput{
    float Horizontal{get; set;}
    float Vertical{get; set;}

    event Action OnPrimaryFire;
}
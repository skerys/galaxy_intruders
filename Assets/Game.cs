using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private ShipFactory enemyFactory;

    void Start(){
        GenerateEnemies();
    }

    void GenerateEnemies(){
        for(int i = -6; i <= 6; i+=2){
            for(int j = 0; j <= 3; ++j){
                var ship = enemyFactory.Get();
                ship.transform.position = new Vector3(i, j, 0);
            }
        }
    }

}

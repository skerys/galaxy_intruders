using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private ShipFactory enemyFactory;

    
    private List<List<GameObject>> enemyShips;

    float deltaX;
    float deltaY;

    void Start(){
        enemyShips = new List<List<GameObject>>();
        GenerateEnemies();
        GetComponent<EnemyMover>().enemyShips = enemyShips;
    }

    void GenerateEnemies(){
        for(int i = 0; i <= 3; i++){
            List<GameObject> shipLine = new List<GameObject>();
            for(int j = -6; j <= 6; j+=2){
                var ship = enemyFactory.Get();
                ship.transform.position = new Vector3(j, i, 0);
                shipLine.Add(ship.gameObject);
            }
            enemyShips.Add(shipLine);
        }
    }



}

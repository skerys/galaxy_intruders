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
        CreatePlayer();
        GenerateEnemiesStageTwo();
        GetComponent<EnemyMover>().enemyShips = enemyShips;
    }

    void CreatePlayer()
    {
        var player = enemyFactory.Get(ShipType.Player);
        player.transform.position = new Vector3(0, -4, 0);
    }

    void GenerateEnemiesStageOne(){
        for(int i = 0; i <= 2; i++){
            List<GameObject> shipLine = new List<GameObject>();
            for(int j = -4; j <= 4; j+=2){
                var ship = enemyFactory.Get(ShipType.EnemyStatic);
                ship.transform.position = new Vector3(j, i, 0);
                shipLine.Add(ship.gameObject);
            }
            enemyShips.Add(shipLine);
        }
    }

    void GenerateEnemiesStageTwo()
    {
        for (int i = 0; i <= 2; i++)
        {
            List<GameObject> shipLine = new List<GameObject>();
            for (int j = -4; j <= 4; j += 2)
            {
                var ship = enemyFactory.Get(ShipType.EnemyJet);
                ship.transform.position = new Vector3(j, i, 0);
                shipLine.Add(ship.gameObject);
            }
            enemyShips.Add(shipLine);
            GetComponent<EnemyMover>().enabled = false;
        }
    }

    private void Update()
    {
        foreach(var shipLine in enemyShips)
        {
            foreach(var ship in shipLine)
            {
                if (ship.activeSelf) return;
            }
        }
        enemyShips.Clear();
        GenerateEnemiesStageTwo();
    }



}

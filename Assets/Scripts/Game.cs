using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private ShipFactory enemyFactory;
    [SerializeField] private GameObject bossPrefab;

    
    private List<List<GameObject>> enemyShips;
    private EnemyMover mover;
    private GameObject boss;

    private int currentStage = 1;


    void Start(){
        enemyShips = new List<List<GameObject>>();
        mover = GetComponent<EnemyMover>();
        CreatePlayer();
        InitiateStageOne();
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

    void GenerateBoss()
    {
        Instantiate(bossPrefab);
    }

    void InitiateStageOne()
    {
        GenerateEnemiesStageOne();
        mover.enemyShips = enemyShips;
    }

    void EndStageOne()
    {
        mover.enabled = false;
    }

    void InitiateStageTwo()
    {
        GenerateEnemiesStageTwo();
        currentStage++;
    }

    void InitiateBossStage()
    {
        Instantiate(bossPrefab);
        currentStage++;
    }

    private void Update()
    {
        if(currentStage <= 2)
        { 
            foreach(var shipLine in enemyShips)
            {
                foreach(var ship in shipLine)
                {
                    if (ship.activeSelf) return;
                }
            }
        }
        enemyShips.Clear();
        if (currentStage == 1)
        {
            EndStageOne();
            InitiateStageTwo();
            
        }
        else if(currentStage == 2)
        {
            InitiateBossStage();
            
        }

        if(currentStage == 3)
        {
            if (!boss)
            {
                Debug.Log("GameOver, you win");
            }
        }
    }



}

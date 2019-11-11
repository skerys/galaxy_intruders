using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private ShipFactory enemyFactory;
    [SerializeField] private GameObject bossPrefab;
    [SerializeField] float shipSpawnOffset = 10.0f;
    [SerializeField] GameObject jetBounds;

    
    private List<List<GameObject>> enemyShips;
    private EnemyMover mover;
    private GameObject boss;

    private int currentStage = 1;
    private bool shipsSuspended;

    private float moveDownAmount = 0.0f;

    void Start(){
        enemyShips = new List<List<GameObject>>();
        mover = GetComponent<EnemyMover>();
        CreatePlayer();
        InitiateStageOne();
        SuspendEnemyShips();
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
                ship.transform.position = new Vector3(j, i + shipSpawnOffset, 0);
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
                ship.transform.position = new Vector3(j, i + shipSpawnOffset, 0);
                shipLine.Add(ship.gameObject);
            }
            enemyShips.Add(shipLine);
            GetComponent<EnemyMover>().enabled = false;
        }
    }

    void SuspendEnemyShips()
    {
        if (currentStage == 3) boss.GetComponent<IShipInput>().SetEnabled(false);
        if (currentStage == 1) mover.enabled = false;
        jetBounds.SetActive(false);
        foreach (var shipLine in enemyShips)
        {
            foreach (var ship in shipLine)
            {
                ship.GetComponent<IShipInput>().SetEnabled(false);
            }
        }
        
        shipsSuspended = true;
    }

    void EnableShips()
    {
        if (currentStage == 3) boss.GetComponent<IShipInput>().SetEnabled(true);
        if (currentStage == 1) mover.enabled = true;
        jetBounds.SetActive(true);
        foreach (var shipLine in enemyShips)
        {
            foreach (var ship in shipLine)
            {
                ship.GetComponent<IShipInput>().SetEnabled(true);
            }
        }
        shipsSuspended = false;
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
        boss = Instantiate(bossPrefab, new Vector3(bossPrefab.transform.position.x, bossPrefab.transform.position.y + shipSpawnOffset, 0.0f), bossPrefab.transform.rotation);
        currentStage++;
    }

    private void Update()
    {
        if (shipsSuspended)
        {
            if(currentStage == 3)
            {
                boss.transform.Translate(Time.deltaTime * -boss.transform.up);
            }
            else
            {
                foreach (var shipLine in enemyShips)
                {
                    foreach (var ship in shipLine)
                    {
                        ship.transform.Translate(Time.deltaTime * -ship.transform.up);
                    }
                }

            }
            moveDownAmount += Time.deltaTime;
            if(moveDownAmount >= shipSpawnOffset)
            {
                shipsSuspended = false;
                EnableShips();
                moveDownAmount = 0.0f;
            }
        }
        else
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
                SuspendEnemyShips();
            
            }
            else if(currentStage == 2)
            {
                InitiateBossStage();
                SuspendEnemyShips();
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



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private ShipFactory enemyFactory;
    [SerializeField] private ShipEngine bossPrefab;
    [SerializeField] float shipSpawnOffset = 10.0f;
    [SerializeField] GameObject jetBounds;

    [SerializeField] GameObject pulseEffect;
    [SerializeField] GameObject warningText;
    [SerializeField] GameObject enemiesInboundText;
    [SerializeField] GameObject selfDestructEffect;
    [SerializeField] GameObject selfDestructEffect2;


    private List<List<GameObject>> enemyShips;
    private EnemyMover mover;
    private RetryHandler retryHandler;

    
    private ShipEngine boss;
    private ShipEngine player;

    private int currentStage = 1;
    private bool shipsSuspended;

    private float moveDownAmount = 0.0f;

    void Start(){
        Debug.Log("yeet");
        enemyShips = new List<List<GameObject>>();
        retryHandler = GetComponent<RetryHandler>();
        mover = GetComponent<EnemyMover>();
        CreatePlayer();
        InitiateStageOne();
        SuspendShips();
    }

    void CreatePlayer()
    {
        player = enemyFactory.Get(ShipType.Player);
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

    void SuspendShips()
    {
        enemiesInboundText.SetActive(true);
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
        player.GetComponent<ShipInput>().canShoot = false;
    }

    void EnableShips()
    {
        enemiesInboundText.SetActive(false);
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
        player.GetComponent<ShipInput>().canShoot = true;
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

    void InitiateBossEnding()
    {
        currentStage++;
        pulseEffect.SetActive(true);
        player.GetComponent<ShipInput>().canShoot = false;
        StartCoroutine(EnableAfterTime(warningText, 0.5f));
        boss.enabled = false;
    }

    void InitiateSelfDestructSequence()
    {
        currentStage++;
        boss.enabled = false;
        warningText.SetActive(false);
        player.GetComponent<ShipInput>().enabled = false;
        Instantiate(selfDestructEffect, player.transform.position, Quaternion.identity);
        StartCoroutine(EnableAfterTime(Instantiate(selfDestructEffect2, player.transform.position, Quaternion.identity), 2.0f));
        StartCoroutine(KillBossAfterTime(4.5f));
        StartCoroutine(KillAppAfterTime(10.0f));

    }

    IEnumerator EnableAfterTime(GameObject go, float t)
    {
        yield return new WaitForSeconds(t);
        go.SetActive(true);
    }

    IEnumerator KillBossAfterTime(float t)
    {
        yield return new WaitForSeconds(t);
        boss.Kill();
    }

    IEnumerator KillAppAfterTime(float t)
    {
        yield return new WaitForSeconds(t);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }



    private void Update()
    {
        if (!player.gameObject.activeSelf)
        {
            Debug.Log("player died");
            retryHandler.currentState = GameState.Dead;
        }

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
                SuspendShips();
            
            }
            else if(currentStage == 2)
            {
                InitiateBossStage();
                SuspendShips();
            }

            if(currentStage == 3)
            {
                if(boss.GetHealth()  <= 1.0f)
                {
                    player.enabled = false;
                    InitiateBossEnding();
                }
                if (!boss)
                {
                    Debug.Log("GameOver, you win");
                    retryHandler.currentState = GameState.Win;
                }
            }
            if(currentStage == 4)
            {
                if (Input.GetKey(KeyCode.L))
                {
                    InitiateSelfDestructSequence();
                }
            }
        }

        
    }



}

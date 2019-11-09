using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private ShipFactory enemyFactory;
    [SerializeField] private float moveDelay;
    [SerializeField] private float lineMoveTimerOffset = 0.15f;

    private float moveTimer = 0.0f;
    private float lineOffsetTimer = 0.0f;

    private int currentLineIndex = 0;

    private bool currentLineMoved = false;

    private float speedX = 0.3f;
    public bool moveLeft = true; 
    private bool newMoveLeft = true;

    private List<List<GameObject>> enemyShips;

    void Start(){
        enemyShips = new List<List<GameObject>>();
        GenerateEnemies();
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

    void Update(){
        float deltaX = moveLeft ? -speedX : speedX;
        //Move ship group
        if(moveTimer >= moveDelay){
            if(lineOffsetTimer >= lineMoveTimerOffset){
                currentLineMoved = false;
                currentLineIndex++;
                if(currentLineIndex >= enemyShips.Count){
                    Debug.Log("last line moved");
                    currentLineIndex = 0;
                    moveTimer = 0.0f;
                    moveLeft = newMoveLeft;
                    deltaX = moveLeft ? -speedX : speedX;
                }
                lineOffsetTimer = 0.0f;
            }else{
                lineOffsetTimer += Time.deltaTime;
            }
            if(!currentLineMoved){
                for(int i = enemyShips[currentLineIndex].Count - 1; i >= 0; i--){
                    if(enemyShips[currentLineIndex][i]){
                        enemyShips[currentLineIndex][i].transform.Translate(new Vector3(deltaX, 0, 0), Space.World);
                    }else{
                        enemyShips[currentLineIndex].RemoveAt(i);
                    }
                }
                currentLineMoved = true;
            }
        }else{
            moveTimer += Time.deltaTime;
        }

        //Check if deltaX should change
        bool exitLoop = false;
        foreach(var line in enemyShips){
            foreach(var ship in line){
                if(!ship) continue;
                if(ship.transform.position.x <= -8.0f){
                    newMoveLeft = false;
                    exitLoop = true;
                    break;
                }
                if(ship.transform.position.x >= 8.0f){
                    newMoveLeft = true;
                    exitLoop = true;
                    break;
                }
            }
            if(exitLoop) break;
        }
    }


}

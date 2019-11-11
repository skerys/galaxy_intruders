using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{

    [SerializeField] private float moveDelay;
    [SerializeField] private float lineMoveTimerOffset = 0.15f;

    private float moveTimer = 0.0f;
    private float lineOffsetTimer = 0.0f;

    private int currentLineIndex = 0;

    private bool currentLineMoved = false;

    private float speedX = 0.3f;
    public bool moveLeft = true; 
    private bool newMoveLeft = true;

    
    public List<List<GameObject>> enemyShips;

    float deltaX;
    float deltaY;


    void Start()
    {
        //enemyShips = new List<List<GameObject>>();
        deltaY = 0;
        deltaX = moveLeft ? -speedX : speedX;
    }

    void Update()
    {
           //Move ship group
        if(moveTimer >= moveDelay){
            if(lineOffsetTimer >= lineMoveTimerOffset){
                currentLineMoved = false;
                currentLineIndex++;
                if(currentLineIndex >= enemyShips.Count){
                    currentLineIndex = 0;
                    moveTimer = 0.0f;
                    if(moveLeft!=newMoveLeft){
                        moveLeft = newMoveLeft;
                        deltaX = 0;
                        deltaY = -0.3f;
                    }else{
                        deltaY = 0;
                        deltaX = moveLeft ? -speedX : speedX;
                    }
                }
                lineOffsetTimer = 0.0f;
            }else{
                lineOffsetTimer += Time.deltaTime;
            }
            if(!currentLineMoved){
                for(int i = enemyShips[currentLineIndex].Count - 1; i >= 0; i--){
                    if(enemyShips[currentLineIndex][i].activeSelf){
                        enemyShips[currentLineIndex][i].transform.Translate(new Vector3(deltaX, deltaY, 0), Space.World);
                    }else{
                        enemyShips[currentLineIndex].RemoveAt(i);
                        lineMoveTimerOffset *= 0.9f;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Playing,
    Dead,
    Win
}
public class RetryHandler : MonoBehaviour
{
    [SerializeField] GameObject retryText;
    [SerializeField] ShipFactory shipFactory;
    [SerializeField] ProjectileFactory projectileFactory;

    [HideInInspector] public GameState currentState;

    private void Start()
    {
        currentState = GameState.Playing;
        if(retryText) retryText.SetActive(false);
    }

    private void Update()
    {
        if(currentState == GameState.Dead)
        {
            if (retryText) retryText.SetActive(true);
            if (Input.GetKey(KeyCode.R))
            {
                //restart the current scene
                shipFactory.Unload();
                projectileFactory.Unload();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                
                
            }
            else if (Input.GetKey(KeyCode.Q))
            {
                shipFactory.Unload();
                projectileFactory.Unload();
                Application.Quit();
            }
        }
    }
}

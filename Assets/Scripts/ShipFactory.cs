using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class ShipFactory : ScriptableObject
{
    [SerializeField] ShipEngine enemyShipPrefab;

    Scene factoryScene;

    ShipEngine GetShip(ShipEngine prefab){
        ShipEngine instance = Instantiate(prefab);
        instance.OriginFactory = this;
        MoveToFactoryScene(instance.gameObject);
        return instance;
    }

    void MoveToFactoryScene(GameObject go){
        if(!factoryScene.isLoaded){
            if(Application.isEditor){
                factoryScene = SceneManager.GetSceneByName(name);
                if(!factoryScene.isLoaded){
                    factoryScene = SceneManager.CreateScene(name);
                }
            }
            else{
                factoryScene = SceneManager.CreateScene(name);
            }
        }
        SceneManager.MoveGameObjectToScene(go, factoryScene);
    }

    //TODO: Change Get and Reclaim to use Object Pooling
    public ShipEngine Get()
    {
        return GetShip(enemyShipPrefab);
    }

    public void Reclaim (ShipEngine ship){
        Destroy(ship.gameObject);
    }

}

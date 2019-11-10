using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ShipType
{
    EnemyStatic,
    EnemyJet,
    Player
}

[CreateAssetMenu]
public class ShipFactory : GameObjectFactory<ShipEngine>
{
    [SerializeField] ShipEngine enemyShipPrefab = default;
    [SerializeField] ShipEngine enemyJetPrefab = default;
    [SerializeField] ShipEngine playerPrefab = default;



    

    public void OnEnable()
    {
        prefabs.Clear();
        prefabs.Add(enemyShipPrefab);
        prefabs.Add(enemyJetPrefab);
        prefabs.Add(playerPrefab);

    }


    //TODO: Change Get and Reclaim to use Object Pooling
    public ShipEngine Get(ShipType type)
    {
        if(pools == null)
        {
            CreatePools();
        }

        ShipEngine instance;

        switch (type)
        {
            case ShipType.EnemyStatic: instance = CreateGameObjectInstance((int)ShipType.EnemyStatic); break;
            case ShipType.EnemyJet: instance = CreateGameObjectInstance((int)ShipType.EnemyJet); break;
            case ShipType.Player: instance = CreateGameObjectInstance((int)ShipType.Player); break;
            default:
                Debug.LogError("Ship type " + type + " not found in shipFactory.");
                return null;
        }
        if (!instance.OriginFactory)
            instance.OriginFactory = this;
        return instance;
    }

    public void Reclaim (ShipEngine ship){
        if(pools == null)
        {
            CreatePools();
        }
        pools[(int)ship.type].Add(ship);
        ship.gameObject.SetActive(false);
    }

}

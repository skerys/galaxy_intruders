using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ProjectileType
{
    Simple,
    Sine,
    HomingRocket,
    Bomb,
    None
};

[CreateAssetMenu]
public class ProjectileFactory : GameObjectFactory<BaseProjectile>
{
    [SerializeField] BaseProjectile simpleProjectile;
    [SerializeField] BaseProjectile sineProjectile;
    [SerializeField] BaseProjectile rocketProjectile;
    [SerializeField] BaseProjectile bombProjectile;

    public void OnEnable()
    {
        prefabs.Clear();
        prefabs.Add(simpleProjectile);
        prefabs.Add(sineProjectile);
        prefabs.Add(rocketProjectile);
        prefabs.Add(bombProjectile);
        Debug.Log("prefabs length: " + prefabs.Count);
    }

    protected void CreatePools()
    {
        scene = SceneManager.GetSceneByName(name);
        if (scene.isLoaded)
        {
            GameObject[] rootObjects = scene.GetRootGameObjects();
            for (int i = 0; i < rootObjects.Length; i++)
            {
                BaseProjectile pooledItem = rootObjects[i].GetComponent<BaseProjectile>();
                if (!pooledItem.gameObject.activeSelf)
                {
                    pools[(int)pooledItem.type].Add(pooledItem);
                }
            }
            return;
        }

        pools = new List<BaseProjectile>[prefabs.Count];
        for (int i = 0; i < prefabs.Count; i++)
        {
            pools[i] = new List<BaseProjectile>();
        }
    }


    public BaseProjectile Get(ProjectileType type)
    {
        if(pools == null)
        {
            CreatePools();
        }

        BaseProjectile instance;

        switch (type)
        {
            case ProjectileType.Simple: instance = CreateGameObjectInstance(0); break;
            case ProjectileType.Sine: instance = CreateGameObjectInstance(1); break;
            case ProjectileType.HomingRocket: instance = CreateGameObjectInstance(2); break;
            case ProjectileType.Bomb: instance = CreateGameObjectInstance(3); break;
            default:
                Debug.LogError("Projectile type " + type + " not found in projectileFactory.");
                return null;
        }
        if(!instance.OriginFactory)
            instance.OriginFactory = this;
        return instance;
    }

    public void Reclaim(BaseProjectile proj)
    {
        if(pools == null)
        {
            CreatePools();
        }
        pools[(int)proj.type].Add(proj);
        proj.gameObject.SetActive(false);
        
    }
}

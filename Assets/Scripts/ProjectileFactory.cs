using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ProjectileType
{
    Simple,
    HomingRocket,
    TypeCount
};

[CreateAssetMenu]
public class ProjectileFactory : ScriptableObject
{
    [SerializeField] BaseProjectile simpleProjectile;
    [SerializeField] BaseProjectile rocketProjectile;

    [SerializeField] bool recycle;
    List<BaseProjectile>[] pools;

    Scene factoryScene;

    void CreatePools()
    {
        pools = new List<BaseProjectile>[(int)ProjectileType.TypeCount];
        for(int i = 0; i < (int)ProjectileType.TypeCount; i++)
        {
            pools[i] = new List<BaseProjectile>();
        }
    }

    BaseProjectile GetProjectile(BaseProjectile prefab, int poolIndex)
    {
        BaseProjectile instance;
        if (recycle)
        {
            List<BaseProjectile> pool = pools[poolIndex];
            int lastIndex = pool.Count - 1;
            if(lastIndex >= 0)
            {
                instance = pool[lastIndex];
                pool.RemoveAt(lastIndex);
            }
            else
            {
                instance = Instantiate(prefab);
                instance.OriginFactory = this;
            }
            instance.gameObject.SetActive(true);

        }
        else { 
            instance = Instantiate(prefab);
            instance.OriginFactory = this;
        }
        MoveToFactoryScene(instance.gameObject);
        return instance;
        
    }

    void MoveToFactoryScene(GameObject go)
    {
        if (!factoryScene.isLoaded)
        {
            if (Application.isEditor)
            {
                factoryScene = SceneManager.GetSceneByName(name);
                if (!factoryScene.isLoaded)
                {
                    factoryScene = SceneManager.CreateScene(name);
                }
            }
            else
            {
                factoryScene = SceneManager.CreateScene(name);
            }
        }
        SceneManager.MoveGameObjectToScene(go, factoryScene);
    }

    public BaseProjectile Get(ProjectileType type)
    {
        if (recycle)
        {
            if(pools == null)
            {
                CreatePools();
            }
        }
        switch (type)
        {
            case ProjectileType.Simple: return GetProjectile(simpleProjectile, 0);
            case ProjectileType.HomingRocket: return GetProjectile(rocketProjectile, 1);
            default:
                Debug.LogError("Projectile type " + type + " not found in projectileFactory.");
                return null;
        }
    }

    public void Reclaim(BaseProjectile proj)
    {
        if (recycle)
        {
            if(pools == null)
            {
                CreatePools();
            }
            pools[(int)proj.type].Add(proj);
            proj.gameObject.SetActive(false);
        }
        else
        {
            Destroy(proj.gameObject);
        }
    }
}

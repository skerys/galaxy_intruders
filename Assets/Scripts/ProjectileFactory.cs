using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ProjectileType
{
    Simple,
    Sine,
    HomingRocket
};

[CreateAssetMenu]
public class ProjectileFactory : GameObjectFactory<BaseProjectile>
{
    [SerializeField] BaseProjectile simpleProjectile;
    [SerializeField] BaseProjectile sineProjectile;
    [SerializeField] BaseProjectile rocketProjectile;

    public void OnEnable()
    {
        prefabs.Add(simpleProjectile);
        prefabs.Add(sineProjectile);
        prefabs.Add(rocketProjectile);
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

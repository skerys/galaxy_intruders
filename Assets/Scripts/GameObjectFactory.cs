using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public abstract class GameObjectFactory<T> : ScriptableObject where T : MonoBehaviour
{
    Scene scene;
    protected List<T>[] pools;
    protected List<T> prefabs;

    protected void CreatePools()
    {
        pools = new List<T>[prefabs.Count];
        for(int i = 0; i < prefabs.Count; i++)
        {
            pools[i] = new List<T>();
        }
    }

    protected T CreateGameObjectInstance (int index)
    {
        T instance;
        List<T> pool = pools[index];
        int lastIndex = pool.Count - 1;
        if(lastIndex >= 0)
        {
            instance = pool[lastIndex];
            pool.RemoveAt(lastIndex);
        }
        else
        {
            instance = Instantiate(prefabs[index]);
        }
        instance.gameObject.SetActive(true);

        MoveToFactoryScene(instance.gameObject);
        return instance;
        
    }

    protected void MoveToFactoryScene(GameObject go)
    {
        if (!scene.isLoaded)
        {
            if (Application.isEditor)
            {
                scene = SceneManager.GetSceneByName(name);
                if (!scene.isLoaded)
                {
                    scene = SceneManager.CreateScene(name);
                }
            }
            else
            {
                scene = SceneManager.CreateScene(name);
            }
        }
        SceneManager.MoveGameObjectToScene(go, scene);
    }
}

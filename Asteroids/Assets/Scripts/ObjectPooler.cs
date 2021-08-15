using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public static ObjectPooler Instance;
    public List<Pool> pools;
    private Dictionary<string, Queue<GameObject>> _poolDict;

    private void Awake() => Instance = this;
    
    private void Start()
    {
        _poolDict = new Dictionary<string, Queue<GameObject>>();

        foreach (var pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            
            _poolDict.Add(pool.tag, objectPool);
        }
    }

    public void Spawn(string poolTag, Vector3 position, Quaternion rotation)
    {
        if (!_poolDict.ContainsKey(poolTag))
        {
            Debug.LogWarning("Pool tag " + poolTag + " doesnt exist");
            return;
        }
        
        GameObject spawnObj = _poolDict[poolTag].Dequeue();
        spawnObj.SetActive(true);
        spawnObj.transform.position = position;
        spawnObj.transform.rotation = rotation;

        IPooledObject pooledObject = spawnObj.GetComponent<IPooledObject>();
        pooledObject?.onSpawn();
        
        _poolDict[poolTag].Enqueue(spawnObj);
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;
    private List<GameObject> pooledGameObjects = new List<GameObject>();
    private int amountToPool = 2;
    [SerializeField] private GameObject prefabs;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
  
    void Start()
    {
        for(int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(prefabs);
            obj.SetActive(false);
            pooledGameObjects.Add(obj);
        }
    }
    public GameObject GetPooledGameObject()
    {
        for (int i = 0; i < pooledGameObjects.Count; i++)
        {
            if (!pooledGameObjects[i].activeInHierarchy)
            {
                return pooledGameObjects[i];
            }
        }
        return null;
    }
}

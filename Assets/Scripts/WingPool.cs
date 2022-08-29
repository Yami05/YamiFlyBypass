using System;
using System.Collections.Generic;
using UnityEngine;

public class WingPool : MonoBehaviour
{
    public static WingPool instance;

    [System.Serializable]
    public struct Pool
    {
        public GameObject wingPrefab;
        public int wingPoolSize;
        public Queue<GameObject> wingPool;
    }


    [SerializeField] Pool[] pools;
    private void Awake()
    {

        instance = this;
    }

    private void Start()
    {
        for (int j = 0; j < pools.Length; j++)
        {
            pools[j].wingPool = new Queue<GameObject>();

            for (int i = 0; i < pools[j].wingPoolSize; i++)
            {
                GameObject wing = Instantiate(pools[j].wingPrefab);
                wing.SetActive(false);
                pools[j].wingPool.Enqueue(wing);
            }

        }
    }

    public GameObject GetwingFromPool(int objectType)
    {
        if (pools[objectType].wingPool.Count > 0)
        {
            GameObject wing = pools[objectType].wingPool.Dequeue();
            wing.SetActive(true);
            return wing;
        }
        else
        {
            GameObject wing = Instantiate(pools[objectType].wingPrefab);
            return wing;
        }
    }

    public void ReturnWingToPool(GameObject wing, int objectType)
    {
        pools[objectType].wingPool.Enqueue(wing);
        wing.SetActive(false);
    }

}

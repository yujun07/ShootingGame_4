using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject[] itemFactorys;
    float currentTime;
    public float minTime = 0.1f;
    public float maxTime = 10f;
    public float createTime = 1;
    public int poolSize = 5;
    public List<GameObject> itemObjectPool;
    public Transform[] SpawnPoints;


    void Start()
    {
        createTime = UnityEngine.Random.Range(minTime, maxTime);

        itemObjectPool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            int ranItem = UnityEngine.Random.Range(0, itemFactorys.Length);
            int ranPoint = UnityEngine.Random.Range(0, SpawnPoints.Length);
            GameObject item = Instantiate(itemFactorys[ranItem], SpawnPoints[ranPoint].position, SpawnPoints[ranPoint].rotation);
            itemObjectPool.Add(item);

            item.SetActive(false);
        }
    }
    void SpawnItem()
    {
        if(GameManager.instance.isGameover==false)
        {
            if (itemObjectPool.Count > 0)
            {
                GameObject item = itemObjectPool[0];

                itemObjectPool.Remove(item);

                int index = Random.Range(0, SpawnPoints.Length);

                item.transform.position = SpawnPoints[index].position;

                item.SetActive(true);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > createTime)
        {
            SpawnItem();
            createTime = UnityEngine.Random.Range(minTime, maxTime);
            currentTime = 0;
        }
    }
}

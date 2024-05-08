using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] enemyFactorys;
    public GameObject BossFactory;
    float currentTime;
    public float minTime = 0.5f;
    public float maxTime = 1.5f;
    public float createTime = 1;
    public float BossCreateTime = 100;
    public int poolSize = 50;
    public int BossPoolSize = 1;
    public int BulletPoolSize = 100;
    public int BossBulletPoolSize = 200;

    public List<GameObject> enemyObjectPool;
    public List<GameObject> BossObjectPool;

    public Transform[] SpawnPoints;
    
    public GameObject enemyBulletACreate;
    public GameObject enemyBulletBCreate;
    public GameObject BossBulletACreate;
    public GameObject BossBulletBCreate;
    public GameObject BossBulletCCreate;
    public GameObject BossBulletDCreate;

    public List<GameObject> enemyBulletAObjectPool;
    public List<GameObject> enemyBulletBObjectPool;

    public List<GameObject> BossBulletAObjectPool;
    public List<GameObject> BossBulletBObjectPool;
    public List<GameObject> BossBulletCObjectPool;
    public List<GameObject> BossBulletDObjectPool;

    public bool isBossSpawn;
    void Start()
    {
        BossCreateTime = 100;
        createTime = UnityEngine.Random.Range(minTime, maxTime);

        enemyObjectPool = new List<GameObject>();
        BossObjectPool = new List<GameObject>();
        for (int j=0; j < BossPoolSize; j++)
        {
            Transform BossSpawnPoint = SpawnPoints[0];
            GameObject boss = Instantiate(BossFactory, BossSpawnPoint);
            BossObjectPool.Add(boss);

            boss.SetActive(false);
        }
        
        for (int i = 0; i < poolSize; i++)
        {
            int ranEnemy = UnityEngine.Random.Range(0, enemyFactorys.Length);
            int ranPoint = UnityEngine.Random.Range(0, SpawnPoints.Length);
            GameObject enemy = Instantiate(enemyFactorys[ranEnemy], SpawnPoints[ranPoint].position, SpawnPoints[ranPoint].rotation);
            enemyObjectPool.Add(enemy);

            enemy.SetActive(false);
        }
    }
    void SpawnEnemy()
    {
        if (GameManager.instance.isGameover == false)
        {
            if (enemyObjectPool.Count > 0)
            {
                GameObject enemy = enemyObjectPool[0];

                enemyObjectPool.Remove(enemy);

                int index = Random.Range(0, SpawnPoints.Length);

                enemy.transform.position = SpawnPoints[index].position;

                enemy.SetActive(true);
            }
        }
    }
    void SpawnBoss()
    {
        if (GameManager.instance.isGameover== false)
        {
            if (BossObjectPool.Count > 0)
            {
                GameObject boss = BossObjectPool[0];

                BossObjectPool.Remove(boss);

                boss.SetActive(true);

                isBossSpawn=true;
            }
        } 
    }
    
    void Update()
    {
        currentTime += Time.deltaTime;

        for (BossCreateTime=100; BossCreateTime <= GameManager.instance.Score; BossCreateTime += 100)
        {
            SpawnBoss();
        }
        if (currentTime > createTime && isBossSpawn==false)
        {
            SpawnEnemy();
            createTime = UnityEngine.Random.Range(minTime, maxTime);
            currentTime = 0;
        }
        
    }
}

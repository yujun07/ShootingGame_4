using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float Speed = 5f;
    
    public float Health = 3f;
    Vector3 dir;

    

    public float curShotDelay;
    public float maxShotDelay;
    public string enemyType;
    
    
    public void Move()
    {
        if (GameManager.instance.isGameover == false)
        {
            switch (enemyType)
            {
                case "A":
                    Speed = 3f;
                    break;
                case "B":
                    Speed = 5f;
                    break;
                case "C":
                    Speed = 2f;
                    break;
            }
            transform.position += dir * Speed * Time.deltaTime;
        }
        
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        GameObject GameManagerObject = GameObject.Find("GameManager");
        GameManager gm = GameManagerObject.GetComponent<GameManager>();

        GameObject emObject = GameObject.Find("EnemyManager");
        EnemyManager manager = emObject.GetComponent<EnemyManager>();
        

        if (GameManager.instance.isGameover == false)
        {
            float BulletDamage = GameObject.Find("Player").GetComponent<Player>().BulletDamage;
                        
            if (other.gameObject.tag=="Bullet")
            {
                other.gameObject.SetActive(false);
                Bullet bullet = other.gameObject.GetComponent<Bullet>();
                Player player = GameObject.Find("Player").GetComponent<Player>();
                switch (bullet.Type)
                {
                    case "Bullet":
                        player.bulletObjectPool.Add(other.gameObject);
                        break;
                    case "BulletL":
                        player.bulletLObjectPool.Add(other.gameObject);
                        break;
                    case "BulletR":
                        player.bulletRObjectPool.Add(other.gameObject);
                        break;
                }
                Health -= BulletDamage;
                if (Health <= 0)
                {
                    gameObject.SetActive(false);
                    manager.enemyObjectPool.Add(gameObject);
                    GameManager.instance.Score++;
                }
                
            }
        }
    }
    void OnEnable()
    {
        int randValue = UnityEngine.Random.Range(0, 10);
        switch (enemyType)
        {
            case "A":
                Health = 3;
                break;
            case "B":
                Health = 2;
                break;
            case "C":
                Health = 4;
                break;
        }
        if (randValue < 6)
        {
            GameObject target = GameObject.Find("Player");
            if (target != null)
            {
                dir = target.transform.position - transform.position;
                dir.Normalize();
            }
        }
        else
        {
            dir = Vector3.down;
        }
    }
    void Fire()
    {
        if (GameManager.instance.isGameover == false)
        {
            GameObject emObject = GameObject.Find("EnemyManager");
            EnemyManager manager = emObject.GetComponent<EnemyManager>();
            if (curShotDelay < maxShotDelay)
                return;
            switch (enemyType)
            {
                case "A":
                    if (manager.enemyBulletAObjectPool.Count > 0)
                    {
                        GameObject enemyBulletA = manager.enemyBulletAObjectPool[0];
                        enemyBulletA.SetActive(true);

                        manager.enemyBulletAObjectPool.Remove(enemyBulletA);

                        enemyBulletA.transform.position = transform.position;
                    }
                    break;
                case "C":
                    if (manager.enemyBulletBObjectPool.Count > 0)
                    {
                        GameObject enemyBulletB = manager.enemyBulletBObjectPool[0];
                        enemyBulletB.SetActive(true);

                        manager.enemyBulletBObjectPool.Remove(enemyBulletB);

                        enemyBulletB.transform.position = transform.position + Vector3.left * 0.2f;
                    }
                    if (manager.enemyBulletBObjectPool.Count > 0)
                    {
                        GameObject enemyBulletB = manager.enemyBulletBObjectPool[0];
                        enemyBulletB.SetActive(true);
                        manager.enemyBulletBObjectPool.Remove(enemyBulletB);

                        enemyBulletB.transform.position = transform.position + Vector3.right * 0.2f;
                    }
                    break;
            }
            curShotDelay = 0;
        }
        
    }
    void Reload()
    {
        if (GameManager.instance.isGameover==false)
        {
            curShotDelay += Time.deltaTime;
        }
        
    }


    void Start()
    {
        GameObject emObject = GameObject.Find("EnemyManager");
        EnemyManager manager = emObject.GetComponent<EnemyManager>();
        manager.enemyBulletAObjectPool = new List<GameObject>();
        manager.enemyBulletBObjectPool = new List<GameObject>();
       
        for (int i = 0; i < manager.BulletPoolSize; i++)
        {
            
            GameObject enemyBulletA = Instantiate(manager.enemyBulletACreate);
            GameObject enemyBulletB = Instantiate(manager.enemyBulletBCreate);
            

            manager.enemyBulletAObjectPool.Add(enemyBulletA);
            manager.enemyBulletBObjectPool.Add(enemyBulletB);

            enemyBulletA.SetActive(false);
            enemyBulletB.SetActive(false);
        }
    }
    void Update()
    {
        Move();
        Fire();
        Reload();
    }

    
}

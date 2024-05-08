using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Player : MonoBehaviour
{

    
    [SerializeField] float Speed = 4f;

    public GameObject bulletCreate;
    public GameObject bulletLCreate;
    public GameObject bulletRCreate;

    

    public int poolSize = 200;

    public int power=1;

    public int maxPower=3;
    public List<GameObject> bulletObjectPool;
    public List<GameObject> bulletLObjectPool;
    public List<GameObject> bulletRObjectPool;
    public float curShotDelay;
    public float maxShotDelay;
    public float BulletDamage = 1.0f;
    public void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        viewPos.x=Mathf.Clamp01(viewPos.x);
        viewPos.y = Mathf.Clamp01(viewPos.y);
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos);
        transform.position = worldPos;

        Vector3 dir = new Vector3(h, v, 0).normalized;
        transform.position += dir * Speed * Time.deltaTime;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject emObject = GameObject.Find("EnemyManager");
        EnemyManager manager = emObject.GetComponent<EnemyManager>();
        GameObject itmObject = GameObject.Find("ItemManager");
        ItemManager itmManager = itmObject.GetComponent<ItemManager>();
        
        if (other.gameObject.tag=="Enemy")
        {
            
            GameManager.instance.Health--;
            other.gameObject.SetActive(false);
            manager.enemyObjectPool.Add(other.gameObject);
            GameManager.instance.Score++;
        }
        if (other.gameObject.tag == "EnemyBullet")
        {
            Bullet bullet = other.gameObject.GetComponent<Bullet>();

            GameManager.instance.Health--;
            
            switch (bullet.Type)
            {
                case "EnemyA":
                    manager.enemyBulletAObjectPool.Add(other.gameObject);
                    other.gameObject.SetActive(false);
                    break;
                case "EnemyB":
                    manager.enemyBulletBObjectPool.Add(other.gameObject);
                    other.gameObject.SetActive(false);
                    break;
            }
        }
        if (other.gameObject.tag == "Item")
        {
            Item item = other.gameObject.GetComponent<Item>();
            switch (item.type)
            {
                case "Heart":
                    GameManager.instance.Health++;
                    break;
                case "Power":
                    if (power == maxPower)
                    {
                        GameManager.instance.Score += 5; 
                    }
                    else
                    {
                        power ++;
                    }
                    break;
            }           
            other.gameObject.SetActive(false);
            itmManager.itemObjectPool.Add(other.gameObject);
        }
        else if (other.gameObject.name.Contains("BossBulletA"))
        {
            GameManager.instance.Health--;
            manager.BossBulletAObjectPool.Add(other.gameObject);
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.name.Contains("BossBulletB"))
        {
            GameManager.instance.Health--;
            manager.BossBulletBObjectPool.Add(other.gameObject);
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.name.Contains("BossBulletC"))
        {
            GameManager.instance.Health--;
            manager.BossBulletCObjectPool.Add(other.gameObject);
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.name.Contains("BossBulletD"))
        {
            GameManager.instance.Health--;
            manager.BossBulletDObjectPool.Add(other.gameObject);
            other.gameObject.SetActive(false);
        }
    }
    public void Fire()
    {
        if (Input.GetButton("Fire1"))
        {
            if (curShotDelay < maxShotDelay)
                return;
            switch (power)
            {
                case 1:
                    if (bulletObjectPool.Count > 0)
                    {
                        GameObject bullet = bulletObjectPool[0];
                        bullet.SetActive(true);

                        bulletObjectPool.Remove(bullet);

                        bullet.transform.position = transform.position;
                    }
                    break;
                case 2:
                    if (bulletLObjectPool.Count > 0)
                    {                   
                        GameObject bulletL = bulletLObjectPool[0];
                        bulletL.SetActive(true);

                        bulletLObjectPool.Remove(bulletL);

                        bulletL.transform.position = transform.position + Vector3.left * 0.2f;
                    }
                    if (bulletRObjectPool.Count > 0)
                    {
                        GameObject bulletR = bulletRObjectPool[0];
                        bulletR.SetActive(true);
                        bulletRObjectPool.Remove(bulletR);

                        bulletR.transform.position = transform.position + Vector3.right * 0.2f;
                    }
                    break;
                case 3:
                    if (bulletLObjectPool.Count > 0)
                    {
                        GameObject bulletL = bulletLObjectPool[0];
                        bulletL.SetActive(true);

                        bulletLObjectPool.Remove(bulletL);

                        bulletL.transform.position = transform.position + Vector3.left * 0.2f;
                    }
                    if (bulletRObjectPool.Count > 0)
                    {
                        GameObject bulletR = bulletRObjectPool[0];
                        bulletR.SetActive(true);
                        bulletRObjectPool.Remove(bulletR);

                        bulletR.transform.position = transform.position + Vector3.right * 0.2f;
                    }
                    if (bulletObjectPool.Count > 0)
                    {
                        GameObject bullet = bulletObjectPool[0];
                        bullet.SetActive(true);

                        bulletObjectPool.Remove(bullet);

                        bullet.transform.position = transform.position;
                    }
                    break;
            }
            curShotDelay = 0;
        }
    }
    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }
    void Start()
    {
        
        bulletObjectPool = new List<GameObject>();
        bulletLObjectPool = new List<GameObject>();
        bulletRObjectPool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject bullet = Instantiate(bulletCreate);
            GameObject bulletL = Instantiate(bulletLCreate);
            GameObject bulletR = Instantiate(bulletRCreate);

            bulletObjectPool.Add(bullet);
            bulletLObjectPool.Add(bulletL);
            bulletRObjectPool.Add(bulletR);

            bullet.SetActive(false);
            bulletL.SetActive(false);
            bulletR.SetActive(false);
        }
    }

    void Update()
    {
        Move();
        Fire();
        Reload();
    }
}

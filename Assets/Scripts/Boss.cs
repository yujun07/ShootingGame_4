using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
using UnityEngine;

public class Boss : MonoBehaviour
{

    
    public float Health = 100f;

    public int patternIndex;
    public int CurPatternCount;
    public int[] MaxPatternCount;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject GameManagerObject = GameObject.Find("GameManager");
        GameManager gm = GameManagerObject.GetComponent<GameManager>();

        GameObject emObject = GameObject.Find("EnemyManager");
        EnemyManager manager = emObject.GetComponent<EnemyManager>();
        
        

        if (GameManager.instance.isGameover == false)
        {
            float BulletDamage = GameObject.Find("Player").GetComponent<Player>().BulletDamage;
            if (other.gameObject.tag == "Bullet")
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
                    manager.BossObjectPool.Add(gameObject);
                    GameManager.instance.Score+=30;
                    manager.isBossSpawn = false;
                }

            }
        }
    }
    void Stop()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector3.zero;

        Invoke("Think", 1.5f);
    }
    void Think()
    {
        patternIndex =  patternIndex == 3 ? 0 : patternIndex + 1;
        CurPatternCount = 0;
        switch (patternIndex)
        {
            case 0:
                Firefoward();
                break;
            case 1:
                FireShot();
                break;
            case 2:
                FireShotSecond();
                break;
            case 3:
                FireAround();
                break;
        }
    }
    void Firefoward()
    {
        
        if (GameManager.instance.isGameover == false)
        {
            GameObject emObject = GameObject.Find("EnemyManager");
            EnemyManager manager = emObject.GetComponent<EnemyManager>();
            if (manager.BossBulletBObjectPool.Count > 0)
            {
                GameObject Bullet1 = manager.BossBulletBObjectPool[0];
                Bullet1.SetActive(true);
                Rigidbody2D rigid = Bullet1.GetComponent<Rigidbody2D>();
                rigid.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
                manager.BossBulletBObjectPool.Remove(Bullet1);

                Bullet1.transform.position = transform.position + Vector3.left * 2f;
                GameObject Bullet2 = manager.BossBulletBObjectPool[0];
                Bullet2.SetActive(true);
                Rigidbody2D rigid2 = Bullet2.GetComponent<Rigidbody2D>();
                rigid2.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
                manager.BossBulletBObjectPool.Remove(Bullet2);

                Bullet2.transform.position = transform.position + Vector3.left * 1f;

                GameObject Bullet3 = manager.BossBulletBObjectPool[0];
                Bullet3.SetActive(true);
                Rigidbody2D rigid3 = Bullet3.GetComponent<Rigidbody2D>();
                rigid3.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
                manager.BossBulletBObjectPool.Remove(Bullet3);

                Bullet3.transform.position = transform.position + Vector3.right * 1f;

                GameObject Bullet4 = manager.BossBulletBObjectPool[0];
                Bullet4.SetActive(true);
                Rigidbody2D rigid4 = Bullet4.GetComponent<Rigidbody2D>();
                rigid4.AddForce(Vector2.down * 8, ForceMode2D.Impulse);
                manager.BossBulletBObjectPool.Remove(Bullet4);

                Bullet4.transform.position = transform.position + Vector3.right * 2f;
            }
            CurPatternCount++;
            if (CurPatternCount < MaxPatternCount[patternIndex])
            {
                Invoke("Firefoward", 2);
            }
            else
            {
                Invoke("Think", 3);
            }
        }
    }
    void FireShot()
    {
       
        if (GameManager.instance.isGameover == false)
        {
            GameObject player = GameObject.Find("Player");
            GameObject emObject = GameObject.Find("EnemyManager");
            EnemyManager manager = emObject.GetComponent<EnemyManager>();

            for (int i = 0; i < 5; i++)
            {
                if (manager.BossBulletCObjectPool.Count > 0)
                {
                    GameObject bullet = manager.BossBulletCObjectPool[0];
                    bullet.SetActive(true);
                    bullet.transform.position = transform.position;
                    manager.BossBulletCObjectPool.Remove(bullet);
                    Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                    Vector2 dirVec = player.transform.position - transform.position;
                    Vector2 ranVec = new Vector2(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(0f, 2f));
                    dirVec += ranVec;
                    rigid.AddForce(dirVec.normalized * 4, ForceMode2D.Impulse);
                }
            }
            CurPatternCount++;
            if (CurPatternCount < MaxPatternCount[patternIndex])
            {
                Invoke("FireShot", 3.5f);
            }
            else
            {
                Invoke("Think", 3);
            }
        }
    }
    void FireShotSecond()
    {
        
        if (GameManager.instance.isGameover == false)
        {
            GameObject emObject = GameObject.Find("EnemyManager");
            EnemyManager manager = emObject.GetComponent<EnemyManager>();
            if (manager.BossBulletDObjectPool.Count > 0)
            {
                GameObject bullet = manager.BossBulletDObjectPool[0];
                bullet.SetActive(true);
                manager.BossBulletDObjectPool.Remove(bullet);
                bullet.transform.position = transform.position;
                bullet.transform.rotation = Quaternion.identity;

                Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * CurPatternCount / MaxPatternCount[patternIndex]), -1);
                rigid.AddForce(dirVec.normalized * 5, ForceMode2D.Impulse);
            }
            CurPatternCount++;
            if (CurPatternCount < MaxPatternCount[patternIndex])
            {
                Invoke("FireShotSecond", 0.15f);
            }
            else
            {
                Invoke("Think", 3);
            }
        }
    }
    void FireAround()
    {
        
        if (GameManager.instance.isGameover == false)
        {
            int roundUNuA = 50;
            int roundUNuB = 40;
            int roundNumC = CurPatternCount % 2 == 0 ? roundUNuA : roundUNuB;

            for (int i = 0; i < roundNumC; i++)
            {
                GameObject emObject = GameObject.Find("EnemyManager");
                EnemyManager manager = emObject.GetComponent<EnemyManager>();
                if (manager.BossBulletAObjectPool.Count > 0)
                {
                    GameObject bullet = manager.BossBulletAObjectPool[0];
                    bullet.SetActive(true);
                    manager.BossBulletAObjectPool.Remove(bullet);
                    bullet.transform.position = transform.position;
                    bullet.transform.rotation = Quaternion.identity;

                    Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
                    Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / roundNumC*-1), Mathf.Sin(Mathf.PI * 2 * i / roundNumC*1));

                    rigid.AddForce(dirVec.normalized * 2, ForceMode2D.Impulse);
                    Vector3 rotVec = Vector3.forward * 360 * i / roundNumC + Vector3.forward * 90;
                    bullet.transform.Rotate(rotVec);
                }       
            }
            CurPatternCount++;
            if (CurPatternCount < MaxPatternCount[patternIndex])
            {
                Invoke("FireAround", 0.7f);
            }
            else
            {
                Invoke("Think", 3);
            }
        }
    }
    void Start()
    {
        GameObject emObject = GameObject.Find("EnemyManager");
        EnemyManager manager = emObject.GetComponent<EnemyManager>();
        manager.BossBulletAObjectPool = new List<GameObject>();
        manager.BossBulletBObjectPool = new List<GameObject>();
        manager.BossBulletCObjectPool = new List<GameObject>();
        manager.BossBulletDObjectPool = new List<GameObject>();
        for (int i = 0; i < manager.BulletPoolSize; i++)
        {

            GameObject BossBulletA = Instantiate(manager.BossBulletACreate);
            GameObject BossBulletB = Instantiate(manager.BossBulletBCreate);
            GameObject BossBulletC = Instantiate(manager.BossBulletCCreate);
            GameObject BossBulletD = Instantiate(manager.BossBulletDCreate);


            manager.BossBulletAObjectPool.Add(BossBulletA);
            manager.BossBulletBObjectPool.Add(BossBulletB);
            manager.BossBulletCObjectPool.Add(BossBulletC);
            manager.BossBulletDObjectPool.Add(BossBulletD);

            BossBulletA.SetActive(false);
            BossBulletB.SetActive(false);
            BossBulletC.SetActive(false);
            BossBulletD.SetActive(false);
        }
    }
    void OnEnable()
    {
        Invoke("Stop", 2);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
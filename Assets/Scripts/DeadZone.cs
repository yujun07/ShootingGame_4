using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D other)
    {
        if (GameManager.instance.isGameover == false)
        {
            if (other.gameObject.name.Contains("Bullet") || other.gameObject.tag=="Enemy" || other.gameObject.tag=="Item"|| other.gameObject.tag == "EnemyBullet"||other.gameObject.tag=="BossBullet")
            {
                GameObject emObject = GameObject.Find("EnemyManager");
                EnemyManager manager = emObject.GetComponent<EnemyManager>();
                if (other.gameObject.tag == "Bullet")
                {
                    Bullet bullet = other.gameObject.GetComponent<Bullet>();
                    Player player = GameObject.Find("Player").GetComponent<Player>();
                    
                    switch (bullet.Type)
                    {
                        case "Bullet":
                            player.bulletObjectPool.Add(other.gameObject);
                            other.gameObject.SetActive(false);
                            break;
                        case "BulletL":
                            player.bulletLObjectPool.Add(other.gameObject);
                            other.gameObject.SetActive(false);
                            break;
                        case "BulletR":
                            player.bulletRObjectPool.Add(other.gameObject);
                            other.gameObject.SetActive(false);
                            break;
                    }
                }
                else if (other.gameObject.name.Contains("BossBulletA"))
                {                  
                    manager.BossBulletAObjectPool.Add(other.gameObject);
                    other.gameObject.SetActive(false);
                }
                else if (other.gameObject.name.Contains("BossBulletB"))
                {
                    manager.BossBulletBObjectPool.Add(other.gameObject);
                    other.gameObject.SetActive(false);
                }
                else if (other.gameObject.name.Contains("BossBulletC"))
                {
                    manager.BossBulletCObjectPool.Add(other.gameObject);
                    other.gameObject.SetActive(false);
                }
                else if (other.gameObject.name.Contains("BossBulletD"))
                {
                    manager.BossBulletDObjectPool.Add(other.gameObject);
                    other.gameObject.SetActive(false);
                }
                else if (other.gameObject.tag == "EnemyBullet")
                {
                    Bullet bullet = other.gameObject.GetComponent<Bullet>();
                    
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
                else if (other.gameObject.tag == "Enemy")
                {
                    other.gameObject.SetActive(false);
                    manager.enemyObjectPool.Add(other.gameObject);
                }
                else if (other.gameObject.tag == "Item")
                {
                    GameObject itmObject = GameObject.Find("ItemManager");

                    ItemManager itmManager = itmObject.GetComponent<ItemManager>();
                    other.gameObject.SetActive(false);
                    itmManager.itemObjectPool.Add(other.gameObject);
                }
            }
        }
    }
}

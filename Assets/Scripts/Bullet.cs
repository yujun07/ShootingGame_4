using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float BulletSpeed = 6.0f;
    
    public string Type;
    
    public void Move()
    {
        if (GameManager.instance.isGameover == false)
        {
            Bullet bullet = gameObject.GetComponent<Bullet>();
            Vector3 dir = Vector3.up;
            if (bullet.Type == "EnemyA"||bullet.Type=="EnemyB")
            {
                dir = Vector3.down;
            }
            transform.position += dir * BulletSpeed * Time.deltaTime;
        }
    }
    void Update()
    {
        Move();
    }
}

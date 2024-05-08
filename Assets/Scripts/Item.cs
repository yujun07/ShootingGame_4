using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] float Speed = 5f;
    public string type;
    public void Move()
    {
        if (GameManager.instance.isGameover == false)
        {
            Vector3 dir = Vector3.down;
            transform.position += dir * Speed * Time.deltaTime;
        }
    }
    
    void Update()
    {
        Move();
    }
}

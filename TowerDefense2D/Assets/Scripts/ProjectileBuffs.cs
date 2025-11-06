using UnityEngine;
using System;
using System.Collections.Generic;


public class ProjectileBuffs : MonoBehaviour
{
    private float collisioncount;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<TowerAttacks>() != null && collisioncount < 4)
        {
            collision.gameObject.GetComponent<TowerAttacks>().buff = 1.25f;
            collisioncount++;
        }
    }
}

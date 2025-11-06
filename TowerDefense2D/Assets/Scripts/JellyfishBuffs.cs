using UnityEngine;
using System;
using System.Collections.Generic;

public class JellyfishBuffs : MonoBehaviour
{
    [SerializeField] private GameObject buff;

    private void Start()
    {
        Instantiate(buff, transform.position,Quaternion.identity);
    }

}

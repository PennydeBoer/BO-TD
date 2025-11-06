using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    public static event Action<float> ManaPerEnemy;
    public static event Action<string> OnWaveIncrease;
    private int waveCount;
    [SerializeField] private float waveMax;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject boss;
    [SerializeField] private float enemyMax;
    private float enemyCount = 0;
    private float enemyDeaths = 0;
    private float timer;
    private float bossCount = 0;
    private float bossMax;
    private float Deathtimer;
    [SerializeField] private float interval;
    private float manaPerDeath;
   
    void Start()
    {
        ProjectileBehavior.OnDeath += EnemyDeathCounter;
        Invoke("AfterStart", 0.1f);
        TowerBuy.placedTowers += TowerSpawn;
        Enemies.OnEndpointReached += EnemiesEndpointReached;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        Deathtimer += Time.deltaTime;
        EnemySpawner();
        GameObject[] enmies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enmies.Length == 0 && enemyCount >= 1)
        {
            if (waveCount < waveMax)
            {
                waveCount++;
                EnemyWaveSpawnIncrease(waveCount);
            }
            else if(waveCount == waveMax)
            {
                SceneManager.LoadScene(4);
            }
            for (int i = 0; i < enemyCount-enemyDeaths; i++)
            {
                enemyDeaths++;
                ManaPerEnemy?.Invoke(10);
            }
            
        }
    }
    private void AfterStart()
    {
        OnWaveIncrease?.Invoke(waveCount + "/" + waveMax);
        
    }
    
    private void EnemySpawner()
    {
        if (timer >= interval && enemyCount < enemyMax)
        {
            GameObject enemyClone = Instantiate(enemy, transform.position, Quaternion.identity);
            enemyCount++;
            timer = 0f;
            return;
        }
        else if (timer >= interval+1f && bossCount < bossMax)
        {
            GameObject enemyClone = Instantiate(boss, transform.position, Quaternion.identity);
            bossCount++;
            timer = 0f;
        }
    }
    private void EnemyWaveSpawnIncrease(int waveCount)
    {
        enemyCount = 0;
        enemyDeaths = 0;
        bossCount = 0;
        enemyMax = enemyMax * 1.1f;
        OnWaveIncrease?.Invoke(waveCount + "/" + waveMax);
        if (waveCount >= waveMax / 2)
        {
            bossMax = bossMax * 1.1f;
        }
        if (waveCount == 3 || waveCount == 6 || waveCount == 9)
        {
            bossMax++;
        }
    }
    private void EnemyDeathCounter(float mana)
    {
        if (Deathtimer >= 0.000000000001f)
        {
            manaPerDeath = mana;
            if (waveCount >= waveMax / 2)
            {
                manaPerDeath = manaPerDeath * 0.9f;
            }
            enemyDeaths++;
            Deathtimer = 0f;
            ManaPerEnemy?.Invoke(manaPerDeath);
        }
    }
    private void EnemiesEndpointReached(float dmg)
    {
        
            enemyDeaths++;
            if (enemyDeaths >= enemyMax + bossMax && waveCount < waveMax)
            {
                waveCount++;
                EnemyWaveSpawnIncrease(waveCount);
            }
     
    }
    private void TowerSpawn(Transform position, GameObject tower)
    {
        Instantiate(tower, position.position, Quaternion.identity);
    }
}

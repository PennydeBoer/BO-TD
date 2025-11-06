using System;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttacks : TowerSwitchAttack
{
    [SerializeField] private float castDistance;
    [SerializeField] private LayerMask PlayerLayer;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private float castTime;
    private BaseUI Paused;
    private RaycastHit2D hit;
    private float timeElapsed;
    private Vector3 spawnPosition;
    private ProjectileBehavior behavior;
    private GameObject projectileInstance;
    private Transform target;
    private float projectiledmg;
    public float buff = 1;
    private GameObject projectile;
    private void Start()
    {
        GameObject temp = GameObject.Find("MainCanvas");
        Paused = temp.GetComponent<BaseUI>();
    }
    private void Update()
    {
        if (!Paused.isPaused)
        {
            spawnPosition = transform.position;
            timeElapsed += Time.deltaTime;
            ProjectileFire();
            projectile= ProjectileSelect();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }
    private float projectileDmgSwitch()
    {
        switch (getProjectileNumber())
        {
            case 0:
                projectiledmg = 15 * buff;
                break;
            case 1:
                projectiledmg = 7.5f * buff;
                break;
            case 2:
                projectiledmg = 30f * buff;
                break;
        }
        return projectiledmg;
    }
    protected void ProjectileFire()
    {
        hit = Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, PlayerLayer);
        if (hit.collider && timeElapsed >= castTime)
        {
            //projectileInstance = Instantiate(attack.Projectiles(),spawnPosition,Quaternion.identity);
            projectileInstance = Instantiate(projectile, spawnPosition, Quaternion.identity);
            behavior = projectileInstance.GetComponent<ProjectileBehavior>();        
            target = hit.transform;
            timeElapsed = 0;
        }
        else if (hit.collider == null || projectileInstance == null || target == null|| behavior == null)
        {
            Destroy(projectileInstance);
            return;
        }
        behavior.fireProjectile(target,projectileDmgSwitch());
        Vector3 directionToTarget = target.position - transform.position;
        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }
}

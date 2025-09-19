using System;
using UnityEngine;

public class TowerAttacks : MonoBehaviour
{
    [SerializeField] private float castDistance;
    [SerializeField] private LayerMask PlayerLayer;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private GameObject fireProjectile;
    [SerializeField] private float castTime;
    private RaycastHit2D hit;
    private float timeElapsed;
    private Vector3 spawnPosition;
    private ProjectileBehavior behavior;
    private GameObject projectileInstance;
    
    private void Update()
    {
        spawnPosition = transform.position;
        timeElapsed += Time.deltaTime;
        hit = Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, PlayerLayer);
        if (hit.collider && timeElapsed >= castTime)
        {
            Debug.Log(gameObject.name +hit.collider.gameObject.name);
            FireProjectile();
            behavior = projectileInstance.GetComponent<ProjectileBehavior>();
            timeElapsed = 0;
        }
        else if (hit.collider == null || projectileInstance == null)
        {
            return;
        }
        Transform target = hit.transform;
        behavior.fireProjectile(target);
    }
    private void FireProjectile()
    {
        projectileInstance = Instantiate(fireProjectile, spawnPosition, Quaternion.identity);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }
}

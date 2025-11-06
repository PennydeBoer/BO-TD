using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Enemies : MonoBehaviour
{
    public static event Action<float> OnEndpointReached;
    [SerializeField] private float dmg;
    [SerializeField] public float hp;
    [SerializeField] public float speed;
    [SerializeField] public float manaWorth;
    private Transform[] waypoints;
    [SerializeField] private float maxDistance;
    private Transform targetwaypoint;
    private int targetIndex = 1;
    public float startSpeed;
    public float speedChangeTimer;
    
    private void Start()
    {
        waypoints = GameObject.Find("path_0").GetComponentsInChildren<Transform>();
        targetwaypoint = waypoints[targetIndex];
        startSpeed = speed;
    }
    private void Update()
    {
        speedChangeTimer += Time.deltaTime;
        if (speedChangeTimer> 1 && startSpeed < speed || speedChangeTimer > 1 && startSpeed > speed )
        {
            speed = startSpeed;
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
        FollowWaypoints(targetwaypoint);
        CheckArrival();
        if (gameObject.GetComponent<SpriteRenderer>().color == Color.red && speedChangeTimer> 0.1)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
    private void FollowWaypoints(Transform waypoint)
    {
        Vector3 directionToTarget = waypoint.position - transform.position;
        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.position = Vector3.MoveTowards(transform.position,waypoint.position,speed*Time.deltaTime);
    }
    private void CheckArrival()
    {
        float distance = Vector3.Distance(transform.position, targetwaypoint.position);
        if(distance < maxDistance)
        {
            targetwaypoint = NextTarget();
        }
    }
    private Transform NextTarget()
    {
        if(targetIndex < waypoints.Length - 1)
        {
            targetIndex++;
        }
        else
        {
            OnEndpointReached?.Invoke(dmg);
            Destroy(gameObject);
            this.enabled = false;
        }
        return waypoints[targetIndex];
    }
}

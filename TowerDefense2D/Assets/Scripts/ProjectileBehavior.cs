using System;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    public static event Action<float> OnDeath;
    [SerializeField] private float speed;
    private float dmg = 15f;
    private Enemies hp;
    private Transform checkTarget;

    public void fireProjectile(Transform target, float dealtdamage)
    {
        dmg = dealtdamage;
        checkTarget = target;
        Vector3 directionToTarget = target.position - transform.position;
        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.position += directionToTarget * speed * Time.deltaTime;
        hp = target.gameObject.GetComponent<Enemies>();
        Destroy(gameObject, 2f);
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.transform == checkTarget)
        {
            hp.GetComponent<SpriteRenderer>().color = Color.red;
            hp.hp -= dmg;
            hp.speedChangeTimer = 0;
            switch (dmg)
            {
                case 7.5f:
                    hp.speed = hp.startSpeed * 0.5f;
                    hp.GetComponent<SpriteRenderer>().color = Color.blue;
                    break;
                case 9.375f:
                    hp.speed = hp.startSpeed * 0.5f;
                    hp.GetComponent<SpriteRenderer>().color = Color.blue;
                    break;
                case 30f:
                    hp.speed = hp.startSpeed * 1.5f;
                    hp.GetComponent<SpriteRenderer>().color = Color.yellow;
                    break;
                case 37.5f:
                    hp.speed = hp.startSpeed * 1.5f;
                    hp.GetComponent<SpriteRenderer>().color = Color.yellow;
                    break;
            }
            if (hp.hp < 0 )
            {
                OnDeath?.Invoke(collision.gameObject.GetComponent<Enemies>().manaWorth);
                Destroy(collision.gameObject);
            }
            Destroy(gameObject);
        }
    }
}

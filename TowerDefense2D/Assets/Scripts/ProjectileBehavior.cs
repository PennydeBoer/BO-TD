using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    [SerializeField] private float speed;
    public void fireProjectile(Transform target )
    {
        Vector3 directionToTarget = target.position - transform.position;
        transform.position += directionToTarget * speed * Time.deltaTime;
        Destroy(gameObject, 4f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}

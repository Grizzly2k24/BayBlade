using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public float bounceForce = 10f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Vector3 bounceDir = (transform.position - collision.transform.position).normalized;
            float adjustedBounce = Mathf.Clamp(bounceForce, 0f, 5f); // or make dynamic
            rb.AddForce(bounceDir * adjustedBounce, ForceMode.Impulse);
            collision.gameObject.GetComponent<HealthManager>()?.TakeDamage(10f);
        }
        else if (collision.gameObject.CompareTag("ArenaWalls"))
        {
            Vector3 bounceDir = -collision.contacts[0].normal;
            float reducedBounce = bounceForce * 0.3f; // Reduce bounce when hitting wall
            rb.AddForce(bounceDir * reducedBounce, ForceMode.Impulse);
        }
    }

    private void LateUpdate()
    {
        Vector3 pos = transform.position;
        pos.y = Mathf.Clamp(pos.y, 0.5f, 1f); // Keep them hovering above ground
        transform.position = pos;
    }

}

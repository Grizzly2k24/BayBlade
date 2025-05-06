using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;
    public float spinSpeed = 500f;
    public float bounceForce = 10f;
    public float stamina = 100f;
    public float staminaDrainRate = 5f;

    private Rigidbody rb;
    private bool isSpinning = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = spinSpeed;
    }

    void FixedUpdate()
    {
        if (!isSpinning) return;

        rb.AddTorque(Vector3.up * spinSpeed, ForceMode.Acceleration);

        Vector3 direction = (player.position - transform.position).normalized;
        rb.AddForce(direction * moveSpeed);

        stamina -= staminaDrainRate * Time.fixedDeltaTime;
        if (stamina <= 0)
        {
            isSpinning = false;
            rb.angularVelocity = Vector3.zero;
        }
    }

    private void LateUpdate()
    {
        Vector3 pos = transform.position;
        pos.y = Mathf.Clamp(pos.y, 0.5f, 1f); // Keep them hovering above ground
        transform.position = pos;
    }


    void OnCollisionEnter(Collision collision)
    {
        if (!isSpinning) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 bounceDir = (transform.position - collision.transform.position).normalized;
            float adjustedBounce = Mathf.Clamp(bounceForce, 0f, 5f); // or make dynamic
            rb.AddForce(bounceDir * adjustedBounce, ForceMode.Impulse);
            collision.gameObject.GetComponent<HealthManager>()?.TakeDamage(10f);
        }
        else if (collision.gameObject.CompareTag("ArenaWalls"))
        {
            Vector3 bounceDir = -collision.contacts[0].normal;
            float reducedBounce = bounceForce * 0.3f; // Softer bounce against wall
            rb.AddForce(bounceDir * reducedBounce, ForceMode.Impulse);
        }
    }
}

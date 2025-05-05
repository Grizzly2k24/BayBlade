using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    //Referenci na obrazek UI.
    //Prom�nou pro �ivoty

    //Metodu take damage kter� p�ijme hodnotu damage a ode�te ho od �ivot�.

    //Metodu UpdateHealthbar kter� aktualizuje vizu�ln� po�et �iot� hr��e.
    public Image healthBar;

    public float maxhp = 100f;
    [SerializeField]private float aktualnihp;

    public GameObject DeathScren;

    public bool isEnemy = false; // Assign this in Inspector


    private void Start()
    {
        aktualnihp = maxhp;
        UpdateHealthBar();
    }

    public void TakeDamage(float damage)
    {
        aktualnihp -= damage;

        aktualnihp = Mathf.Clamp(aktualnihp, 0, maxhp);

        UpdateHealthBar();

        if (aktualnihp <= 0)
        {
            Die();
        }

     
    }
    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, aktualnihp / maxhp, Time.deltaTime * 10f);
            Debug.Log("Health updated: " + aktualnihp + "/" + maxhp);
        }
        else
        {
            Debug.LogWarning("HealthBar is not assigned!");
        }
    }

    private void Die()
    {
        if (isEnemy)
        {
            Debug.Log("Enemy BayBlade destroyed! Respawning...");
            RespawnEnemy();
        }
        else
        {
            Debug.Log("Player BayBlade destroyed!");
            Time.timeScale = 0f;
            DeathScren.SetActive(true);
            Destroy(gameObject);
        }
    }

    private void RespawnEnemy()
    {
        aktualnihp = maxhp;
        UpdateHealthBar();

        // Option 1: Reset position to a spawn point
        transform.position = GetRandomSpawnPosition();
        GetComponent<Rigidbody>().linearVelocity = Vector3.zero;

        // Optional: Reactivate behavior, reset stamina, etc.
    }

    private Vector3 GetRandomSpawnPosition()
    {
        // Random point near center, tweak as needed
        float range = 5f;
        return new Vector3(Random.Range(-range, range), 0.5f, Random.Range(-range, range));
    }


    public void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        aktualnihp = maxhp;
        UpdateHealthBar();
        Time.timeScale = 1f; // Obnov� hru
        Debug.Log("BayBlade byl obnoven!");
        DeathScren.SetActive(false); // Skryje obrazovku smrti
    }
}

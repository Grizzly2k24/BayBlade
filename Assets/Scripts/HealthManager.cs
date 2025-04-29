using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    //Referenci na obrazek UI.
    //Prom�nou pro �ivoty

    //Metodu take damage kter� p�ijme hodnotu damage a ode�te ho od �ivot�.

    //Metodu UpdateHealthbar kter� aktualizuje vizu�ln� po�et �iot� hr��e.
    public Image healthBar;

    public float maxhp = 100f;
    private float aktualnihp;

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
            healthBar.fillAmount = aktualnihp / maxhp;
        }
    }

    private void Die()
    {
        Debug.Log("BayBlade byl zni�en!");
        Destroy(gameObject);
    }
}

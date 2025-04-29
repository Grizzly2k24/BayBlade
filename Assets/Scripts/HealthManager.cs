using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    //Referenci na obrazek UI.
    //Promìnou pro životy

    //Metodu take damage která pøijme hodnotu damage a odeète ho od životù.

    //Metodu UpdateHealthbar která aktualizuje vizuálnì poøet žiotù hráèe.
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
        Debug.Log("BayBlade byl znièen!");
        Destroy(gameObject);
    }
}

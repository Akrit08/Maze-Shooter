using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class HealthBarController : MonoBehaviour
{
    public Image healthBar;
    public float health = 100f;
    public float startHealth = 100f;
    public GameObject splatter;
    public static bool isTakingDamage = false;
    private void Update()
    {
        if (isTakingDamage)
        {
            StartCoroutine(enableSPlatter());

        }

    }
    IEnumerator enableSPlatter()
    {
        splatter.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        splatter.SetActive(false);
    }
    public float takeDamage(float damage)
    {
        isTakingDamage = true;
        health -= damage;
        healthBar.fillAmount = health / startHealth;
        return health;
    }
    public void ResetHealth()
    {
        health = 100f;
        healthBar.fillAmount = 100f;
    }
}

using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{   HealthBarController healthBar;
    GameObject player;
    private void Awake()
    {
        player = GameObject.Find("FPSPlayer");
        healthBar = FindObjectOfType<HealthBarController>();      
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            if (healthBar.takeDamage(1) <= 0)
            {
                if (Player.tries > 1)
                {
                    player.gameObject.GetComponent<Player>().ResetPlayer();
                    player.gameObject.GetComponent<Player>().decreaseTries();
                }
                else
                {
                    Player.GameOverUI.GetComponent<GameOver>().ShowGameOver();
                }
            }
            StartCoroutine(Destroy());
        }
        else
        {
            HealthBarController.isTakingDamage = false;
        }

        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag ("Enemy"))
        {
            StartCoroutine(Destroy());
        }
    }
    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(transform.gameObject);
    }
}

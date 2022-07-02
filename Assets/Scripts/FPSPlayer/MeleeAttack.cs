using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    AudioSource knifeSound;
    public GameObject blood;
    public GameObject MagUI;
    public Animator anim;
    bool beingAttack = false;
    [SerializeField] LineRenderer lineRend;

    // Start is called before the first frame update
    void Start()
    {
        knifeSound = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        lineRend.enabled = false;
        MagUI.SetActive(false);
        if (Input.GetKeyDown(KeyCode.Mouse0) && gameObject.CompareTag("Melee") && !PauseMenu.GameIsPaused)
        {
            knifeSound.Play();
            beingAttack = true;
         anim.SetBool("attacking", true);
        } 
        if (Input.GetButtonUp("Fire1") && gameObject.CompareTag("Melee"))
        {
            beingAttack = false;
            anim.SetBool("attacking", false);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && beingAttack)
        {
            EnemyScript enemy = collision.transform.GetComponent<EnemyScript>();
            Instantiate(blood, enemy.transform.position, Quaternion.identity);
            if (enemy != null)
            {
                enemy.TakeDamage(100f);
            }
        }
    }
}

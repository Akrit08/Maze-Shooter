using UnityEngine;
using System.Collections;

public class GunScript : MonoBehaviour
{
    public GameObject blood;
    public GameObject MagUI;
    public AudioSource gunFireSound;
    public float damage = 25f;
    public float range = 250f;
    public float fireRate = 15f;
    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public Transform muzzle;
    [SerializeField] LineRenderer lineRend;
    Reload reload;
    public GameObject sniper;
    private float nextTimeToFire=0;

    void Start()
    {
        reload = GetComponent<Reload>();
        gunFireSound = GetComponent<AudioSource>();
    }
    void Update()
    {
        lineRend.startWidth = 0.1f;
        lineRend.endWidth = 0.1f;
        lineRend.SetPosition(0, muzzle.position);
        MagUI.SetActive(true);
        reload.updateMagUI();
        if (Input.GetButton("Fire1") && Time.time >=nextTimeToFire && !reload.isReloading && reload.haveAmmo && !PauseMenu.GameIsPaused)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            reload.decreaseAmmo();
            Shoot();
        }
        else
        {
        lineRend.enabled = false;
        }
    }
    
    void Shoot()
    {
        muzzleFlash.Play();
        gunFireSound.Play();
        RaycastHit hitInfo;
        //(start,direction,information var,range)
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hitInfo, range))
        {
            if (!sniper.GetComponent<Scope>().isScoped)
            {
                lineRend.enabled = true;
                lineRend.SetPosition(1, hitInfo.point);
            }
               EnemyScript enemy = hitInfo.transform.GetComponent<EnemyScript>();
            Debug.Log(hitInfo.transform.name);
            if(enemy != null)
            {
                Instantiate(blood, hitInfo.point, Quaternion.identity);
                enemy.TakeDamage(damage);
            }
        }
    }
}

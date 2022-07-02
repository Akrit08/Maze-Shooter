using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2BossActivator : MonoBehaviour
{
    public GameObject arrow;
    public GameObject boss;

    private void Update()
    {
        
        if (GameObject.Find("BossEnemy1") != null)
        {
            arrow.SetActive(true);
            arrow.transform.LookAt(GameObject.Find("BossEnemy1").transform.position);
        }else if(GameObject.Find("BossEnemy2") != null)
        {
            arrow.SetActive(true);
            arrow.transform.LookAt(GameObject.Find("BossEnemy2").transform.position);
        }
        else if (GameObject.Find("BossEnemy3") != null)
        {
            arrow.SetActive(true);
            arrow.transform.LookAt(GameObject.Find("BossEnemy3").transform.position);
        }
        else if (GameObject.Find("BossEnemy4") != null)
        {
            arrow.SetActive(true);
            arrow.transform.LookAt(GameObject.Find("BossEnemy4").transform.position);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            boss.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossActivator : MonoBehaviour
{ public GameObject bossEnemy;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag== "Player")
        {
            bossEnemy.SetActive(true);
        }
    }
}

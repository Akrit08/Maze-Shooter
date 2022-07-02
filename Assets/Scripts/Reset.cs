using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    public GameObject player;
    private void OnTriggerEnter(Collider other)
    {
        player.GetComponent<Player>().ResetPlayer();
    }
   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionScipt : MonoBehaviour
{
    [SerializeField]
    GameObject finalWall;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(finalWall.transform.position);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope : MonoBehaviour
{
    public Animator animator;
    public  bool isScoped = false;

    public GameObject scopeOverlay;
    public GameObject weaponCamera;
    public Camera mainCamera;

    public float scopeFOV = 15f;
    private float normalFOV;
 
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            toggleScope();
        }

    }
    public void toggleScope()
    {   isScoped = !isScoped;
        animator.SetBool("isScoped", isScoped);
        if (isScoped)
        {
            StartCoroutine(OnScoped());
        }
        else
        {
            OnUnScoped();
        }
    }

    IEnumerator OnScoped()
    {
        yield return new WaitForSeconds(.15f);
        scopeOverlay.SetActive(true);
        weaponCamera.SetActive(false);

        normalFOV = mainCamera.fieldOfView;
        mainCamera.fieldOfView = scopeFOV;
    }
    public void OnUnScoped()
    {   
        scopeOverlay.SetActive(false);
        weaponCamera.SetActive(true);

        mainCamera.fieldOfView = normalFOV;
    }
}

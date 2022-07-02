using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reload : MonoBehaviour
{
    public int magCapacity;
    public int remainingBullets;
    public int lowAmmo;
    public GameObject MagUI;
    public GameObject reloadText;
    public bool isReloading = false;
    public bool haveAmmo = true;
    private void OnEnable()
    {
        isReloading = false;
        hideReloading();
    }
    void Start()
    {        
        remainingBullets = magCapacity;
        updateMagUI();
    }

    public void checkReloadText()
    {
        if (remainingBullets <= lowAmmo && !isReloading)
        {
            reloadText.GetComponent<TMPro.TextMeshProUGUI>().text = "Low Ammo(Press R)";
            reloadText.SetActive(true);
        }
        if (isReloading)
        {
            reloadText.GetComponent<TMPro.TextMeshProUGUI>().text = "Reloading";
        }
    }
    public void hideReloading()
    {
        reloadText.SetActive(false);
    }
    void Update()
    {
        checkReloadText();
        if (remainingBullets <= lowAmmo && Input.GetKey(KeyCode.R) && !PauseMenu.GameIsPaused)
        {
            StartCoroutine(ForReload());
        }
        if (remainingBullets <= 0)
        {
            haveAmmo = false;
        }
    }
    public void ReloadAmmo()
    {
        remainingBullets = magCapacity;
        reloadText.SetActive(false);
        isReloading = false;
        haveAmmo = true;
        updateMagUI();
    }
    IEnumerator ForReload()
    {
        isReloading = true;
        yield return new WaitForSeconds(1.5f);
        ReloadAmmo();
    }
    public void decreaseAmmo()
    {
        remainingBullets -= 1;
        updateMagUI();
    }
    public void updateMagUI()
    {
        MagUI.GetComponent<TMPro.TextMeshProUGUI>().text = remainingBullets + "/" + magCapacity;
    }
}

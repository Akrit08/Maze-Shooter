using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    public GameObject  sniper;
    public int selectedWeapon = 0;
    public GameObject reloadText;
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.GameIsPaused)
        {
            checkScrollAndKeyPress();
        }
    }
    void checkScrollAndKeyPress()
    {
        if (selectedWeapon == 0)
        {
            reloadText.SetActive(false);
        }
        int prevSelectedWeapon = selectedWeapon;
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
            {
                selectedWeapon = 0;
            }
            else
            {
                selectedWeapon++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0f)
            {
                selectedWeapon = transform.childCount - 1;
            }
            else
            {
                selectedWeapon--;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            selectedWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            selectedWeapon = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            selectedWeapon = 3;
        }
        if (prevSelectedWeapon != selectedWeapon)
        {
            SelectWeapon();
        }
    }
    void SelectWeapon()
    {
        if (sniper.activeInHierarchy && sniper.GetComponent<Scope>().isScoped)
        {
            sniper.GetComponent<Scope>().toggleScope();
        }

        int index = 0;
        foreach(Transform weapon in transform)
        {
            if(index== selectedWeapon)
            {
                weapon.gameObject.SetActive(true);     
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            index++;
        }
    }
}

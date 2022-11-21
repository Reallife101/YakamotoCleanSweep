using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeapons : MonoBehaviour
{
    [SerializeField] private KeyCode[] swapKeys;
    [SerializeField] private GameObject[] weapons;
    [SerializeField] private int startingWeaponIndex = 0;
    [SerializeField] GameObject sprayIcon;
    [SerializeField] GameObject broomIcon;
    [SerializeField] GameObject soapIcon;
    [SerializeField] GameObject sprayCrosshair;
    [SerializeField] GameObject broomCrosshair;
    [SerializeField] GameObject soapCrosshair;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject weapon in weapons)
        {
            if (weapon == weapons[startingWeaponIndex])
            {
                weapon.SetActive(true);
            }
            else
            {
                weapon.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        WeaponSwap();
    }

    private void WeaponSwap()
    {
        for (int i = 0; i < swapKeys.Length; i++)
        {
            if (Input.GetKeyDown(swapKeys[i]))
            {
                enableWeapons(i);
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            startingWeaponIndex-=1;
            if (startingWeaponIndex < 0)
            {
                startingWeaponIndex = weapons.Length - 1;
            }
            enableWeapons(startingWeaponIndex);
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            startingWeaponIndex += 1;
            if (startingWeaponIndex >= weapons.Length)
            {
                startingWeaponIndex = 0;
            }
            enableWeapons(startingWeaponIndex);
        }
    }

    private void enableWeapons(int index)
    {
        foreach (GameObject weapon in weapons)
        {
            if (weapon == weapons[index])
            {
                weapon.SetActive(true);

                if (weapon.name == "Aerosol") {
                    sprayIcon.SetActive(true);
                    broomIcon.SetActive(false);
                    soapIcon.SetActive(false);
                    sprayCrosshair.SetActive(true);
                    broomCrosshair.SetActive(false);
                    soapCrosshair.SetActive(false);
                } else if (weapon.name == "Broom") {
                    sprayIcon.SetActive(false);
                    broomIcon.SetActive(true);
                    soapIcon.SetActive(false);
                    sprayCrosshair.SetActive(false);
                    broomCrosshair.SetActive(true);
                    soapCrosshair.SetActive(false);
                } else if (weapon.name == "SoapTalisman") {
                    sprayIcon.SetActive(false);
                    broomIcon.SetActive(false);
                    soapIcon.SetActive(true);
                    sprayCrosshair.SetActive(false);
                    broomCrosshair.SetActive(false);
                    soapCrosshair.SetActive(true);
                }
            }
            else
            {
                weapon.SetActive(false);
            }
        }
    }
}

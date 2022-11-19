using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeapons : MonoBehaviour
{
    [SerializeField] private KeyCode[] swapKeys;
    [SerializeField] private GameObject[] weapons;
    [SerializeField] private int startingWeaponIndex = 0;

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
            }
            else
            {
                weapon.SetActive(false);
            }
        }
    }
}

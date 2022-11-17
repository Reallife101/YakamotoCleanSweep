using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeapons : MonoBehaviour
{
    [SerializeField] private KeyCode[] swapKeys;
    [SerializeField] private GameObject[] weapons;

    // Start is called before the first frame update
    void Start()
    {
        
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
                foreach (GameObject weapon in weapons)
                {
                    if (weapon == weapons[i])
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
    }
}

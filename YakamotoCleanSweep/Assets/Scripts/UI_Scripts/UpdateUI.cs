using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateUI : MonoBehaviour
{
    [SerializeField] GameObject sprayIcon;
    [SerializeField] GameObject broomIcon;
    [SerializeField] GameObject soapIcon;
    [SerializeField] GameObject sprayCrosshair;
    [SerializeField] GameObject broomCrosshair;
    [SerializeField] GameObject soapCrosshair;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            sprayIcon.SetActive(true);
            broomIcon.SetActive(false);
            soapIcon.SetActive(false);
            sprayCrosshair.SetActive(true);
            broomCrosshair.SetActive(false);
            soapCrosshair.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            sprayIcon.SetActive(false);
            broomIcon.SetActive(true);
            soapIcon.SetActive(false);
            sprayCrosshair.SetActive(false);
            broomCrosshair.SetActive(true);
            soapCrosshair.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            sprayIcon.SetActive(false);
            broomIcon.SetActive(false);
            soapIcon.SetActive(true);
            sprayCrosshair.SetActive(false);
            broomCrosshair.SetActive(false);
            soapCrosshair.SetActive(true);
        }
    }

}

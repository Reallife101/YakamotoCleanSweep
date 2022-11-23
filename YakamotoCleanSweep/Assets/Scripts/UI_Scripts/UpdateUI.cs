using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateUI : MonoBehaviour
{
    [SerializeField] GameObject spray;
    [SerializeField] GameObject broom;
    [SerializeField] GameObject soap;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1)) {
            spray.SetActive(true);
            broom.SetActive(false);
            soap.SetActive(false);
        }
        else if (Input.GetKey(KeyCode.Alpha2)) {
            spray.SetActive(false);
            broom.SetActive(true);
            soap.SetActive(false);
        }
        else if (Input.GetKey(KeyCode.Alpha3)) {
            spray.SetActive(false);
            broom.SetActive(false);
            soap.SetActive(true);
        }
    }

}

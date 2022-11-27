using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateUI : MonoBehaviour
{   
    [SerializeField] private GameObject[] states;
    [SerializeField] private GameObject[] butlers;
    [SerializeField] private GameObject[] maids;
    /* put this behavoir into the weapon swap script
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
    */
    void Start() {
        this.updateCharacter();
    }
    
    public void updateCharacter() {
        if (PlayerPrefs.GetString("character", "") == "Maid") {
            foreach (GameObject b in butlers) {
                b.SetActive(false);
            }
            foreach (GameObject m in maids) {
                m.SetActive(true);
            }
        }
        else {
            foreach (GameObject b in butlers) {
                b.SetActive(true);
            }
            foreach (GameObject m in maids) {
                m.SetActive(false);
            }
        }
    }

    public void setHealth(int health) {
        for (int i = 0; i < states.Length; i++) {
            if (i == health - 1) {
                states[i].SetActive(true);
            }
            else {
                states[i].SetActive(false);
            }    
        }
    }
}

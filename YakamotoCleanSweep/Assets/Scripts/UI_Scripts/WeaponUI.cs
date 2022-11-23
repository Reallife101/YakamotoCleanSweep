using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] private Ranged weapon;
    private int remaining;
    private string size;
    private TextMeshProUGUI textMesh;
    // Start is called before the first frame update
    void Start()
    {
        this.size = this.weapon.getSize().ToString();
        this.remaining = this.weapon.getSize();
        this.textMesh = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        this.remaining = weapon.getAmmo();
        textMesh.text = remaining.ToString() + " / " + size; 
    }
}

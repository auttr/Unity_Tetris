using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Image energyBar;
    Image hpBar;
    float hp;
    float energy;
    private void Awake()
    {
        energyBar = GameObject.Find("Image_��q").GetComponent<Image>();
        hpBar = GameObject.Find("Image_���").GetComponent<Image>();
    }

}

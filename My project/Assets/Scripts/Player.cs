using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    Image energyBar;
    Image hpBar;
    [SerializeField, Header("HP")]
    float hpSet;

    float hp;
    [SerializeField, Header("energy")]
    float energySet;
    float energy;
    private void Awake()
    {
        energyBar = GameObject.Find("Image_¯à¶q").GetComponent<Image>();
        hpBar = GameObject.Find("Image_¦å±ø").GetComponent<Image>();
        hp = hpSet;

    }
    private void Update()
    {
        hpBar.fillAmount = hp / hpSet;
        Dead();
    }
    public void GetHurt(float damage)
    {
        hp -= damage;
    }
    void Dead()
    {
        if (hp<=0)
        {
            GameManager.Instance.FinishGame();
        }
    }

}

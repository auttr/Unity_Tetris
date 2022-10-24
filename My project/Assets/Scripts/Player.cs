using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
namespace auttr
{
    public class Player : MonoBehaviour
    {
        Image energyBar;
        Image hpBar;
        [SerializeField, Header("HP")]
        float hpSet;
        float hp;
        [SerializeField, Header("energy")]
        float energySet;
        public float energy;
        bool isOP;
        float timer;
        float time = 20f;
        private void Awake()
        {
            energyBar = GameObject.Find("Image_能量").GetComponent<Image>();
            hpBar = GameObject.Find("Image_血條").GetComponent<Image>();
            hp = hpSet;

        }
        private void Update()
        {
            hpBar.fillAmount = hp / hpSet;
            energyBar.fillAmount = (energy > energySet) ? energySet : GameManager.Instance.totalScore / energySet;
            Dead();
            if (energyBar.fillAmount == 1)
            {
                timer += Time.deltaTime;
                if (timer >= time)
                {
                    timer = 0;
                    isOP = false;
                    energy = 0;
                    return;
                }

                OPTime();
            }

        }
        public void GetHurt(float damage)
        {
            if (isOP)
            {
                return;
            }
            else
            {
                hp -= damage;
            }

        }
        void Dead()
        {
            if (hp <= 0)
            {
                GameManager.Instance.isWin = false;
                GameManager.Instance.FinishGame();
            }
        }
        void OPTime()
        {
            isOP = true;
            //特效
        }

    }

}

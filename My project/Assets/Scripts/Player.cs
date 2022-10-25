using System.Collections;
using System.Collections.Generic;
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
        float time = 12f;
        ParticleSystem OPfire;
        [Header("火焰")] public AudioClip audioClipsFire;
        int playCount;
        private void Awake()
        {
            timer = time;
            energyBar = GameObject.Find("Image_能量").GetComponent<Image>();
            hpBar = GameObject.Find("Image_血條").GetComponent<Image>();
            hp = hpSet;
            OPfire = GameObject.Find("Particle System_OP火焰").GetComponent<ParticleSystem>();
            OPfire.Pause();
        }
        private void Update()
        {
            hpBar.fillAmount = hp / hpSet;


            if (energy >= energySet)
            {
                timer -= Time.deltaTime;
                energyBar.fillAmount = (timer <= 0) ? 0 : timer / time;
                if (timer <= 0)
                {
                    timer = time;
                    OPTime(false);
                    energy = 0;
                    OPfire.Stop();
                    playCount = 0;
                }
                else
                {

                    OPTime(true);
                }

            }
            else
            {
                energyBar.fillAmount = (energy > energySet) ? energySet : energy / energySet;
            }
            Dead();
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
        void OPTime(bool isOPOP)
        {
            isOP = isOPOP;
            //特效
            OPfire.Play();
            AudioFire();
        }
        void AudioFire()
        {
            if (playCount == 0)
            {
                playCount++;
               GameManager.Instance.audioSourceFall.PlayOneShot(audioClipsFire);

            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
namespace auttr
{
    public class EnemySystem : MonoBehaviour
    {
        #region Fields
        [SerializeField, Header("��q")]
        float hpSet;
        float hp;
        [SerializeField, Header("�������j")]
        float attackCdSet;
        float attackCd;
        Image imageHp;
        [SerializeField, Header("�����ˮ`")]
        float attackDamage;

        public bool a;
        Animator ani;
        string paraDead = "Ĳ�o��";
        string paraHurt = "Ĳ�o�h";
        string paraAtt = "Ĳ�o����";

        public int hurtCount;
        public int comboCount;

        float timer;
        float time = 2.5f;

        Player player;
        TextMeshProUGUI attCountText;
        #endregion
        #region Unity Event Func
        private void Awake()
        {
            hp = hpSet;
            attackCd = attackCdSet;
            imageHp = GameObject.Find("Image_�ĤH���").GetComponent<Image>();
            ani = GetComponent<Animator>();
            player = GameObject.Find("Player").GetComponent<Player>();
            attCountText = GameObject.Find("Text_�����˼�").GetComponent<TextMeshProUGUI>();

        }
        private void Update()
        {
            imageHp.fillAmount = hp / hpSet;
            Attack();
            Dead();
        }
        #endregion
        public void GetHurt(float damage)
        {

            hurtCount++;
            hp -= damage;
            ani.SetTrigger(paraHurt);

        }
        public void ComboDamage(float damage)
        {
            hp -= damage;
        }
        void Dead()
        {
            if (hp <= 0)
            {
                timer += Time.deltaTime;
                ani.SetBool(paraDead, true);
                if (gameObject.CompareTag("Boss"))
                {
                    if (timer > time)
                    {

                        timer = 0;
                        Destroy(gameObject);
                        GameManager.Instance.isWin = true;
                        GameManager.Instance.FinishGame();
                    }

                }
                else
                {
                    if (timer > time)
                    {
                        timer = 0;
                        Destroy(gameObject);
                        GameManager.Instance.FinishWave();
                    }


                }

            }
        }
        void Attack()
        {
            attackCd -= Time.deltaTime;
            attCountText.text = ((int)attackCd).ToString();
            if (attackCd <= 0)
            {

                attackCd = attackCdSet;
                ani.SetTrigger(paraAtt);
                player.GetHurt(attackDamage);
            }


        }


    }

}


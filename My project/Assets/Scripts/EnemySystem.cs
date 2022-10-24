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
        [SerializeField, Header("¦å¶q")]
        float hpSet;
        float hp;
        [SerializeField, Header("§ðÀ»¶¡¹j")]
        float attackCdSet;
        float attackCd;
        Image imageHp;
        [SerializeField, Header("§ðÀ»¶Ë®`")]
        float attackDamage;

        public bool a;
        Animator ani;
        string paraDead = "Ä²µo¦º";
        string paraHurt = "Ä²µoµh";
        string paraAtt = "Ä²µo§ðÀ»";

        public int hurtCount;
        public int comboCount;

        float timer;
        float time = 1.5f;

        Player player;
        TextMeshProUGUI attCountText;
        #endregion
        #region Unity Event Func
        private void Awake()
        {
            hp = hpSet;
            attackCd = attackCdSet;
            imageHp = GameObject.Find("Image_¼Ä¤H¦å±ø").GetComponent<Image>();
            ani = GetComponent<Animator>();
            player = GameObject.Find("Player").GetComponent<Player>();
            attCountText = GameObject.Find("Text_§ðÀ»­Ë¼Æ").GetComponent<TextMeshProUGUI>();

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
                    GameManager.Instance.totalScore += 3000;
                    GameManager.Instance.isWin = true;
                    GameManager.Instance.FinishGame();
                }
                else
                {
                    if (timer > time)
                    {
                        timer = 0;
                        Destroy(gameObject);
                        GameManager.Instance.totalScore += 500;
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


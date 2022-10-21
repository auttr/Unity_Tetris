using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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
    Animator ani;
    string paraDead = "Ĳ�o��";
    string paraHurt = "Ĳ�o�h";

    public int hurtCount;
    public int comboCount;


    
    #endregion
    #region Unity Event Func
    private void Awake()
    {
        hp = hpSet;
        imageHp = GameObject.Find("Image_�ĤH���").GetComponent<Image>();
        ani = GetComponent<Animator>();
    }
    private void Update()
    {
        imageHp.fillAmount = hp / hpSet;
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
            ani.SetBool(paraDead, true);
            GameManager.Instance.FinishGame();
        }
    }


}

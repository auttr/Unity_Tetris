using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySystem : MonoBehaviour
{
    #region Fields
    [SerializeField, Header("血量")]
    float hpSet;
    float hp;
    [SerializeField, Header("攻擊間隔")]
    float attackCdSet;
    float attackCd;
    Image imageHp;
    Animator ani;
    string paraDead = "觸發死";
    string paraHurt = "觸發痛";

    public int hurtCount;
    public int comboCount;

    #endregion
    #region Unity Event Func
    private void Awake()
    {
        hp = hpSet;
        imageHp = GameObject.Find("Image_敵人血條").GetComponent<Image>();
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
        print(hurtCount);
        hp -= damage;
        ani.SetTrigger(paraHurt);

    }
    void Dead()
    {
        if (hp <= 0)
        {
            ani.SetBool(paraDead, true);
        }
    }


}

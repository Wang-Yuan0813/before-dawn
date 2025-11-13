using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [Header("攻击属性")]
    public float smash;
    public float counterSmash;
    public float attackerX;
    public bool isStab;

    private GameObject player;
    private GameObject boss;
    private void Awake()
    {
        player = GameObject.Find("Player");
        boss = this.gameObject.transform.parent.gameObject;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        attackerX = this.transform.parent.transform.position.x;
        
        if (other.gameObject.CompareTag("Player") && boss.GetComponent<Boss_Control>().attackValid)//攻击命中了玩家对象，需要在这里判定一下是否有攻击
        {
            boss.GetComponent<Boss_Control>().attackValid = false;

            if (!player.GetComponent<Player_Control>().cantHit)
            {
                if(!isStab)
                {
                    player.GetComponent<Player_Control>().TakeHit(smash, attackerX);//调用敌人受伤函数，传递伤害参数与攻击者位置
                }
                else
                {
                    player.GetComponent<Player_Control>().GetCatched(attackerX);//调用玩家抓取，玩家播放被抓的动画
                    boss.GetComponent<Boss_Control>().catchPlayer = true;
                    boss.GetComponent<Boss_Control>().isCatchPlayer = true;
                }
            } 
        } 
    }
}

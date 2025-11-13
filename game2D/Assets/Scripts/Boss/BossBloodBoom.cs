using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBloodBoom : MonoBehaviour
{
    [Header("攻击属性")]
    public float smash;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))//攻击命中了玩家对象，需要在这里判定一下是否有攻击
        {
            GameObject Player = other.gameObject;
            Player.GetComponent<Player_Control>().TakeHit(smash, transform.position.x);//调用敌人受伤函数，传递伤害参数与攻击者位置
        }
    }
    public void AnimEnd()
    {
        Destroy(gameObject);
    }
    public void DirectionChange()
    {
        GameObject boss = GameObject.Find("Boss").gameObject;
        float distance = boss.transform.position.x - transform.position.x;
        if(boss.GetComponent<Boss_Control>().superBloodBoom)
            transform.localScale = new Vector2(transform.localScale.x * (1 + Mathf.Abs(distance)/15), transform.localScale.y * (1 + Mathf.Abs(distance) / 15));
        if (distance > 0)
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);

    }
}

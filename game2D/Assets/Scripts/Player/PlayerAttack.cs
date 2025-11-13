using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("需要挂载的对象")]
    private GameObject counterFx;
    private GameObject counterFx1;
    private GameObject player;
    private GameObject boss;


    private Cinemachine.CinemachineCollisionImpulseSource impulse;
    private GameObject cameraControl;


    private void Awake()
    {
        player = transform.parent.gameObject;
        boss = GameObject.Find("Boss");
        impulse = GetComponent<Cinemachine.CinemachineCollisionImpulseSource>();
        counterFx = Resources.Load<GameObject>("FXPref/CounterFx");
        counterFx1 = Resources.Load<GameObject>("FXPref/CounterFx1");
        cameraControl = GameObject.Find("Main Camera");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnemyAttack") && player.GetComponent<Player_Control>().attackValid)//攻击中了Attack的Tag的对象，优先判断是否counter
        {
            float counterSmash;
            float attackerX;
            counterSmash = other.gameObject.GetComponent<BossAttack>().counterSmash;
            attackerX = other.gameObject.GetComponent<BossAttack>().attackerX;

            player.GetComponent<Player_Control>().attackValid = false;

            Instantiate(counterFx,new Vector3(transform.position.x + 0.5f * player.GetComponent<Player_Control>().facedirection, transform.position.y + 0.5f,0),Quaternion.identity,null);
            Instantiate(counterFx1, new Vector3(transform.position.x + 0.5f * player.GetComponent<Player_Control>().facedirection, transform.position.y + 0.5f, 0), Quaternion.identity, null);
            Invincible(0.2f);
            cameraControl.GetComponent<Camera_Control>().HitPause(0.1f);
            impulse.GenerateImpulse();
            player.GetComponent<Player_Control>().Counter(counterSmash, attackerX);
        }
        if(other.gameObject.CompareTag("Enemy")&&!boss.GetComponent<Boss_Control>().cantHit && !player.GetComponent<Player_Control>().isCounter && player.GetComponent<Player_Control>().attackValid)  //boss处于非无敌状态且玩家处于非对刀状态时
        {   
            //没有counter且攻击中了敌人
            player.GetComponent<Player_Control>().attackValid = false;
            boss.GetComponent<Boss_Control>().TakeHit();//传递攻击信号
        }
        
    }
    private void Invincible(float duration)
    {
        player.GetComponent<Player_Control>().isCounter = true;
        player.GetComponent<Player_Control>().dodgeAllow = false;
        Physics2D.IgnoreLayerCollision(7, 9, true);
        StartCoroutine(Delay(duration));
    }
    IEnumerator Delay(float duration)
    {
        while (duration > 0)
        {
            duration -= Time.deltaTime;
            yield return null;
        }
        player.GetComponent<Player_Control>().isCounter = false;
        player.GetComponent<Player_Control>().dodgeAllow = true;
        Physics2D.IgnoreLayerCollision(7, 9, false);

        yield return null;
    }
    public void Counter(float counterSmash, float attackerX)
    {

    }


}

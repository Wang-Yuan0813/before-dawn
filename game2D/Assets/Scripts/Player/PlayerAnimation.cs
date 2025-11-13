using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Physics_Check physicsCheck;
    private Rigidbody2D rb;
    private Player_Control player_Control;
    private GameObject landingFX;


    private void Awake()
    {
        landingFX = Resources.Load<GameObject>("FXPref/PlayerLandingGround");
        anim = GetComponent<Animator>();
        physicsCheck = GetComponent<Physics_Check>();
        rb = GetComponent<Rigidbody2D>();
        player_Control = GetComponent<Player_Control>();

    }
    private void Update()
    {
        SetAnimation();
    }
    public void SetAnimation()
    {
        anim.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));//向动画状态树输入横向速度float
        anim.SetFloat("velocityY", rb.velocity.y);//纵向的速度
        anim.SetBool("isGround", physicsCheck.isGround);//是否脚部位于地面
        anim.SetBool("dodge", player_Control.dodge);
        anim.SetBool("isDodge", player_Control.isDodge);
        anim.SetBool("isAttack",player_Control.isAttack);//处于攻击状态
        anim.SetBool("isJumpAttack", player_Control.isJumpAttack);//处于跳跃攻击状态，区分开避免落地时由于空中的动画没播放完导致接连播放地面动画
        anim.SetBool("attack", player_Control.attack);//攻击触发，这里的bool可以理解成trigger，因为攻击动画的开始帧就将其置为false
        anim.SetBool("getCatched", player_Control.getCatched);
        anim.SetBool("takeHit", player_Control.takeHit);//受伤
        anim.SetBool("isTakeHit", player_Control.isTakeHit);//受伤状态
        anim.SetBool("cantHit", player_Control.cantHit);//角色无敌帧
    }
    public void DodgeStart()
    {
        player_Control.isAttack = false;
        player_Control.canAttack = false;
        player_Control.isJumpAttack = false;
        player_Control.isTakeHit = false;
        player_Control.takeHit = false;
        player_Control.attack = false;
        player_Control.preAttack = false;
        player_Control.dodge = false;

        player_Control.isDodge = true;
        player_Control.cantHit = true;

        Physics2D.IgnoreLayerCollision(7, 9, true);
    }
    public void DodgeOver()
    {
        player_Control.isDodge = false;
        player_Control.cantHit = false;

        Physics2D.IgnoreLayerCollision(7, 9, false);
    }
    public void AttackStart()
    {
        
        player_Control.attack = false;
        player_Control.isAttack = true;
        player_Control.attackValid = true;
    }
    public void AttackOver()
    {
        player_Control.isAttack = false;
        player_Control.attackValid = false;
    }
    public void CanAttack()
    {
        player_Control.canAttack = true;
    }
    public void JumpAttackStart()
    {
        player_Control.attack = false;
        player_Control.isJumpAttack = true;
    }
    public void JumpAttackOver()
    {
        player_Control.isJumpAttack = false;
    }
    public void LandingStart()//这里是防止空中攻击落地后由于为播放完导致没有将isAttack置0，从而二次播放动画
    {
        Instantiate(landingFX, new Vector3(transform.position.x, transform.position.y - 0.2f, 0), Quaternion.identity, null);
        player_Control.isJumpAttack = false;
        player_Control.canAttack = true;
        player_Control.cantHit = false;
    }

    public void JumpStart()
    {
        player_Control.isAttack = false;
        player_Control.canAttack = true;
    }
    public void TakeHitStart()
    {
        player_Control.takeHit = false;
        player_Control.isAttack = false;
        player_Control.attack = false;
        player_Control.isJumpAttack = false;
        player_Control.preAttack = false;
        player_Control.canAttack = false;
        
        player_Control.isTakeHit = true;
        player_Control.cantHit = true;

        Physics2D.IgnoreLayerCollision(7, 9, true);


    }
    public void GetCatchedStart()
    {
        rb.velocity = new Vector2(0, 0);
        player_Control.isTakeHit = true;
        player_Control.cantHit = true;

        player_Control.getCatched = false;
        player_Control.takeHit = false;
        player_Control.isAttack = false;
        player_Control.attack = false;
        player_Control.isJumpAttack = false;
        player_Control.preAttack = false;
        player_Control.canAttack = false;

    }
    public void TakeHitOver()
    {
        player_Control.canAttack = true;

        player_Control.isTakeHit = false;
        player_Control.cantHit = false;
        player_Control.preAttack = false;

        Physics2D.IgnoreLayerCollision(7, 9, false);

    }
    public void InvincibleStart()
    {
        player_Control.cantHit = true;

    }
    public void InvincibleOver()
    {
        player_Control.cantHit = false;
    }
    public void ThrowFX()
    {
        Instantiate(landingFX, new Vector3(transform.position.x, transform.position.y - 0.2f, 0), Quaternion.identity, null);
    }
}

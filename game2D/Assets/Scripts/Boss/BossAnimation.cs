using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimation : MonoBehaviour
{
    private Boss_Control boss_Control;
    private Animator anim;
    private Rigidbody2D rb;
    public int maxAttackTypes;//IdleAttack的攻击动画有几种

    private void Awake()
    {
        anim = GetComponent<Animator>();
        //physicsCheck = GetComponent<Physics_Check>();
        boss_Control = GetComponent<Boss_Control>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("ChooseAttackType", 1, 0.05f);
    }

    // Update is called once per frame
    void Update()
    {

        SetAnimation();
    }
    public void SetAnimation()
    {
        anim.SetFloat("PlayerDistance",boss_Control.playerDistance);

        anim.SetBool("Walk", boss_Control.isWalk);
        anim.SetBool("isTeleport", boss_Control.isTeleport);
        anim.SetBool("teleport", boss_Control.teleport);
        anim.SetBool("isAttack", boss_Control.isAttack);
        anim.SetBool("catchPlayer", boss_Control.catchPlayer);
    }
    void ChooseAttackType()
    {
        anim.SetInteger("AttackType", Random.Range(1, maxAttackTypes));
    }
    public void TeleportStart()
    {
        rb.velocity = Vector2.zero;
        boss_Control.isTeleport = true;
        boss_Control.teleport = false;
        boss_Control.isWalk = false;

    }
    public void TeleportOver()
    {
        boss_Control.isTeleport = false;
        rb.velocity = Vector2.zero;
    }
    //测试用攻击函数
    public void AttackStart()
    {
        boss_Control.isWalk = false;
        boss_Control.isAttack = true;
        boss_Control.attackValid = true;
    }
    public void AttackOver()
    {
        boss_Control.isAttack = false;
        boss_Control.attackValid = false;
    }
    public void Attack1DashStart()
    {
        boss_Control.AttackDash(15,5);
    }
    public void Attack1DashEnd()
    {
        boss_Control.AttackDash(0, 0);
    }
    public void DodgeAttackDodgeStart()
    {
        boss_Control.AttackDash(-20, 0);
    }
    public void DodgeAttackDodgeEnd()
    {
        boss_Control.AttackDash(0, 0);
    }
    public void DodgeAttackDashStart()
    {
        boss_Control.AttackDash(15, 5);
    }
    public void DodgeAttackDashEnd()
    {
        boss_Control.AttackDash(0, 0);
    }
    public void DodgeAttackFlash()
    {
        transform.Translate(4f * boss_Control.facedirection, 0,0);
    }
    public void StabAttackFlash()
    {
        transform.Translate(3f * boss_Control.facedirection, 0, 0);
    }
    public void ThrowOutStart()
    {
        boss_Control.catchPlayer = false;
        boss_Control.isAttack = true;
    }
    public void ThrowOutEnd()
    {
        boss_Control.isAttack = false;
    }
    public void CantHitStart()
    {
        boss_Control.cantHit = true;
    }
    public void CantHitEnd()
    {
        boss_Control.cantHit = false;
    }
    public void BloodBoomStart()
    {
        rb.velocity = Vector2.zero;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Control : MonoBehaviour
{
    private Rigidbody2D Rb;
    private GameObject energyBar;
    [Header("全局控制的属性")]
    public bool attackAllow;
    public bool dodgeAllow;

    [Header("移动参数")]
    public float Speed;//10
    public float moveAcc;
    [Header("移动检测属性")]
    public int facedirection;//-1：左,1：右
    public float moveDir;
    [Header("跳跃参数")]
    public float JumpForce;//15
    public float JumpHoldForce;//6
    public float JumpHoldDuration;//0.05
    [Header("跳跃检测属性")]
    public bool JumpPressed;
    public bool JumpHeld;//长按跳跃键
    public bool IsJump;
    private float JumpTime;//跳跃时间记录
    [Header("躲避参数")]

    public float DodgeSpeed;
    [Header("躲避检测属性")]
    public bool isDodge;
    public bool dodge;



    [Header("环境检测")]
    private Physics_Check physicsCheck;

    [Header("战斗属性")]
    public bool preAttack;
    public float counterTime;//counter攻击后收到冲击的时间

    private float preAttackExist;
    private float lastAttack = -10f;

    [Header("受伤属性")]
    public bool cantHit;
    public bool isTakeHit;
    public bool takeHit;
    public bool getCatched;

    [Header("战斗检测")]
    public bool isAttack;//处于攻击状态
    public bool isJumpAttack;
    public bool canAttack;//可以攻击
    public bool attack;//地面上时攻击被按下
    public bool isCounter;
    public bool attackValid;

    [Header("体力属性设置")]
    private float energyLeft;

    private GameObject cameraControl;
    private GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();//获取GameManager
        cameraControl = GameObject.Find("Main Camera");
        Rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<Physics_Check>();
        energyBar = transform.Find("EnergyBar").gameObject;
        //攻击有关初始化
        canAttack = true;
        preAttackExist = 0.2f;//预输入攻击标志存在时间
        
        //下面这3个之后删掉嗷，现在就是方便调试时候用一下
        attackAllow = true;
        dodgeAllow = true;
    }
    void Update()
    {
        //运动相关
        if (gameManager.playerCanMove)
        {
            if (!isTakeHit)
            {
                if (Input.GetButtonDown("Jump"))//跳跃被按下时
                {
                    JumpPressed = true;
                }
                JumpHeld = Input.GetButton("Jump");
                if (!Input.GetButton("Jump"))
                {
                    JumpPressed = false;
                }
                if (dodgeAllow)
                {
                    if (Input.GetButtonDown("Dodge"))
                    {
                        if (energyLeft > 0)
                            ReadyToDodge();
                    }
                }
            }
            CheckHorzontalMove();//检测移动的方向
            EnergyUpdate();
            //攻击
            if (attackAllow)
                AttackCheck();
        }
    }
    void FixedUpdate()
    {
        Dodge();
        if (!isDodge && !isTakeHit)
        {
            AirMovement();
            FaceDirection();
            GroundMovement();
        }
    }
    public void CheckHorzontalMove()
    {
        float h = Input.GetAxisRaw("Horizontal");
        moveDir = h;
    }
    void GroundMovement()//地面移动代码
    {
        if(moveDir == 0)
        {
            float deltaSpeed = Time.deltaTime * moveAcc * -3;
            if (Mathf.Abs(Rb.velocity.x) >= 2.0f)
            {
                if(Rb.velocity.x > 0)
                    Rb.velocity = new Vector2((Mathf.Abs(Rb.velocity.x) + deltaSpeed), Rb.velocity.y);
                if(Rb.velocity.x < 0)
                    Rb.velocity = new Vector2(-(Mathf.Abs(Rb.velocity.x) + deltaSpeed), Rb.velocity.y);
            }
            else
            { 
                Rb.velocity = new Vector2(0, Rb.velocity.y);
            }
        }
        else
        {
            float deltaSpeed = Time.deltaTime * moveAcc;
            if (Mathf.Abs(Rb.velocity.x) >= Speed)
                Rb.velocity = new Vector2(Speed * moveDir, Rb.velocity.y);
            else
            {
                Rb.velocity = new Vector2((Mathf.Abs(Rb.velocity.x) + deltaSpeed) * moveDir, Rb.velocity.y);
            }
        }
    }
    void AirMovement()
    {
        if (JumpPressed && physicsCheck.isGround)
        {
            IsJump = true;

            isAttack = false;//跳跃会打断当前攻击动画

            JumpTime = Time.time + JumpHoldDuration;
            Rb.velocity = new Vector2(Rb.velocity.x, JumpForce);

            JumpPressed = false;
        }
        else if (IsJump)
        {
            if (JumpHeld)
                Rb.AddForce(new Vector2(0f, JumpHoldForce), ForceMode2D.Impulse);
            if (JumpTime < Time.time)
                IsJump = false;
            JumpPressed = false;
        }
    }
    void Dodge()//躲避
    {
        if(isDodge)
        {
            //Physics.IgnoreLayerCollision(7,9);
            Rb.velocity = new Vector2(DodgeSpeed * facedirection, 0);
            ShadowPool.instance.GetFromPool();
        }
    }
    void ReadyToDodge()
    {
        dodge = true;
        energyBar.GetComponent<EnergyBarControl>().EnergyConsume(3);
    }
    void AttackCheck()//检测是否有攻击输入
    {
        if (lastAttack + preAttackExist < Time.time)
        {
            preAttack = false;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (energyLeft > 0 && canAttack && !isDodge && !dodge)
            {
                Attack();
            }
            else
            {
                lastAttack = Time.time;
                preAttack = true;
            }
            
        }
        //当没有按键但是处于预攻击状态时
        if(preAttack && energyLeft > 0 && canAttack && !isDodge && !dodge)
        {
            Attack();
        }
    }
    void Attack()
    {
        energyBar.GetComponent<EnergyBarControl>().EnergyConsume(1);//消耗的量
        attack = true;
        canAttack = false;
        preAttack = false;
    }

    void EnergyUpdate()
    {
        energyLeft = energyBar.GetComponent<EnergyBarControl>().energyLeft;
    }

    void FaceDirection()//转向控制
    {
        if (moveDir > 0) 
            facedirection = 1;
        if(moveDir < 0)
            facedirection = -1;
        switch (facedirection)
        {
            case 1: transform.localScale = new Vector2(1, 1); break;//朝右,使用sr.flip无法改变碰撞体
            case -1: transform.localScale = new Vector2(-1, 1); break;//朝左
            default: break;
        }
    }
    //受伤动画以及收到的冲击大小
    public void TakeHit(float smash, float attackerX)
    {
        float hitDir;
        hitDir = transform.position.x - attackerX;
        takeHit = true;
        if (hitDir > 0)//收到向右侧的冲击
        {
            Rb.velocity = new Vector2(smash, Rb.velocity.y);
        }
        else
        {
            Rb.velocity = new Vector2(-smash, Rb.velocity.y);
        }   
    }
    public void Counter(float smash, float attackerX)
    {
        float hitDir;
        hitDir = transform.position.x - attackerX;
        //isCounter = true;
        if (hitDir > 0)//收到向右侧的冲击
        {
            Rb.velocity = new Vector2(smash, Rb.velocity.y);
        }
        else
        {
            Rb.velocity = new Vector2(-smash, Rb.velocity.y);
        }
    }

    public void GetCatched(float attackerX)
    {
        if (!cantHit)
        {
            float hitDir;
            hitDir = transform.position.x - attackerX;
            getCatched = true;
            
            if (hitDir > 0)//收到向右侧的冲击
                facedirection = -1;
            else
                facedirection = 1;
            switch (facedirection)
            {
                case 1: transform.localScale = new Vector2(1, 1); break;//朝右,使用sr.flip无法改变碰撞体
                case -1: transform.localScale = new Vector2(-1, 1); break;//朝左
                default: break;
            }

        }
    }
    public void ThrowOutStart()
    {
        Rb.velocity = new Vector2(20f * facedirection, 0);
        Rb.drag = 2;
    }
    public void ThrowOutEnd()
    {
        Rb.velocity = new Vector2(0, 0);
        Rb.drag = 0;
    }
    private void CameraZoomIn()
    {

    }
    private void CameraZoomOut()
    {

    }
}

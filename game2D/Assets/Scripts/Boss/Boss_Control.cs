using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Control : MonoBehaviour
{
    
    [Header("绑定组件及对象")]
    private GameObject player;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private GameObject cameraControl;
    private Cinemachine.CinemachineCollisionImpulseSource impulse;
    private GameObject hitFX;
    private GameObject bloodBoom;


    [Header("移动设置")]
    public float WalkDistance;//距离超过这个值，将向玩家移动
    public float StopDistance;//停止移动的距离
    public float WalkSpeed;

    [Header("传送设置")]
    public float teleportCD;
    public float teleportDistance;//超过这个数值开始传送
    public bool isTeleport;
    public bool teleport;//该bool型变量可当作触发器
    private float lastTeleportTime = -10f;
    [Header("动画标志位检测")]
    public bool isWalk;
    [Header("检测属性")]
    public float playerDistance;//与玩家对象的距离
    public int facedirection;//1左-1右
    [Header("攻击设置")]
    public bool isAttack;//攻击进行标志
    public bool attack;//攻击触发标志（理解成trigger）
    public bool catchPlayer;
    public bool isCatchPlayer;
    public bool attackValid;

    [Header("受伤设置")]
    public bool isTakeHit;
    public bool cantHit;
    [Header("技能设置")]
    public bool superBloodBoom;


    private float Distance;
    void Start()
    {
        hitFX = Resources.Load<GameObject>("FXPref/HitFX");
        impulse = GetComponent<Cinemachine.CinemachineCollisionImpulseSource>();
        cameraControl = GameObject.Find("Main Camera");
        bloodBoom = Resources.Load<GameObject>("FXPref/BloodBoom");
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        Distance_cal();//计算距离

        //Attack();
        if(!isAttack)
            Movement();
        if(isCatchPlayer)
        {
            player.transform.position = new Vector3(transform.position.x + 1f * facedirection, transform.position.y + 0.3f, 0);
        }
    }
    //距离判定
    void Distance_cal()
    {
        Distance = player.transform.position.x - transform.position.x;
        playerDistance = Mathf.Abs(Distance);
    }
    //移动
    void Movement()
    {
        Teleport();
        if(!isTeleport)
        {
            Walk();
        } 
    }
    void Walk()
    {
        FaceDirection();
        if (playerDistance >= WalkDistance)
        {
            isWalk = true;
        }
        if (playerDistance <= StopDistance)
        {
            isWalk = false;
        }
        if (isWalk)
        {
            rb.velocity = new Vector2(WalkSpeed * facedirection, 0);
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    void Teleport()
    {
        if (playerDistance >= teleportDistance && (Time.time - lastTeleportTime)>=teleportCD)
        {
            //rb.velocity = new Vector2(0, 0);
            teleport = true;
            lastTeleportTime = Time.time;
        }
        else
        {
            teleport = false;
        }
    }
    public void TeleportToPlayer()//这个作为事件函数，供动画使用
    {
        transform.position = new Vector2(player.transform.position.x, transform.position.y);
    }
    /// <summary>
    /// 面向玩家
    /// </summary>
    public void FaceDirection()
    {
        if(Distance >= 0)
            facedirection = 1;
        else
            facedirection = -1;
        switch (facedirection)
        {
            case 1: transform.localScale = new Vector2(1, 1); break;//朝右,使用sr.flip无法改变碰撞体
            case -1: transform.localScale = new Vector2(-1, 1); break;//朝左
            default: break;
        }
    }
    /// <summary>
    /// 攻击引发的前冲效果
    /// </summary>
    /// <param name="dashSpeed">前冲初速度</param>
    /// <param name="linearDrag">前冲受阻效果</param>
    public void AttackDash(float dashSpeed,float linearDrag)
    {
        rb.velocity = new Vector2(dashSpeed * facedirection, 0);
        rb.drag = linearDrag;
    }
    /// <summary>
    /// 这个函数是给投技用的事件函数，目的是将玩家丢出前改变玩家的位置
    /// </summary>
    public void ChangePlayerPosition()
    {
        player.transform.position = new Vector3(transform.position.x - 1f * facedirection, transform.position.y, 0);
        isCatchPlayer = false;

    }
    public void TakeHit()
    {
        if(!cantHit)//处于非攻击状态时
        {
            TakeHitFX();
            TakeHitHP();
        }
        else//处于攻击无敌帧时，背后可以遭受攻击
        {
            if(!isTakeHit &&(player.transform.position.x - transform.position.x) * facedirection <= 0)
            {
                TakeHitFX();
                TakeHitHP();
            }
        }
    }
    private void TakeHitFX()
    {
        isTakeHit = true;
        Instantiate(hitFX, new Vector3(transform.position.x, transform.position.y + 0.5f, 0), Quaternion.identity, null);
        cameraControl.GetComponent<Camera_Control>().HitPause(0.1f);
        StartCoroutine(TurnColor(0.2f));
        impulse.GenerateImpulse();
    }

    public void TakeHitHP()
    {

    }

    IEnumerator TurnColor(float duration)
    {
        while(duration>0)
        {
            duration -= Time.deltaTime;
            sr.color = new Color(0,0,0,1);
            isTakeHit = true;
            yield return null;
        }
        isTakeHit = false;
        sr.color = new Color(1, 1, 1, 1);
        yield return null;
    }
    
    public void BloodBoom()
    {
        if (!superBloodBoom)
            StartCoroutine(CreateBloodBoom(4, 0.5f));
        else
            StartCoroutine(CreateBloodBoom(5, 0.5f));
    }
    IEnumerator CreateBloodBoom(int times, float sec)
    {
        for(int i = 1;i<=times;i++)
        {
            if(!superBloodBoom)
            {
                Instantiate(bloodBoom, new Vector3(transform.position.x + i * 1.5f, transform.position.y + 1.2f, 0), Quaternion.identity, null);
                Instantiate(bloodBoom, new Vector3(transform.position.x - i * 1.5f, transform.position.y + 1.2f, 0), Quaternion.identity, null);
            }
            else
            {
                Instantiate(bloodBoom, new Vector3(transform.position.x + i * 1.5f, transform.position.y + 1.1f + i * 0.1f, 0), Quaternion.identity, null);
                Instantiate(bloodBoom, new Vector3(transform.position.x - i * 1.5f, transform.position.y + 1.1f + i * 0.1f, 0), Quaternion.identity, null);
            }
            yield return new WaitForSeconds(sec);
        }
    }

}

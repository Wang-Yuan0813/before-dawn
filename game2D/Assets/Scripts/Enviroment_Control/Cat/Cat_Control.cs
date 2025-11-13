using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat_Control : MonoBehaviour
{
    [Header("属性设置")]
    public float walkSpeed;
    public float runSpeed;
    public float idleRange;
    public float walkRange;
    [Header("公共变量")]
    public bool idle;
    public bool walk;
    public bool run;

    private Animator anim;
    private GameObject player;
    public float distance;
    private bool isWalk;
    private bool isRun;
    private int facedirection;
    private Rigidbody2D rb;
    private float idleLastTime;
    public bool isIdle;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {        
        distance = transform.position.x - player.transform.position.x;
        
        Movement();
    }
    
    /// <summary>
    /// 面向玩家
    /// </summary>
    public void FaceDirection()
    {
        if (distance <= 0)
            facedirection = 1;
        else
            facedirection = -1;
        switch (facedirection)
        {
            case 1: transform.localScale = new Vector2(1, 1); break;
            case -1: transform.localScale = new Vector2(-1, 1); break;
            default: break;
        }
    }
    private void Movement()
    {
        FaceDirection();
        if (Mathf.Abs(distance)<=idleRange)
        {
            rb.velocity = new Vector2(0, 0);
            idle = true;
            walk = false;
            run = false;
            
        }
        else if(Mathf.Abs(distance) <= walkRange)
        { 
            rb.velocity = new Vector2(walkSpeed * facedirection, 0);
            idle = false;
            walk = true;
            run = false;
        }
        else
        {
            rb.velocity = new Vector2(runSpeed * facedirection, 0);
            idle = false;
            walk = false;
            run = true;
        }   
    }

    private float lastTime;
    private void StretchCD()
    {
        
    }

}

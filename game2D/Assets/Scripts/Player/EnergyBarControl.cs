using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBarControl : MonoBehaviour
{
    private Animator anim;
    private GameObject content;
    private bool disappearFlag;
    private float lastUseTime = -10f;
    private float lastFullTime = -10f;
    private bool fullFlag;
    private float reCD = 1;
    private float energyCon;
    private float disappearSpeed = 3;
    public float energyMax;
    public float energyLeft;//之后改成private
    public float reSpeed;
    public bool useFlag;
    // Start is called before the first frame update
    void Start()
    {
        energyLeft = energyMax;
        content = transform.Find("Content").gameObject;
        this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        content.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if(useFlag)//体力消耗
        {
            disappearFlag = false;
            this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            content.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            useFlag = false;
            energyLeft -= energyCon;
            if (energyLeft < 0)
                energyLeft = 0;
        }
        else
        {
            if(lastUseTime + reCD <= Time.time)//当使用体力后，1s才能恢复
            {
                if (energyLeft < energyMax)//没恢复满时
                {
                    energyLeft += reSpeed * Time.deltaTime;//体力恢复
                    fullFlag = false;
                }
                else//恢复满后
                {
                    energyLeft = energyMax;
                    if(!fullFlag)//记录状态条成为满状态时的时刻
                    {
                        lastFullTime = Time.time;
                        fullFlag = true;
                    }
                    if (lastFullTime + 2 <= Time.time)
                    {
                        if(!disappearFlag)
                        {
                            disappearFlag = true;
                            anim.SetTrigger("disappear");
                        } 
                        BarDisappear();//满状态两秒后体力条消失
                    }
                }
            }
        }
        content.GetComponent<Transform>().localScale=new Vector2(1,energyLeft/energyMax);

        if(energyLeft <= energyMax/2)//体力条小于一半时颜色改变
        {
            BarColorTurn();
        }


    }
    public void EnergyConsume(float consumption)
    {
        anim.SetTrigger("stay");

        lastUseTime = Time.time;
        useFlag = true;
        energyCon = consumption;
    }

    void BarDisappear()
    {
        disappearFlag = true;
        float a = this.GetComponent<SpriteRenderer>().color.a;
        a -= disappearSpeed * Time.deltaTime;
        this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, a);
        content.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, a);

    }
    void BarColorTurn()
    {
        float halfMax = energyMax / 2;
        float x = (halfMax-energyLeft) / halfMax;
        this.GetComponent<SpriteRenderer>().color = new Color(1-x/2, 1-x, 1-x, 1);
        content.GetComponent<SpriteRenderer>().color = new Color(1-x/2, 1-x, 1-x, 1);

    }
}

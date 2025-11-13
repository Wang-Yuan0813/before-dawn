using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowSprite : MonoBehaviour
{
    private Transform player;

    private SpriteRenderer thisSprite;
    private SpriteRenderer playerSprite;
    private Color color;

    [Header("时间控制")]
    public float activeTime;
    public float activeStart;

    [Header("不透明度控制")]
    private float alpha;
    public float alphaSet;
    public float alphaMultiplier;

    private void OnEnable()//一旦被SetActive设置为ture即可调用该方法
    {
        //获取对象组件
        player = GameObject.FindGameObjectWithTag("Player").transform;
        thisSprite = GetComponent<SpriteRenderer>();
        playerSprite = player.GetComponent<SpriteRenderer>();

        alpha = alphaSet;

        thisSprite.sprite = playerSprite.sprite;
        transform.position = player.position;
        transform.rotation = player.rotation;

        activeStart = Time.time;

    }

    void Update()
    {
        alpha *= alphaMultiplier;

        color = new Color(0.5f, 0.5f, 1, alpha);//完全显示图像颜色，1表示100%
        //color = new Color(1, 1, 1, 1);

        thisSprite.color = color;

        if(Time.time>=activeStart+activeTime)
        {
            //返回对象池
            ShadowPool.instance.ReturnPool(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFX : MonoBehaviour
{
    private Animator anim;
    private int type;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        type = Random.Range(1, 4);
        anim.SetInteger("type", type);
    }
    public void AnimStart()//这里加了一点噪声，目的是让特效出现的位置略微不一样一点
    {
        float noisy_x = Random.Range(-0.8f, 0.8f);
        float noisy_y = Random.Range(-0.1f, 0.1f);
        float noisy_rotation_z = Random.Range(-20f, 20f);
        transform.SetPositionAndRotation(new Vector3(transform.position.x + noisy_x, transform.position.y + noisy_y, 0), Quaternion.Euler(0f, 0f, noisy_rotation_z));
    }
    public void AnimEnd()
    {
        Destroy(gameObject);
    }
}

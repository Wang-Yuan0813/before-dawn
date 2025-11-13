using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAnimation : MonoBehaviour
{
    private Animator anim;
    private Cat_Control cat;
    void Start()
    {
        anim = GetComponent<Animator>();
        cat = GetComponent<Cat_Control>();
    }
    void Update()
    {
        SetAnimation();
    }
    void SetAnimation()
    {
        //anim.SetFloat("distance", Mathf.Abs(cat.distance));
        anim.SetBool("idle", cat.idle);
        anim.SetBool("walk", cat.walk);
        anim.SetBool("run",cat.run);
    }

}

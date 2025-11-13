using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics_Check : MonoBehaviour
{
    public bool isGround;
    public LayerMask groundLayer;
    public float checkRadius;
    private void Update()
    {
        Check();
    }
    public void Check()
    {
        //ºÏ≤‚µÿ√Ê
        isGround = Physics2D.OverlapCircle(transform.position, checkRadius, groundLayer);
    }
}

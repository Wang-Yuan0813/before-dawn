using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicDetails : MonoBehaviour
{
    public GameObject sparks1;
    public GameObject light1;
    ParticleSystem particles1;
    void Start()
    {
        particles1 = sparks1.GetComponent<ParticleSystem>();

        InvokeRepeating("Sparks1", 2, 7);

    }

    
    void Update()
    {
        
    }
    public void Sparks1()
    {
        particles1.Play();
        light1.SetActive(true);
        light1.GetComponent<Animation>().Play();
    }



}

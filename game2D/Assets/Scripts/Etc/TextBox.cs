using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBox : MonoBehaviour
{
    [Header("文本框调整")]
    public float height;
    public float weight;
    // Start is called before the first frame update
    void Start()
    {
        height = 1;
        weight = 1;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(height, weight, 0);
    }
}

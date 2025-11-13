using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky_Control1 : MonoBehaviour
{
    [Header("∂‘œÛ∞Û∂®")]
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(Player.transform.position.x, 2);
    }
}

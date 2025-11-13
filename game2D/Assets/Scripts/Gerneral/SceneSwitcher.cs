using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public string sceneName;
    public float delay = 3f;
    public List<GameObject> gameObjectsToStayAlive;//切换后要保存的对象
    private float startTime = 0f;
    private bool isSwitching = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("1")&&!isSwitching)
        {
            startTime = Time.time;
            Debug.Log("3s后切换");
            isSwitching = true;
        }
        if (isSwitching)
        {
            float elapsedTime = Time.time - startTime;
            if (elapsedTime >= delay)
            {
                SceneManager.LoadScene(sceneName);//会自动卸载前面的场景
                isSwitching = false;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPC_Talk : MonoBehaviour
{

    [Header("对话提示")]
    public GameObject talkSign;
    [Header("对话框UI")]
    public GameObject talkUI;   
    [Header("头像")]
    public Image headImage;
    [Header("文本文件")]
    public TextAsset textFile;
    [Header("头像")]
    public Sprite head01,head02;
    [Header("文本显示速度")]
    public float textSpeed;

    public bool textFinished;
    public int index;
    //public Text textLable;
    
    
    List<string> textList = new List<string>();
    //内部参数
    void Start()
    {
        //Text textLable = talkUI.GetComponentInChildren<Text>();
        index = 0;
        textFinished = true;
        GetTextFromFile(textFile);
        talkSign.SetActive(false);
        talkUI.SetActive(false);
    }


    void Update()
    {
        if (talkSign.activeSelf && Input.GetKeyDown(KeyCode.E)) 
        {
            talkUI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E) && index != textList.Count)
            {
                if (textFinished)
                {
                    //StartCoroutine(SetTextUI());
                }
            }
            else
            {
                talkUI.SetActive(false);
                index = 0;
                return;
            }
        }



    }
    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log("Object entered trigger: " + other.gameObject.name);
        talkSign.SetActive(true);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Object exited trigger: " + other.gameObject.name);
        talkSign.SetActive(false);
        talkUI.SetActive(false);
        index = 0;
    }

    void GetTextFromFile(TextAsset file)
    {
        textList.Clear();
        index = 0;

        var lineData = file.text.Split('\n');
        foreach (var line in lineData)
        {
            textList.Add(line);
        }
    }
    /*
    IEnumerator SetTextUI()
    {
        textFinished = false;
        //textLable.text = "";

        
        switch (textList[index].Trim().ToString())
        {
            case "A":
                headImage.sprite = head01;
                index++;
                break;
            case "B":
                headImage.sprite = head02;
                index++;
                break;
        }
        for (int i = 0; i < textList[index].Length; i++)
        {
            //textLable.text += textList[index][i];

            yield return new WaitForSeconds(textSpeed);

        }
        textFinished = true;
        index++;
    }*/

}








using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using System.Collections;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private Text dialogueText;
    [SerializeField] private Button buttonPrefab;
    [SerializeField] private GameObject buttonContainer;//这个用于放置按钮
    private static DialogueManager instance;
    private bool dialogueIsPlaying;
    private Story currentStory;
    private GameObject currentNPC;
    private bool isChoosing;
    public static DialogueManager GetInstance()
    {
        if (instance == null)
        {
            Debug.LogError("DialogueManager instance is null.");
        }
        return instance;
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Debug.Log("DialogueManager instance created.");
        }
        else
        {
            Debug.LogWarning("DialogueManager instance already exists.");
        }
    }
    void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
    }
    void Update()
    {
        if (!dialogueIsPlaying)
        {
            return;
        }
        if (Input.GetButtonDown("Interact") && !isChoosing)
        {
            Debug.Log("Contiunue");
            ContinueStory();
        }
    }
    public void EnterDialoguemode(TextAsset inkJSON, GameObject npc)
    {
        currentNPC = npc;
        Debug.Log("EnterDialogueMode, talking with " + npc.name);
        GameManager.GetInstance().playerCanMove = false;

        currentStory = new Story(inkJSON.text);
        StartCoroutine(ChangeDialogueStute());
        dialoguePanel.SetActive(true);

        ContinueStory();
    }
    public void ExitDialogueMode()
    {
        Debug.Log("ExitDialogueMode");
        GameManager.GetInstance().playerCanMove = true;
        currentNPC.GetComponent<NPC_Control>().StopDialogue();

        //使用协程来避免对话结束后又迅速开启
        StartCoroutine(ChangeDialogueStute());
        dialoguePanel.SetActive(false);
 
        RemoveButtonandText();
        currentNPC = null;
    }
    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            //先清空内容
            RemoveButtonandText();
            dialogueText.text = currentStory.Continue();
            if(dialogueText.text == "")//空白时直接结束对话
            {
                ExitDialogueMode();
                return;
            }
            Debug.Log(currentNPC.name + dialogueText.text);
            if (currentStory.currentChoices.Count > 0)
            {
                CreateChoicesButton();
            }
        }
        else
        {
            ExitDialogueMode();
        }
    }
    public bool DialogueIsPlaying//用户其他脚本调取
    {
        get { return dialogueIsPlaying; }
    }
    IEnumerator ChangeDialogueStute()
    {
        yield return null;//等待一帧
        dialogueIsPlaying = !dialogueIsPlaying;
    }
    void RemoveButtonandText()
    {
        dialogueText.text = string.Empty;
        int childCount = buttonContainer.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            GameObject.Destroy(buttonContainer.transform.GetChild(i).gameObject);
        }
    }
    Button CreateChoiceView(string text)
    {
        // Creates the button from a prefab
        Button choice = Instantiate(buttonPrefab) as Button;
        choice.transform.SetParent(buttonContainer.transform, false);

        // Gets the text from the button prefab
        Text choiceText = choice.GetComponentInChildren<Text>();
        choiceText.text = text;

        return choice;
    }
    void OnClickChoiceButton(Choice choice)
    {
        isChoosing = false;
        currentStory.ChooseChoiceIndex(choice.index);
        ContinueStory();
    }
    public void CloseAllButtonChosenBlock()//关闭所有按钮的选中状态
    {
        
        for (int i = 0; i < buttonContainer.transform.childCount; i++)
        {
            GameObject button = buttonContainer.transform.GetChild(i).gameObject;
            button.transform.Find("ChosenBlock").gameObject.SetActive(false);
            Color buttonColor = button.GetComponent<Image>().color;
            button.GetComponent<Image>().color = new Color(buttonColor.r, buttonColor.g, buttonColor.b, 0.3f);
        }
    }
    private void CreateChoicesButton()
    {
        for (int i = 0; i < currentStory.currentChoices.Count; i++)
        {
            Choice choice = currentStory.currentChoices[i];
            Button button = CreateChoiceView(choice.text.Trim());
            // Tell the button what to do when we press it
            button.onClick.AddListener(delegate {
                OnClickChoiceButton(choice);
            });
        }
        CloseAllButtonChosenBlock();
        //选择第一个按钮
        EventSystem.current.SetSelectedGameObject(null);//清除选中的对象
        EventSystem.current.SetSelectedGameObject(buttonContainer.transform.GetChild(0).gameObject);
        isChoosing = true;
    }
}

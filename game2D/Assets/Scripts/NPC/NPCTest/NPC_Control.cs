using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Control : MonoBehaviour
{
    [Header("NPC 气泡最大间隔")]
    public float bubbleMaxInterval; // NPC 说话最大间隔
    [Header("NPC 气泡最小间隔（>3）")]
    public float bubbleMinInterval; // NPC 说话最小间隔
    [Header("NPC 气泡内容（.asset）")]
    public NPCSpeaking npcSpeaking;
    [Header("NPC 常态深色系数")]
    public float normalColor; // NPC 常态深色系数
    private Animator animator;
    private GameObject bubble;
    private SpriteRenderer sp;
    private ParticleSystem choosingP;
    private bool canInteracting;
    [Header("对话JSON")]
    [SerializeField] private TextAsset inkJSON;
    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        bubble = this.transform.Find("SpeakingBubble").gameObject;
        sp = gameObject.GetComponent<SpriteRenderer>();
        sp.color = new Color(normalColor, normalColor, normalColor, 1);
        choosingP = this.transform.Find("ChoosingParticle").gameObject.GetComponent<ParticleSystem>();

        canInteracting = false;
        StartCoroutine(BubbleTriggerActionCoroutine());
        Debug.Log(this.name + " amount of bubble =" + npcSpeaking.bubbleList.Count);
    }

    // Update is called once per frame
    void Update()
    {
        if(canInteracting)
        {
            if (Input.GetButtonDown("Interact")&&!DialogueManager.GetInstance().DialogueIsPlaying)
            {
                StartDialogue();
            }
        }
    }
    public void StartDialogue()
    {
        bubble.GetComponent<SpeakBubble_Control>().canBubbleActive = false;
        bubble.GetComponent<SpeakBubble_Control>().BubbleDisappear(0.0f);
        DialogueManager.GetInstance().EnterDialoguemode(inkJSON,this.gameObject);
    }
    public void StopDialogue()
    {
        bubble.GetComponent<SpeakBubble_Control>().canBubbleActive = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log(other.gameObject.name + "Enter interacting zone: " + this.name);
            // 变亮
            StartCoroutine(TurnBright());
            // 启动选中粒子效果
            canInteracting = true;
            if (choosingP != null)
            {
                choosingP.Play();
            }
        }
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log(other.gameObject.name + "Exit interacting zone: " + this.name);
            // 变暗
            StartCoroutine(TurnDark());
            // 停止选中粒子效果
            canInteracting = false;
            if (choosingP != null)
            {
                choosingP.Stop();
            }
        }
    }
    private IEnumerator BubbleTriggerActionCoroutine()//这里把该NPC的气泡内容加进去
    {
        while (true)
        {
            float bubbleInterval = Random.Range(bubbleMinInterval, bubbleMaxInterval);
            yield return new WaitForSeconds(bubbleInterval);
            if(bubble.GetComponent<SpeakBubble_Control>().canBubbleActive)
            {
                string bubbleContent = npcSpeaking.bubbleList[Random.Range(0, npcSpeaking.bubbleList.Count)];
                bubble.GetComponent<SpeakBubble_Control>().SetText(bubbleContent);
                Debug.Log("[" + this.name + "]" + "bubbleContent = " + bubbleContent);
            }
        }
    }
    private IEnumerator TurnDark()//变暗
    {
        float durationTime = 0.2f;
        float time = 0;
        Color startColor = new Color(1, 1, 1, 1);
        Color endColor = new Color(normalColor, normalColor, normalColor, 1);
        while (time < durationTime)
        {
            time += Time.deltaTime;
            sp.color = Color.Lerp(startColor, endColor, time / durationTime);
            yield return null;
        }
        sp.color = endColor;
    }
    private IEnumerator TurnBright()//变亮
    {
        float durationTime = 0.2f;
        float time = 0;
        Color startColor = new Color(normalColor, normalColor, normalColor, 1);
        Color endColor = new Color(1, 1, 1, 1);
        while (time < durationTime)
        {
            time += Time.deltaTime;
            sp.color = Color.Lerp(startColor, endColor, time / durationTime);
            yield return null;
        }
        sp.color = endColor;
    }
}

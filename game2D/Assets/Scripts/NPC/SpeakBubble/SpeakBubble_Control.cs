using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeakBubble_Control : MonoBehaviour
{
    private GameObject canvas;
    private GameObject backGround;
    private Text content;
    private SpriteRenderer arrow;
    private bool isBubbleActive;
    public bool canBubbleActive;
    void Start()
    {
        canvas = transform.Find("Canvas").gameObject;
        backGround = canvas.transform.Find("BG").gameObject;
        content = backGround.transform.Find("Text").gameObject.GetComponent<Text>();
        arrow = transform.Find("Arrow").gameObject.GetComponent<SpriteRenderer>();
        //初始化
        arrow.enabled = false;
        backGround.GetComponent<Image>().enabled = false;
        content.enabled = false;
        isBubbleActive = false;
        canBubbleActive = true;
    }
    public void SetText(string textString)
    {
        content.text = textString;
        BubbleAppear();
    }
    public void BubbleDisappear(float delay)
    {
        StartCoroutine(HideBubbleAfterDelay(delay));//延时关闭
    }
    private void BubbleAppear()
    {
        if (canBubbleActive)
        {
            isBubbleActive = true;
            arrow.enabled = true;
            backGround.GetComponent<Image>().enabled = true;
            content.enabled = true;

            AdjustBackgroundSize(content.preferredWidth, content.preferredHeight);//设置背景大小过渡动画
            BubbleDisappear(3.0f);//延时关闭
        }
    }
    private void AdjustBackgroundSize(float width, float height)
    {
        RectTransform rectTransform = backGround.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            rectTransform.sizeDelta = new Vector2(width / 10 + 5, 0); // 添加一些填充不然不好看

            StartCoroutine(AnimateBackgroundHeight(height / 10 + 3));
        }
    }
    private IEnumerator AnimateBackgroundHeight(float targetHeight)//出现
    {
        RectTransform rectTransform = backGround.GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            float duration = 0.1f; // 动画持续时间
            float elapsedTime = 0f;
            float initialHeight = rectTransform.sizeDelta.y;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                float newHeight = Mathf.Lerp(0, targetHeight, elapsedTime / duration);
                rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, newHeight);
                yield return null;
            }

            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, targetHeight);
        }
    }
    private IEnumerator HideBubbleAfterDelay(float hideDelay)
    {
        yield return new WaitForSeconds(hideDelay);
        if(isBubbleActive)
        {
            StartCoroutine(FadeOut());
            isBubbleActive = false;
        }
    }
    private IEnumerator FadeOut()//渐隐
    {
        float duration = 0.5f; // 渐隐持续时间
        float elapsedTime = 0f;

        Image backGroundImage = backGround.GetComponent<Image>();
        Color arrowColor = arrow.color;
        Color backGroundColor = backGroundImage.color;
        Color textColor = content.color;
        float backGroundAlpha = backGroundColor.a;
        float textAlpha = textColor.a;
        float arrowAlpha = arrowColor.a;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha1 = Mathf.Lerp(backGroundColor.a, 0, elapsedTime / duration);
            float alpha2 = Mathf.Lerp(textColor.a, 0, elapsedTime / duration);
            float alpha3 = Mathf.Lerp(arrowColor.a, 0, elapsedTime / duration);
            backGroundImage.color = new Color(backGroundColor.r, backGroundColor.g, backGroundColor.b, alpha1);
            content.color = new Color(textColor.r, textColor.g, textColor.b, alpha2);
            arrow.color = new Color(arrowColor.r, arrowColor.g, arrowColor.b, alpha3);
            yield return null;
        }

        backGroundImage.color = new Color(backGroundColor.r, backGroundColor.g, backGroundColor.b, backGroundAlpha);
        content.color = new Color(textColor.r, textColor.g, textColor.b, textAlpha);
        arrow.color = new Color(arrowColor.r, arrowColor.g, arrowColor.b, arrowAlpha);

        arrow.enabled = false;
        backGroundImage.enabled = false;
        content.enabled = false;
        
    }
}

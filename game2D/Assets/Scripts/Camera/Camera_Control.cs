using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Control : MonoBehaviour
{
    IEnumerator Pause(float duration)
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1;
    }
    /// <summary>
    /// 攻击顿帧时间控制
    /// </summary>
    /// <param name="duration">顿帧持续时间，秒</param>
    public void HitPause(float duration)
    {
       StartCoroutine(Pause(duration));
    }
}

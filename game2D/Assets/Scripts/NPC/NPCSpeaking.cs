using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New NPC Speaking", menuName = "NPC/New NPC Speaking")]
public class NPCSpeaking : ScriptableObject
{
    [Header("Bubble Content")]
    public List<string> bubbleList = new List<string>();
    [Header("Main Content")]
    public List<string> mainList = new List<string>();
}
    

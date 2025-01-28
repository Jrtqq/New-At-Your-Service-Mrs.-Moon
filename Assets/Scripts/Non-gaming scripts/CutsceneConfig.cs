using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cutscene")]

public class CutsceneConfig : ScriptableObject
{
    [SerializeField] public string[] Names;
    [SerializeField] public string[] Text;
    [SerializeField] public bool IsIntroNext;
}

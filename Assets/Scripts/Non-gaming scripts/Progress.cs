using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progress : MonoBehaviour
{
    [field: SerializeField] public CutsceneConfig[] Cutscenes { get; private set; }
    [field: SerializeField] public CutsceneConfig[] Intros { get; private set; }

    private int _lastLevel = 0;

    public int LastLevel
    {
        get { return _lastLevel; }
        set { _lastLevel = value; }
    }

    public static Progress Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
}

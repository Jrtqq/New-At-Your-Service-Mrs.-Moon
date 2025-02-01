using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Image;

public class Test : MonoBehaviour
{
    private Action _action;

    private void Awake()
    {
        _action -= asdf;

        _action?.Invoke();
    }

    private void asdf() { Debug.Log("asdf"); }
}

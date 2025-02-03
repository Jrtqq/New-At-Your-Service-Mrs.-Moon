using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButtons : MonoBehaviour
{
    [SerializeField] private LevelMenuFade _fade;

    private const int IntroScene = 9;

    public void GoToLevel(int index)
    {
        Progress.Instance.LastLevel = index;

        if (index == 0 || index == 4 || index == 5 || index == 6)
            _fade.NextSceneIndex = IntroScene;
        else
            _fade.NextSceneIndex = index + 1;

        _fade.IsFirstLevel = index == 0;
        _fade.gameObject.SetActive(true);
    }
}

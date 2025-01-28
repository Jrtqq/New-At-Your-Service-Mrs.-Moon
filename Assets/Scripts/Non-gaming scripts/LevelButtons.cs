using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButtons : MonoBehaviour
{
    private const int IntroScene = 8;

    public void GoToLevel(int index)
    {
        Progress.Instance.LastLevel = index;

        if (index == 0 || index == 4 || index == 5 || index == 6)
            SceneManager.LoadScene(IntroScene);
        else
            SceneManager.LoadScene(index);
    }
}

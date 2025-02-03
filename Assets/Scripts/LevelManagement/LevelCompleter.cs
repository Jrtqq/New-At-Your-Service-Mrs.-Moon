using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleter : MonoBehaviour
{
    private const int CutsceneSceneIndex = 8;
    private bool _isActive = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && _isActive)
        {
            Progress.Instance.LastLevel++;
            Restarter.ResetStageCounter();
            SceneManager.LoadScene(CutsceneSceneIndex);
            _isActive = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _levels;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenMainMenu();
        }
    }

    public void OpenMainMenu()
    {
        _mainMenu.SetActive(true);
        _levels.SetActive(false);
    }

    public void OpenLevels()
    {
        _mainMenu.SetActive(false);
        _levels.SetActive(true);
    }

    public void Exit() => Application.Quit();
}

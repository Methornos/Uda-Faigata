using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject _menu;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_menu.activeSelf) ShowMenu();
            else CloseMenu();
        }
    }

    private void ShowMenu()
    {
        _menu.SetActive(true);
        GamePause.IsPaused = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CloseMenu()
    {
        _menu.SetActive(false);
        GamePause.IsPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

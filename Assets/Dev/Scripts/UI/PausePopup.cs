using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PausePopup : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0;
    }

    public void QuitGameButton()
    {
        Application.Quit();
    }

    public void ResumeGameButton()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}

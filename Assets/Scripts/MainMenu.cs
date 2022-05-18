using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class MainMenu : MonoBehaviour
{
    
    public void PlayButton()
    {
        if(!String.IsNullOrWhiteSpace(DataManager.Instance.UserName))
        {
             SceneManager.LoadScene(1);
        }
       
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

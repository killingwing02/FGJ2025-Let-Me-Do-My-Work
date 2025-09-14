using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LobbyManager : MonoBehaviour
{
    //public Button toMainScene;
    //public Button toInstructions;
    //public Button quitGame;

    public GameObject instructions;
    public AudioSource audioSource;
    public Button back;

    // Update is called once per frame
    public void SwitchScenes()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainGame");
    }

    public void PopInstructions()
    {
        instructions.SetActive(true);
    }

    public void QuitMovie()
    {
        instructions.SetActive(false);
    }
    
    public void QuitGame()
    {
        Application.Quit();
        //EditorApplication.isPlaying = false;
    }
}

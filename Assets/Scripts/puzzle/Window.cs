using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Window : MonoBehaviour
{
    public Button quitButton;
    public GameObject windows;

    void OnButtonClick()
    {
        // ÁôÂÃ windows ª«¥ó
        windows.SetActive(false);
    }

    public void BackToLobby()
    {
        SceneManager.LoadScene("GameLobby");
    }
}

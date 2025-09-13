using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window : MonoBehaviour
{
    public Button quitButton;
    public GameObject windows;


    void OnButtonClick()
    {
        // ÁôÂÃ windows ª«¥ó
        windows.SetActive(false);
    }
}

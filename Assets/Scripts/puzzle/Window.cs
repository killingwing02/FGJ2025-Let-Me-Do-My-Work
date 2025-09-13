using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window : MonoBehaviour
{
    public Button quitButton;     // 注意型別是 Button，不是 GameObject
    public GameObject windows;    // 要隱藏的物件


    void OnButtonClick()
    {
        // 隱藏 windows 物件
        windows.SetActive(false);
    }
}

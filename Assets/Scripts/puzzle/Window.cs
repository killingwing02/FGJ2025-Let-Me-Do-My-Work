using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window : MonoBehaviour
{
    public Button quitButton;     // �`�N���O�O Button�A���O GameObject
    public GameObject windows;    // �n���ê�����


    void OnButtonClick()
    {
        // ���� windows ����
        windows.SetActive(false);
    }
}

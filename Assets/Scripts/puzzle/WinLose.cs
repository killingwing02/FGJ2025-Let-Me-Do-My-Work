using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinLose : MonoBehaviour
{
    public static WinLose Instance;
    public GameObject winUI;
    public GameObject loseUI;

    private void Awake()
    {
        Instance = this;
    }

    public void Win()
    {
        winUI.SetActive(true);
        StartCoroutine(PauseAfterDelay(2f));
    }

    public void Lose()
    {
        loseUI.SetActive(true);
        StartCoroutine(PauseAfterDelay(2f));
    }
    IEnumerator PauseAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 0;
    }

}

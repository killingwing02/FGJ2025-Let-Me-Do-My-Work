using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinLose : MonoBehaviour
{
    public static WinLose Instance;
    public GameObject winUI;
    public GameObject loseUI;
    public GameObject timerText;

    public AudioClip result;
    private AudioSource audioSource;

    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void Win()
    {
        audioSource.clip = result;
        audioSource.Play();

        winUI.SetActive(true);
        timerText.SetActive(false);
        StartCoroutine(PauseAfterDelay(2f));
        
    }

    public void Lose()
    {
        audioSource.clip = result;
        audioSource.Play();

        loseUI.SetActive(true);
        timerText.SetActive(false);
        StartCoroutine(PauseAfterDelay(2f));
    }
    IEnumerator PauseAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 0;
    }

}

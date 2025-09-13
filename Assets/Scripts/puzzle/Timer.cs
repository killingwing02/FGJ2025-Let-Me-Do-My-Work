using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]public int totalSec;
    public int sec;
    public int min;

    public TextMeshProUGUI time;
    private void Start()
    {
        totalSec = (min * 60) + sec;
        StartCoroutine(CountDown());
    }
    IEnumerator CountDown()
    {
        while (totalSec > 0)
        {
            time.text = string.Format("{0}:{1}", min.ToString("00"), sec.ToString("00"));
            //minus seconds
            yield return new WaitForSeconds(1);
            sec--;
            totalSec--;

            if(sec < 0 && min > 0)
            {
                min -= 1;
                sec = 59;
            }
            if (min == 0 && sec < 0)
            {
                sec = 0;
            }
        }
        //update timer
        time.text = string.Format("{0}:{1}", min.ToString("00"), sec.ToString("00"));

        if (PuzzleManager.Instance.snappedCount < PuzzleManager.Instance.totalPieces)
        {
            WinLose.Instance.Lose();
        }

        Time.timeScale = 0;
        
    }
}

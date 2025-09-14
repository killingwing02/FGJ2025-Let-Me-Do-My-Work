using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioClip playGame;
    public AudioClip result;
    // Start is called before the first frame update
    void Awake()
    {
        AudioSource source = GetComponent<AudioSource>();
        source.clip = playGame;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

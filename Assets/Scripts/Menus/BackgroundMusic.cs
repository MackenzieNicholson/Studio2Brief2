using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{

    public AudioSource Music;
    public AudioClip clipToPlay;
    // Start is called before the first frame update
    void Start()
    {
        Music.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ValumeValue : MonoBehaviour
{
    private AudioSource audioSrc;
    private float musicVolume = 1f;
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audioSrc.volume = musicVolume;
    }
    public void SetValume(float vol)
    {
        musicVolume = vol;
    }
}

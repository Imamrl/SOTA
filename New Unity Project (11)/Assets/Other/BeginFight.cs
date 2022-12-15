using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;
using UnityEngine.Animations;

public class BeginFight : MonoBehaviour
{
    public GameObject blockExit;
    public AudioSource fightMusic;
    public AudioSource ambientMusic;
    bool Fade;
    // Start is called before the first frame update
    void Start()
    {
        blockExit.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Fade) 
        {
            ambientMusic.volume -= Time.deltaTime*0.1f;
            if (ambientMusic.volume <= 0)
            {
                ambientMusic.Stop();
                fightMusic.Play();

            }
            if (!ambientMusic.isPlaying) 
            {
                fightMusic.volume += Time.deltaTime * 0.1f;
                if (fightMusic.volume >= 0.1)
                {
                    Fade = false;
                    fightMusic.volume = 0.1f;
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        blockExit.SetActive(true);
        Fade = true;
    }
    
}

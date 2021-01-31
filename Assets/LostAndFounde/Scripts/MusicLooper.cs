using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLooper : MonoBehaviour
{

    public AudioSource musicIntroSource;
    public AudioSource musicLoopSource;

    private bool looping = false;

    private void Start()
    {
        musicLoopSource.PlayScheduled(musicIntroSource.clip.length);
    }

    private void Update()
    {

        // if (looping = false && !musicIntroSource.isPlaying)
        // {
        //     musicLoopSource.Play();
        // }
    }





    // public AudioSource[] musicSources;
    // // public int musicBPM, timeSignature, barsLength;

    // // public float loopPointMinutes, loopPointSeconds;
    // public float loopPointSeconds;
    // private double time;
    // private int nextSource;

    // void Start()
    // {
    //     // loopPointMinutes = (barsLength * timeSignature) / musicBPM;

    //     // loopPointSeconds = loopPointMinutes * 60;

    //     time = AudioSettings.dspTime;

    //     musicSources[0].Play();
    //     nextSource = 1;
    // }

    // void Update()
    // {
    //     if (!musicSources[nextSource].isPlaying)
    //     {
    //         time = time + loopPointSeconds;

    //         musicSources[nextSource].PlayScheduled(time);

    //         nextSource = 1 - nextSource; //Switch to other AudioSource
    //     }
    // }

}

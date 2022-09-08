using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{
    public GMScript gmScript;

    public AudioClip menuSong;

    public AudioClip twinkle1;
    public AudioClip twinkle2;
    public AudioClip twinkle3;
    public AudioClip twinkle4;
    public AudioClip twinkle5;
    public AudioClip twinkle6;

    public AudioClip stopper;

    public void PlayMenuSong() {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = menuSong;
        audio.Play();
    }

    public void PlayBacking() {
        AudioClip[] songSections = new AudioClip[] { twinkle1, twinkle2, twinkle3, twinkle4, twinkle5, twinkle6 };
         // calculate song section based on current position in song
        AudioClip songSection = songSections[gmScript.startingIndex / gmScript.numBlocksInSection];    
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = songSection;
        audio.Play();
    }

    // plays short clip of silence to stop all other sounds
    public void StopSoundPlayer() {
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = stopper;
        audio.Play();
    }
}

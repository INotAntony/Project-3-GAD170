using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Item that when used changes to the next song, when out of songs turns off, when used while off, plays first song.
/// 
/// TODO; It should auto play, randomise order potentially and go to next track when used.
///     In other words, act kind of like the radio in a GTA style game.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class BoomBoxItem : InteractiveItem
{
    protected AudioSource audioSource;
    [SerializeField] AudioClip[] clips;

    int currentSongIndex =-1;

    bool isUsed = false;
    protected override void Start()
    {
        base.Start();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clips[0];
    }

    public void PlayClip()
    {
        audioSource.clip = clips[currentSongIndex];
        audioSource.Play();

    }

    void Update()
    {
        if (!audioSource.isPlaying && isUsed)
        {
            currentSongIndex++;

            if (currentSongIndex > clips.Length)
                currentSongIndex = 0;
            
            PlayClip();
        }
    }
    public override void OnUse()
    {
        base.OnUse();
        isUsed = true;

        if (!audioSource.isPlaying && currentSongIndex < clips.Length - 1)
        {
            currentSongIndex++;
            PlayClip();
        }
        else
        {
            audioSource.Stop();
            currentSongIndex = 0;
            isUsed = false;
        }
    }
}

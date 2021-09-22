using System;
using TeddyToolKit.Core;
using UnityEngine;

/// <summary>
/// in charge of handling the audio in the game
/// </summary>
public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField] private AudioSource bgm;
    [SerializeField] private AudioSource matchFX;
    [SerializeField] private AudioSource selectFX;
    [SerializeField] private AudioSource gameOverFX;
    
    public void playMusic(bool isPlay)
    {
        if (isPlay)
        {
            bgm.Play();
        }
        else
        {
            bgm.Stop();
        }
    }
    
    public void playSelectFx(bool isPlay)
    {
        if (isPlay)
        {
            selectFX.Play();
        }
        else
        {
            selectFX.Stop();
        }
    }
    
    public void playMatchFx(bool isPlay)
    {
        if (isPlay)
        {
            matchFX.Play();
        }
        else
        {
            matchFX.Stop();
        }
    }
    
    public void playGameOver(bool isPlay)
    {
        if (isPlay)
        {
            gameOverFX.Play();
        }
        else
        {
            gameOverFX.Stop();
        }
    }
    
}

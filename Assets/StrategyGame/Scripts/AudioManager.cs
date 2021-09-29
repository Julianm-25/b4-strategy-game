using System;
using TeddyToolKit.Core;
using UnityEngine;

/// <summary>
/// in charge of handling the audio in the game
/// </summary>
public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField] private AudioSource bgm;
    [SerializeField] private AudioSource attackFX;
    [SerializeField] private AudioSource moveFX;
    [SerializeField] private AudioSource selectFX;
    [SerializeField] private AudioSource endturnFX;
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
    
    public void PlayAttackFx(bool isPlay)
    {
        if (isPlay)
        {
            attackFX.Play();
        }
        else
        {
            attackFX.Stop();
        }
    }
    
    public void PlayMoveFx(bool isPlay)
    {
        if (isPlay)
        {
            moveFX.Play();
        }
        else
        {
            moveFX.Stop();
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
    
    public void playEndTurn(bool isPlay)
    {
        if (isPlay)
        {
            endturnFX.Play();
        }
        else
        {
            endturnFX.Stop();
        }
    }
}

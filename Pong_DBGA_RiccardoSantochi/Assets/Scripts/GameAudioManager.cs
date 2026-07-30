using UnityEngine;

public class GameAudioManager : MonoBehaviour
{
    // AudioSource usato per riprodurre tutti gli effetti sonori
    public AudioSource SoundEffect;

    public AudioClip scoreSound;
    public AudioClip wallSound;
    public AudioClip winSound;
    public AudioClip paddleSound;


    //Suono del punto
    public void PlayScoreSound()
    {
        SoundEffect.PlayOneShot(scoreSound);
    }

    // Suono del muro
    public void PlayWallSound()
    {
        SoundEffect.PlayOneShot(wallSound);
    }

    // Suono della vittoria
    public void PlayWinSound()
    {
        SoundEffect.PlayOneShot(winSound);
    }

    // Suono della paddle
    public void PlayPaddleSound()
    {
        SoundEffect.PlayOneShot(paddleSound);
    }
}

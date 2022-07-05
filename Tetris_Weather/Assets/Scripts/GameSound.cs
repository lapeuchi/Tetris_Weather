using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSound : MonoBehaviour
{
    public static GameSound sound = null; 
    [SerializeField] public AudioSource bgm;
    [SerializeField] public AudioSource fx;
    [Header("AudioClips")]
    [SerializeField] AudioClip waveClear_Fx;
    [SerializeField] AudioClip rain_Fx;
    [SerializeField] AudioClip wind_Fx;
    [SerializeField] AudioClip static_Fx;
    [SerializeField] AudioClip spawn_Fx;
    [SerializeField] AudioClip game_bgm;
    [SerializeField] AudioClip button_Fx;
    
    void Awake()
    {
        if(sound == null) sound = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    public void PlayBGM()
    {
        Debug.Log("BGM PLAY");
        bgm.clip = game_bgm;
        bgm.Play();
    }
    public void StopBGM()
    {
        Debug.Log("BGM STOP");
        bgm.Stop();
    }
    public void PlayWaveClear()
    {
        fx.PlayOneShot(waveClear_Fx);
    }
    public void PlayRain()
    {
        fx.PlayOneShot(rain_Fx);
    }
    public void PlayWind()
    {
        fx.PlayOneShot(wind_Fx);
    }
    public void PlayButton()
    {
        fx.PlayOneShot(button_Fx);
    }
    public void PlayStatic()
    {
        fx.PlayOneShot(static_Fx);
    }
    public void PlaySpawn()
    {
        fx.PlayOneShot(spawn_Fx);
    }
      
}

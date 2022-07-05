using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Option : MonoBehaviour
{
    [SerializeField] Slider bgm_Slider;
    [SerializeField] Slider effect_Slider;
    [SerializeField] GameObject ToLobby_Button;
    void Awake()
    {
        if(PlayerPrefs.HasKey("bgm_Slider") == true)
        {
            Debug.Log("GetVolume");
            GetVolume();
        }
        else
        {
            bgm_Slider.value = 0.6f;
            effect_Slider.value = 1;
        }
        
        if(GameManager.instance.curScene == Define.Scene.Game)
        {
            ToLobby_Button.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if(gameObject.active == false) return;
        GameSound.sound.bgm.volume = bgm_Slider.value;
        GameSound.sound.fx.volume = effect_Slider.value;
    }
    
    public void ExitOption()
    {
        GameSound.sound.PlayButton();
        SetVolume();
        gameObject.SetActive(false);
        if(GameManager.instance.curScene == Define.Scene.Game)
        {
            Time.timeScale = 1;
            PlayerController.SetJoyStick(true);
        }
    }

    public void ToLobby()
    {
        if(GameManager.instance.curScene == Define.Scene.Game)
        {
            Time.timeScale = 1;
            SetVolume();
        }
        StopAllCoroutines();
        Game.isStart = false;
        GameSound.sound.PlayButton();
        SceneManager.LoadScene("LobbyScene");
    }
    public void GetVolume()
    {
        bgm_Slider.value = PlayerPrefs.GetFloat("bgm_Slider");
        effect_Slider.value = PlayerPrefs.GetFloat("effect_Slider");
    }
    public void SetVolume()
    {
        PlayerPrefs.SetFloat("bgm_Slider", bgm_Slider.value);
        PlayerPrefs.SetFloat("effect_Slider",effect_Slider.value);
    }
    
}

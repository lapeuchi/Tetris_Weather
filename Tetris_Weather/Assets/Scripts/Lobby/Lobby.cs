using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    [SerializeField] GameObject mainLobby;

    [SerializeField] GameObject gameMode;

    [SerializeField] GameObject singleMode;
    [SerializeField] Text difficulty_Text;
    int difficulty = 0;
    [SerializeField] GameObject option;

    void Awake()
    {
        
    }

    void Start()
    {
        if(GameManager.instance.curScene == Define.Scene.Result
         && GameManager.instance.curGameMode == Define.GameMode.Single)
        {  
            mainLobby.SetActive(false);
            gameMode.SetActive(false);
            option.SetActive(false);
            singleMode.SetActive(true);      
        }
        else
        {
            mainLobby.SetActive(true);
            gameMode.SetActive(false);
            option.SetActive(false);
            singleMode.SetActive(false);
        }
        GameManager.instance.curScene = Define.Scene.Lobby;
        GameManager.instance.curGameMode = Define.GameMode.None;
        GameSound.sound.StopBGM();
    }

    void Update()
    {
        
    }

    public void ClickedPlay()
    {
        option.SetActive(false);
        mainLobby.SetActive(false);
        gameMode.SetActive(true);
        GameSound.sound.PlayButton();
    }

    public void ClickedOption()
    {
        option.GetComponent<Option>().GetVolume();
        option.SetActive(true);
        GameSound.sound.PlayButton();
    }

    public void ClickedExit()
    {
        GameSound.sound.PlayButton();
        Application.Quit();
    }

    public void ClickedToMain()
    {
        gameMode.SetActive(false);
        mainLobby.SetActive(true);
        option.GetComponent<Option>().SetVolume();
        option.SetActive(false);
        GameSound.sound.PlayButton();
    }

    public void ClickedSingle()
    {
        option.SetActive(false);
        gameMode.SetActive(false);
        singleMode.SetActive(true);
        GameSound.sound.PlayButton();
    }
    public void ClickedDifficulty()
    {
        if(difficulty == 0)
        {
            difficulty = 1;
            difficulty_Text.text = "Hard";
            return;
        }
        else if(difficulty == 1)
        {
            difficulty = 0;
            difficulty_Text.text = "Normal";
            return;
        }
        GameSound.sound.PlayButton();
    }
    public void ClickedGameStart()
    {
        GameManager.instance.difficulty = this.difficulty;
        SceneManager.LoadScene("GameScene");
        GameSound.sound.PlayButton();
    }
    public void ClickedToMode()
    {
        option.SetActive(false);
        singleMode.SetActive(false);
        gameMode.SetActive(true);
        GameSound.sound.PlayButton();
    }

    //multi
    public void ClickedMulti()
    {
        option.SetActive(false);
        GameSound.sound.PlayButton();
    }
}

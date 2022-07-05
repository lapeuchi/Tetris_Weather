using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField] private Text cnt_text;
    [SerializeField] private Text playTime_Text;
    public static TetrominoManager tetrominoManager;
    public static PlayerController playerController; 
    [SerializeField] Canvas GameOverUI;
    [SerializeField] GameObject clearEffect;
    WeatherManager weatherManager;

    public List<GameObject> Trash = new List<GameObject>();
    public static bool isRemoving;
    public static int curWave;

    bool isGameOver;
    public static bool isStart = false;
    public static float playTime;
    [SerializeField] GameObject option;
    [SerializeField] Transform ground;

    void Awake()
    {
        if(GameManager.instance.difficulty == 1)
        {
            ground.localScale = new Vector3(ground.lossyScale.x/2f, 1f, 1f);
        }
        option.SetActive(false);
        weatherManager = gameObject.GetComponent<WeatherManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        tetrominoManager = GameObject.Find("Player").GetComponent<TetrominoManager>();
    }

    void Start()
    {
        playTime = 0;
        GameManager.instance.curScene = Define.Scene.Game;
        GameManager.instance.curGameMode = Define.GameMode.Single;
        StartCoroutine(Count(3));
    }

    void Update()
    {
        if(isStart == false) return;
        TimeCalculate();

        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Menu))
        {
            ActiveOption();
        }
    }
    public void ActiveOption()
    {
        if(option.active)
        {
            option.SetActive(false); 
            Time.timeScale = 1;
            if(SystemInfo.deviceType != DeviceType.Desktop)
                PlayerController.SetJoyStick(true);
            return;
        }
        else
        {
            Time.timeScale = 0;
            option.SetActive(true);
            PlayerController.SetJoyStick(false);
            option.GetComponent<Option>().GetVolume();
            return;
        }
    }
    
    IEnumerator Count(int CountTime)
    {
        for(int i = CountTime; i > 0; i--)
        {
            cnt_text.text = $"{i}";
            yield return new WaitForSeconds(1f);
        }
        cnt_text.text = $"Start!";
        GameSound.sound.PlayBGM();
        yield return new WaitForSeconds(1f);
        cnt_text.text = "";
        isStart = true;
        curWave = 1;
        yield return new WaitUntil(() => curWave * 60 <= playTime);
        StartCoroutine(weatherManager.Instance());
        StartCoroutine(ChangeWave());
    }

    IEnumerator ChangeWave()
    {
        Debug.Log($"Wave {curWave} Clear");
        isRemoving = true;
        foreach(GameObject obj in Trash)
        {
            Instantiate(clearEffect, obj.transform.position, Quaternion.identity);
            GameSound.sound.PlayWaveClear();
            Destroy(obj);
            yield return new WaitForSeconds(0.05f);
        }
        Trash.Clear();
        isRemoving = false;
        ++curWave;
        yield return new WaitUntil(() => curWave * 60 <= playTime);
        StartCoroutine(ChangeWave());
    }

    int m = 0, s = 0;
    void TimeCalculate()
    {
        playTime += Time.deltaTime;
        m = (int)playTime / 60;
        s = (int)playTime % 60;
        
        if(m > 9 && s > 9)
            playTime_Text.text = $"{m} : {s}";
        else
        {
            if (m < 10 && s < 10)
                playTime_Text.text = $"0{m} : 0{s}";
            else if (s < 10)
                playTime_Text.text = $"{m} : 0{s}";
            else if (m < 10)
                playTime_Text.text = $"0{m} : {s}";
        }   
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Tetromino") && isStart == true)
        {
            isStart = false;           
            GameOverUI.gameObject.SetActive(true);
            Debug.Log("GameOver");
        }
    }

}

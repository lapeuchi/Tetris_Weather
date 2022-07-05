using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Result : MonoBehaviour
{
    [SerializeField] Text curScore_Text;
    float curScore;
    [SerializeField] Text bestScore_Text;
    float bestScore;
     
    public void Start()
    {
        curScore = Game.playTime;
        bestScore = PlayerPrefs.GetFloat("bestScore");
        curScore_Text.text = $"Score: {TimeCalculate(ref curScore)}";
        bestScore_Text.text = $"Best: {TimeCalculate(ref bestScore)}";

        if(curScore > bestScore)
        {
            PlayerPrefs.SetFloat("bestScore", curScore);
        }
        GameManager.instance.curScene = Define.Scene.Result;
    }

    string TimeCalculate(ref float time)
    {
        int m = 0, s = 0;
        string returnValue = null;
        m = (int)time / 60;
        s = (int)time % 60;
        
        if(m > 9 && s > 9)
            returnValue = $"{m} : {s}";
        else
        {
            if (m < 10 && s < 10)
                returnValue = $"0{m} : 0{s}";
            else if (s < 10)
                returnValue = $"{m} : 0{s}";
            else if (m < 10)
                returnValue = $"0{m} : {s}";
        }
        return returnValue;
    }

    public void ClickedReturn()
    {
        SceneManager.LoadScene("LobbyScene");
    }
}

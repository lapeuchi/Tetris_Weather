using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{   
    Define.Weather weather;
    public int STACK;
    public float weatherSpawnTime;
    public float spawnDelay;
    bool isSun;
    Transform spwanPos;
    [SerializeField] GameObject rainPrefab;
    [SerializeField] GameObject wind_L;
    [SerializeField] GameObject wind_R;
    public static int windDir;
    
    void Awake()
    {
        windDir = 0;
        if(rainPrefab == null) rainPrefab = Resources.Load<GameObject>("Prefabs/RainObj");
        wind_L.SetActive(false);
        wind_R.SetActive(false);
        spwanPos = GameObject.Find("WeatherPos").transform;
        weatherSpawnTime = 11;
        spawnDelay = 1f;
    }

    void Start()
    {
        //StartCoroutine(SpawnWeather());
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Q)) WeatherClear();
    }

    public IEnumerator Instance()
    {   
        while(true)
        {
            Debug.Log("RAINTIME");
            for(int i = 0; i < Game.curWave + 2; i++)
            {
                for(int j = 0; j < 5; j++)
                {
                    Instantiate(rainPrefab, new Vector2(Random.Range(-6.5f, 6.5f), spwanPos.position.y), Quaternion.identity);
                }   
                yield return new WaitForSeconds(1);
            }
            yield return new WaitForSeconds(2);

            Debug.Log("WindTime");
            for(int i = 0; i < 3; i++)
            {
                int j = Random.Range(0, 100);
                if(j < 50)
                {
                    wind_R.SetActive(true);
                    wind_L.SetActive(false);
                    yield return new WaitForSeconds(1f);
                    windDir = -1;
                }
                else
                {
                    wind_R.SetActive(false);
                    wind_L.SetActive(true);
                    yield return new WaitForSeconds(1f);
                    windDir = 1;
                }
                GameSound.sound.PlayWind();
                yield return new WaitForSeconds(Random.Range(3f, 5f));
                wind_R.SetActive(false);
                wind_L.SetActive(false);
                yield return new WaitForSeconds(0.6f);
                windDir = 0;
            }
            yield return new WaitForSeconds(weatherSpawnTime);
        }
    }
    void Lightning()
    {
        
    }
    
    void WeatherClear()
    {
        isSun = true;
        // for(int i = 0; i < weatherStack.Length; i++)
        // {
        //     weatherStack[i] = 0;
        // }
    } 
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyManager : MonoBehaviour
{
    [SerializeField] Transform cloudsTransform;
    [SerializeField] GameObject[] clouds;
    [SerializeField] Vector3 cloudOrigin;
    float timer;
    //5.5 ~ 17
    void Start()
    {
        
    }

    void Update()
    {
        cloudsTransform.position += Vector3.left * Time.deltaTime * 2f;
        timer += Time.deltaTime;
        if(timer > 4.5)
            SpawnCloud();
    }    

    void SpawnCloud()
    {
        timer = 0;
        GameObject newCloud = Instantiate(clouds[Random.Range(0,2)], new Vector3(cloudOrigin.x, Random.Range(5.5f, 17f), cloudOrigin.z), Quaternion.Euler(60,60,60), cloudsTransform);
        Destroy(newCloud, 30f);
    }
}

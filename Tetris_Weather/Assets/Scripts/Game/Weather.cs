using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weather : MonoBehaviour
{
    public Define.Weather weather;
    public ParticleSystem destroyEffect;
    Rigidbody2D rigid;
    void Awake()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        if(weather == Define.Weather.rain)
            rigid.velocity = new Vector2(Random.Range(0, 0.5f), -1 * 18);
    }
    
    private void OnDestroy()
    {
        if(weather == Define.Weather.rain)
        {
            GameSound.sound.PlayRain();
            destroyEffect.transform.parent = null;
            destroyEffect.Play();
        }
    }

    void FixedUpdate()
    {
        
    }

    void Update()
    {
        if (weather == Define.Weather.lightning)
        {
            return;
        } 
    }

   
    void Rain()
    {
        
    }   

}

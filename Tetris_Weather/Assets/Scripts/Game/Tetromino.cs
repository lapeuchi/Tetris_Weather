using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetromino : MonoBehaviour
{
    Rigidbody2D rigid;
    Collider2D coll;
    public bool isActive;
    SpriteRenderer[] sprite;
    
    public Game game;
    public ParticleSystem staticParticle;
    void Awake()
    {
        game = GameObject.FindWithTag("GameController").GetComponent<Game>();
        sprite = gameObject.GetComponentsInChildren<SpriteRenderer>();
        Color c = new Color(Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f));
        
        foreach(SpriteRenderer sprite in sprite)
        {
            sprite.color = c;
        }
        coll = gameObject.GetComponent<Collider2D>();
        rigid = gameObject.GetComponent<Rigidbody2D>();
        transform.localScale = new Vector3(0.75f,0.75f,0.75f);
        rigid.isKinematic = true;
        coll.isTrigger = true;
        SetActive(true);
    }
    
    void Update()
    {

    }

    void SetActive(bool activeParam)
    {
        if(!activeParam)
        {
            isActive = false;
            gameObject.isStatic = true;
            rigid.freezeRotation= false;
            StartCoroutine(SetStatic());
            return;
        }
        else
        {
            isActive = true;
            Debug.Log("Active");
            return;
        }
        
    }
    
    IEnumerator HitEvent(Define.Weather objWeather)
    {
        switch(objWeather)
        {
            case Define.Weather.rain:
                rigid.AddForce(new Vector2(Random.Range(-3f, 3f), -7), ForceMode2D.Impulse);

            break;

            case Define.Weather.lightning:
            
            break;
        }
        yield return null;
    }

    IEnumerator SetStatic()
    {
        float timer = 0f;
        while(timer < 5f)
        {
            if(rigid.velocity.y < 0.01f && rigid.velocity.x < 0.01)
            {
                timer += Time.deltaTime;
            }
            else timer = 0;

            yield return null;
        }
        rigid.bodyType = RigidbodyType2D.Static;
        staticParticle.Play();
        GameSound.sound.PlayStatic();
        Debug.Log("SetStatic");
        
    }  

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if((other.gameObject.CompareTag("Landable") 
            || other.gameObject.CompareTag("Tetromino")) && isActive)
        {
            SetActive(false);
            StartCoroutine(InputTrash());
        }
    }

    IEnumerator InputTrash()
    {
        yield return new WaitUntil(() => Game.isRemoving == false);
        if(game.Trash.Count == 0)
        {
            game.Trash.Add(gameObject);
            yield break;
        }

        if(game.Trash[game.Trash.Count-1] != gameObject)
        game.Trash.Add(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Weather"))
        {   
            if(TetrominoManager.curTetrominoObj == gameObject )
                StartCoroutine(HitEvent(other.gameObject.GetComponent<Weather>().weather));
            if(TetrominoManager.curTetrominoObj == gameObject || isActive == false)
                Destroy(other.gameObject);
        }
    }

    private void OnParticleCollision(GameObject other) 
    {
        if(other.gameObject.CompareTag("Weather") && TetrominoManager.curTetrominoObj == gameObject)
        {
            Debug.Log(other.gameObject.name);
            StartCoroutine(HitEvent(Define.Weather.lightning));
        }
    }
}
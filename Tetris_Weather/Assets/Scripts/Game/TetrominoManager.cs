using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrominoManager : MonoBehaviour
{
    public GameObject[] tetrominoPrefabs;
    public static GameObject curTetrominoObj;
    public static Tetromino curTetriminoComponent;
    PlayerController playerComponent;

    public Queue<GameObject> tetriminoQueue = new Queue<GameObject>();

    [SerializeField] private Transform[] ListPos = new Transform[5];
    [SerializeField] private GameObject[] Blocks = new GameObject[5];
    Transform spawnPoint;
    
    void Awake()
    {
        spawnPoint = GameObject.Find("SpawnPoint").transform;
        playerComponent = gameObject.GetComponent<PlayerController>();
        for(int i = 0; i < 7; i++)
        {
            int j = Random.Range(0, tetrominoPrefabs.Length-1);
            int k = Random.Range(0, tetrominoPrefabs.Length-1);
            GameObject tmp = tetrominoPrefabs[j];
            tetrominoPrefabs[j] = tetrominoPrefabs[k];
            tetrominoPrefabs[k] = tmp;
        }

        foreach (GameObject tetromino in tetrominoPrefabs)
        {
            tetriminoQueue.Enqueue(tetromino);
        }
        
        for(int i = tetriminoQueue.Count-3; i >= 0; i--)
        {
            Blocks[i] = Instantiate(tetriminoQueue.Peek(), ListPos[i].position, Quaternion.identity);
            tetriminoQueue.Enqueue(tetriminoQueue.Peek());
            tetriminoQueue.Dequeue();
        }
        curTetrominoObj = Blocks[4];
    }

    void Start()
    {
        StartCoroutine(TetriminoControll());
    }

    void Update()
    {
        
    }

    IEnumerator TetriminoControll()
    {
        if(Game.isStart == false)
            yield return new WaitUntil(()=> Game.isStart == true);
        
        Debug.Log("Set");
        SetTetromino();
        InstantiateTetromino();

        // 블록 착지
        yield return new WaitUntil(()=> curTetriminoComponent.isActive == false);
        
        curTetrominoObj = Blocks[Blocks.Length-1];
        
        yield return new WaitForSeconds(0.1f);

        yield return null;
        StartCoroutine(TetriminoControll());
    }

    void SetTetromino()
    {
        curTetrominoObj.transform.localScale = new Vector3(1f, 1f,1f);
        curTetrominoObj.transform.position = spawnPoint.position;
        curTetrominoObj.GetComponent<Rigidbody2D>().isKinematic = false;
        curTetrominoObj.GetComponent<Collider2D>().isTrigger = false;
        curTetriminoComponent = curTetrominoObj.GetComponent<Tetromino>();

        for(int i = tetriminoQueue.Count-4; i >= 0; i--)
        {
            Blocks[i+1] = Blocks[i];
            Blocks[i].transform.position = ListPos[i+1].position;
        }
        playerComponent.SetTetromino();
        
    }

    void InstantiateTetromino()
    {
        Blocks[0] = Instantiate(tetriminoQueue.Peek());
        Blocks[0].transform.rotation = Quaternion.Euler(0,0,Random.Range(1, 4) * 90f);
        Blocks[0].transform.position = ListPos[0].position;
        tetriminoQueue.Enqueue(tetriminoQueue.Peek());
        tetriminoQueue.Dequeue();
    }
}

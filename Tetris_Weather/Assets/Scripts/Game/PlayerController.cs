using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public static Joystick joystick_L;
    [SerializeField] public static Joystick joystick_R;

    GameObject curTetromino;
    Rigidbody2D rigid;
    
    float verticalForce = -9f;
    float sprintHorizontalForce = 10;
    float curHorizontalForce = 7;
    float horizontalForceFactor = 5f;

    public bool isStun;

    [SerializeField] private float hAxis;
    [SerializeField] private float vAxis;
    private void Awake() 
    {
        joystick_L = GameObject.Find("Stick_L").GetComponent<Joystick>();
        joystick_R = GameObject.Find("Stick_R").GetComponent<Joystick>();
        if(SystemInfo.deviceType == DeviceType.Desktop)
        {    
            SetJoyStick(false);
        }
        else SetJoyStick(true);
    }

    void Update()
    {
        if(isStun == false)
        {
            InputKey();
        }
    }

    private void FixedUpdate() 
    {   
        if(Game.isStart == false)
            return;

        if(rigid != null)
        {
            rigid.velocity = new Vector2(curHorizontalForce * hAxis + (WeatherManager.windDir * 2f), rigid.velocity.y);
            Vector3 eulerAngleVelocity = new Vector3(0,0, vAxis);
            Quaternion deltaRot = Quaternion.Euler(eulerAngleVelocity * Time.fixedDeltaTime);            
            rigid.rotation += vAxis * 3f;
        }
        
    }

    void InputKey()
    {
        hAxis = Input.GetAxisRaw("Horizontal");
        vAxis = Input.GetAxisRaw("Vertical");
        if(SystemInfo.deviceType == DeviceType.Handheld)
        {
            hAxis = joystick_L.Horizontal;
            vAxis = -joystick_R.Horizontal;
        }
       
        
        
        if(isStun)
        {
            hAxis = 0; vAxis = 0;
        }

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            curHorizontalForce = horizontalForceFactor;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            curHorizontalForce = horizontalForceFactor;
        }
    }

    public void SetTetromino()
    {
        if(rigid != null)
            rigid.velocity = Vector2.zero;
        curTetromino = TetrominoManager.curTetrominoObj;
        rigid = curTetromino.GetComponentInChildren<Rigidbody2D>();
        GameSound.sound.PlaySpawn();
    }
    
    public static void SetJoyStick(bool set)
    {
        joystick_L.gameObject.SetActive(set);
        joystick_R.gameObject.SetActive(set);    
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //声明玩家的移动速度
    public float moveSpeed = 5f;
    //声明玩家的刚体组件
    private Rigidbody2D playerRigidbody;
    //声明玩家的水平输入
    private float horizontalInput;
    //声明玩家的垂直输入
    private float verticalInput;
    //声明玩家的animator控制器
    private Animator playerAnimator;

    void Start()
    {
        //获取玩家的刚体组件
        playerRigidbody = GetComponent<Rigidbody2D>();
        //获取玩家的animator控制器
        playerAnimator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //获取玩家的水平和垂直输入
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        //处理玩家的动画逻辑
        if (horizontalInput == 0 && verticalInput == 0)
        {
            playerAnimator.SetBool("isIdle", true);
            playerAnimator.SetBool("isMoving", false);
        }
        else
        {
            playerAnimator.SetBool("isIdle", false);
            playerAnimator.SetBool("isMoving", true);
        }
      
    }
    void FixedUpdate()
    {
        //处理玩家的移动逻辑
        playerRigidbody.velocity = new Vector2(horizontalInput * moveSpeed, verticalInput * moveSpeed);
    }
}

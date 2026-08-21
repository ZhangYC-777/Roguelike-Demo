using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//声明玩家状态枚举
enum PlayerState
{
    Locomotion, //常规移动状态
    Dodge //翻滚
}
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
    //声明玩家的状态
    private PlayerState currentState = PlayerState.Locomotion;
    //声明玩家的翻滚方向
    private Vector2 dodgeDirection;
    //声明玩家的翻滚速度
    public float dodgeSpeed = 10f;
    //声明玩家翻滚时间
    public float dodgeDuration = 0.2f;
    //声明玩家的翻滚计时器
    private float dodgeTimer;
    //声明玩家的翻滚冷却时间
    public float dodgeCooldown = 1f;
    //声明玩家的翻滚冷却计时器
    private float dodgeCooldownTimer;

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
        if (currentState == PlayerState.Locomotion)
        {
            if (horizontalInput != 0 || verticalInput != 0)
            {
                playerAnimator.SetBool("isMoving", true);
                playerAnimator.SetBool("isIdle", false);
            }
            else
            {
                playerAnimator.SetBool("isMoving", false);
                playerAnimator.SetBool("isIdle", true);
            }
        }
        //处理玩家的翻滚冷却逻辑
        if (dodgeCooldownTimer > 0)
        {
            dodgeCooldownTimer -= Time.deltaTime;
        }
        //处理玩家的状态切换
        if(Input.GetKeyDown(KeyCode.Space) )
        {
           if(currentState == PlayerState.Locomotion && dodgeCooldownTimer <= 0 && (horizontalInput != 0 || verticalInput != 0))
            {
                //获取玩家的翻滚方向并单位化
                dodgeDirection = new Vector2(horizontalInput, verticalInput).normalized;
                ChangeState(PlayerState.Dodge);
            }
        }
      
    }
    void FixedUpdate()
    {
        //根据玩家状态处理移动逻辑
        switch (currentState)
        {
            case PlayerState.Locomotion:
                {
                    //处理玩家的移动逻辑
                    playerRigidbody.velocity = new Vector2(horizontalInput * moveSpeed, verticalInput * moveSpeed);
                }
            break;
            case PlayerState.Dodge:
                {
                    //处理玩家的翻滚逻辑
                    playerRigidbody.velocity = dodgeDirection * dodgeSpeed;
                    //处理计时器逻辑
                    dodgeTimer -= Time.fixedDeltaTime;
                    if (dodgeTimer <= 0)
                    {
                        ChangeState(PlayerState.Locomotion);
                    }
                }
            break;


        }
    }
    //声明一个方法改变玩家状态
    private void ChangeState(PlayerState newState)
    {
        currentState = newState;
         Debug.Log("当前玩家状态：" + currentState);
         //如果玩家状态为翻滚则启动计时器
         if (newState == PlayerState.Dodge)
         {
             dodgeTimer = dodgeDuration;
             dodgeCooldownTimer = dodgeCooldown;
             playerAnimator.SetBool("isMoving", false);
             playerAnimator.SetBool("isIdle", false);
             //播放翻滚动画
             if (dodgeDirection.x > 0)
                 playerAnimator.SetBool("rollRight", true);
            else if (dodgeDirection.x < 0)
                 playerAnimator.SetBool("rollLeft", true);
            else if (dodgeDirection.y > 0)
                 playerAnimator.SetBool("rollUp", true);
            else if (dodgeDirection.y < 0)
                 playerAnimator.SetBool("rollDown", true);
        
         }
         if (newState == PlayerState.Locomotion)
         {
             //停止翻滚动画
             playerAnimator.SetBool("rollRight", false);
             playerAnimator.SetBool("rollLeft", false);
             playerAnimator.SetBool("rollUp", false);
             playerAnimator.SetBool("rollDown", false);
         }
    }
}

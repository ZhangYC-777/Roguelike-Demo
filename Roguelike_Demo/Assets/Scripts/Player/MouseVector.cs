using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseVector : MonoBehaviour
{
    //声明鼠标的位置
    private Vector2 mousePosition;
    //声明玩家的位置
    private Vector2 playerPosition;
    //声明玩家的朝向
    private Vector2 playerDirection;
    //声明玩家的旋转角度
    private float playerRotationAngle;
    //声明玩家的动画控制器
    private Animator playerAnimator;
    void Start()
    {
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();


    }
        

    // Update is called once per frame
    void Update()
    {
        //获取玩家的位置
        playerPosition = playerAnimator.transform.position;

    
        //获取鼠标的位置
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //计算玩家的朝向
        playerDirection = (mousePosition - playerPosition);
        ChangePlayerRotation();
       ChangePlayerDirection();
    }
    //定义一个函数去计算人物旋转角度
    public void ChangePlayerRotation()
    {
        playerRotationAngle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, playerRotationAngle);
    }
    //定义一个函数去改变人物的朝向
    public void ChangePlayerDirection()
    {
        if(playerDirection.y > 0)
        {
            if(playerDirection.x < 0.1f && playerDirection.x > -0.1f)
            {
                playerAnimator.SetBool("aimUp", true);
                playerAnimator.SetBool("aimDown", false);
                playerAnimator.SetBool("aimLeft", false);
                playerAnimator.SetBool("aimRight", false);
                playerAnimator.SetBool("aimUpRight", false);
                playerAnimator.SetBool("aimUpLeft", false);
            }
            else if(playerDirection.x > 0.1f)
            {
                playerAnimator.SetBool("aimUpRight", true);
                playerAnimator.SetBool("aimDown", false);
                playerAnimator.SetBool("aimLeft", false);
                playerAnimator.SetBool("aimRight", false);
                playerAnimator.SetBool("aimUp", false);
                playerAnimator.SetBool("aimUpLeft", false);
            }
            else if(playerDirection.x < -0.1f)
            {
                playerAnimator.SetBool("aimUpLeft", true);
                playerAnimator.SetBool("aimDown", false);
                playerAnimator.SetBool("aimLeft", false);
                playerAnimator.SetBool("aimRight", false);
                playerAnimator.SetBool("aimUp", false);
                playerAnimator.SetBool("aimUpRight", false);
            }
        }
        else if(playerDirection.y < 0)
        {
            if(playerDirection.x < 0.1f && playerDirection.x > -0.1f)
            {
                playerAnimator.SetBool("aimDown", true);
                playerAnimator.SetBool("aimUp", false);
                playerAnimator.SetBool("aimLeft", false);
                playerAnimator.SetBool("aimRight", false);
                playerAnimator.SetBool("aimUpRight", false);
                playerAnimator.SetBool("aimUpLeft", false);
            }
            else if(playerDirection.x > 0.1f)
            {
                playerAnimator.SetBool("aimRight", true);
                playerAnimator.SetBool("aimDown", false);
                playerAnimator.SetBool("aimLeft", false);
                playerAnimator.SetBool("aimUp", false);
                playerAnimator.SetBool("aimUpRight", false);
                playerAnimator.SetBool("aimUpLeft", false);
            }
            else if(playerDirection.x < -0.1f)
            {
                playerAnimator.SetBool("aimLeft", true);
                playerAnimator.SetBool("aimDown", false);
                playerAnimator.SetBool("aimUp", false);
                playerAnimator.SetBool("aimRight", false);
                playerAnimator.SetBool("aimUpRight", false);
                playerAnimator.SetBool("aimUpLeft", false);
            }
        }
    }
}

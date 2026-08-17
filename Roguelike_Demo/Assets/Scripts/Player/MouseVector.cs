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
    void Start()
    {
        


    }
        

    // Update is called once per frame
    void Update()
    {
        //获取玩家的位置
        playerPosition = transform.position;

    
        //获取鼠标的位置
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //计算玩家的朝向
        playerDirection = (mousePosition - playerPosition);
        ChangePlayerRotation();
       
    }
    //定义一个函数去计算人物旋转角度
    public void ChangePlayerRotation()
    {
        playerRotationAngle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, playerRotationAngle);
    }
}

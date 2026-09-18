using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//定义一个房间状态机
public enum RoomState
{
    Inactive,//不活跃
    Combat,//战斗状态
    Cleared//敌人清除
}
public class RoomController : MonoBehaviour
{
    //声明当前的状态
    private RoomState currentState = RoomState.Inactive;
    //声明门的控制器数组
    [SerializeField]
    private DoorController[] doors;
    [SerializeField]
    //声明敌人的生命健康组件数组
    private Health[] enemyHealths;
    //声明敌人的剩余数量
    private int remainingEnemies;
    //声明房间清空的事件
    public event Action RoomCleared;
    //初始化敌人的数量
    void Awake()
    {
        remainingEnemies = enemyHealths.Length;
        //出生房检查
        if(remainingEnemies == 0)
        {
            ChangeState(RoomState.Cleared);
        }
        SetDoorsClosed(false);
    }
    //触发器函数改变房间状态
    void OnTriggerEnter2D(Collider2D collision)
    {
        //判断是否为玩家并且现在的状态是否为未激活
        if(collision.CompareTag("Player") && currentState == RoomState.Inactive)
        {
            ChangeState(RoomState.Combat);
        }
        
    }

    //建立一个方法去改变房间状态
    private void ChangeState( RoomState newState)
    {
        if(currentState == newState)
        {
            return;
        }
        else
        {
            currentState = newState;
            //如果进入战斗状态就关闭房门
            if(currentState == RoomState.Combat)
            {
                SetDoorsClosed(true);
            }
            if(currentState == RoomState.Cleared)
            {
                SetDoorsClosed(false);
                RoomCleared?.Invoke();
            }
            Debug.Log("状态改变为： " + newState);
        }
        
    }
    //声明一个方法去设置所有门的开关
    private void SetDoorsClosed(bool shouldClose)
    {
        //遍历所有门的控制器
        foreach(DoorController door in doors)
        {
            door.SetClosed(shouldClose);
        }
        
    }
    //定义一个方法去接受敌人的死亡通知
    private void OnEnemyDied()
    {
        if(currentState != RoomState.Combat || remainingEnemies <= 0)
        {
            return;
        }
        else 
        {
            remainingEnemies --;
            if(remainingEnemies == 0)
            {
                ChangeState(RoomState.Cleared);
            }
        }
    }
    //订阅死亡事件
    void OnEnable()
    {
        foreach(Health enemyHealth in enemyHealths)
        {
            enemyHealth.Died += OnEnemyDied;
        }
        
    }
    void OnDisable()
    {
        foreach(Health enemyHealth in enemyHealths)
        {
            enemyHealth.Died -= OnEnemyDied;
        }
        
    }
}

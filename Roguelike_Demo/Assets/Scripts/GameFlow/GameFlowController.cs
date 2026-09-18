using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//定义游戏状态枚举
enum GameState
{
    Playing,//游玩中
    Won,//胜利
    Lost//失败
}
public class GameFlowController : MonoBehaviour
{
    //声明当前游戏状态
    private GameState currentState = GameState.Playing;
    //绑定最终房间
    [SerializeField]
    private RoomController finalRoom;
    //声明玩家的生命脚本
    private Health playerHealth;
    //声明游戏结算面板
    [SerializeField]
    private GameObject wonPanel;
    [SerializeField]
    private GameObject lostPanel;
    //接收房间清空的消息
    void OnEnable()
    {
        if(finalRoom == null)
        {
            return;
        }
        finalRoom.RoomCleared += HandleFinalRoomCleared;
    }
    void OnDisable()
    {
        if(finalRoom !=null)
        {
            finalRoom.RoomCleared -= HandleFinalRoomCleared;
        }
        if(playerHealth != null)
        {
            playerHealth.Died -= HandlePlayerDied;
        }
    }

    void Start()
    {
        //更新结算画面
        UpdateEndPanels();
        //更新游戏时间
        UpdateTimeScale();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHealth == null)
        {
            TryFindPlayer();
        }
        TryRestart();
    }
    //建立方法改变状态
    private void ChangeState(GameState newState)
    {
        if(currentState == newState)
        {
            return;
        }
        currentState = newState;
        UpdateEndPanels();
        UpdateTimeScale();
        Debug.Log("状态改变为: " + newState);
    }
    //设立一个接收方法处理房间清理的逻辑
    private void HandleFinalRoomCleared()
    {
        //将状态改变为胜利
        ChangeState(GameState.Won);
    }
    //声明一个方法去寻找玩家
    private void TryFindPlayer()
    {
        GameObject player;
        player = GameObject.FindGameObjectWithTag("Player");
        if(player == null)
        {
            return;
        }
        else
        {
            playerHealth = player.GetComponent<Health>();
            if(playerHealth != null)
            {
                playerHealth.Died += HandlePlayerDied;
            }
        }
        
    }
    //建立一个方法去处理玩家的死亡跳转
    private void HandlePlayerDied()
    {
        ChangeState(GameState.Lost);
    }
    //声明一个方法去更新游戏的结算画面
    private void UpdateEndPanels()
    {
        wonPanel.SetActive(currentState == GameState.Won);
        lostPanel.SetActive(currentState == GameState.Lost);
    }
    //改变游戏时间
    private void UpdateTimeScale()
    {
        Time.timeScale = currentState == GameState.Playing ? 1f : 0f;
    }
    //写一个方法重开
    private void TryRestart()
    {
        if(currentState == GameState.Playing || !Input.GetKeyDown(KeyCode.R))
        {
            return;
        }
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

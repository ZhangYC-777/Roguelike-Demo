using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    //声明游戏结束的UI面板
    [SerializeField]
    private GameObject gameOverPanel;
    //声明玩家的健康组件
    private Health playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHealth == null)
        {
            FindPlayer();
            if(playerHealth != null)
            {
                playerHealth.Died += ShowGameOverPanel;
            }
        }
    }
    //实现退订操作
    void OnDisable()
    {
        if(playerHealth != null)
        {
            playerHealth.Died -= ShowGameOverPanel;
        }
    }
    //声明一个方法去查找玩家
    private void FindPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            playerHealth = player.GetComponent<Health>();
            Debug.Log("找到了玩家");
        }
        else
        {
            Debug.Log("未找到玩家");
            return;
        }
    }
    //声明一个方法去显示游戏结束的UI面板
    private void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }
}

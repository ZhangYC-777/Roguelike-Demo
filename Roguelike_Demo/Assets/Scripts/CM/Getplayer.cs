using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Getplayer : MonoBehaviour
{
    // Start is called before the first frame update
     //声明相机的targetgroup
     private Cinemachine.CinemachineTargetGroup targetGroup;
     //声明玩家的位置
     private Transform playerTransform;
     //声明一个bool值判断是否获取到玩家的targetgroup
     private bool isGetPlayer = false;

    void Start()
    {
        //获取玩家的Transform组件
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        //获取相机的targetgroup组件
        targetGroup = GetComponent<Cinemachine.CinemachineTargetGroup>();
        GetPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //定义一个方法来获取玩家的targetgroup
    public void GetPlayer()
    {
        //判断是否获取到玩家的targetgroup
        if (isGetPlayer == false)
        {
            //将玩家的Transform组件添加到相机的targetgroup中
            targetGroup.AddMember(playerTransform, 1f, 2f);
            //将bool值设为true，表示已经获取到玩家的targetgroup
            isGetPlayer = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    //声明门的实际碰撞体
    public Collider2D doorCollider;
    //声明门的动画组件
    public Animator doorAnimator;
    //声明一个方法设置门的开关
    public void SetClosed(bool shouldClose)
    {
        //关系相反
        doorCollider.enabled = shouldClose;
        doorAnimator.SetBool("open", !shouldClose);
    }
}

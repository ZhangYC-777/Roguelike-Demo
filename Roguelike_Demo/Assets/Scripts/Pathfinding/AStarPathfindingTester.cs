using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathfindingTester : MonoBehaviour
{
   //声明测试的起点和终点
    [SerializeField]
    private Transform startTestTransform;
    [SerializeField]
    private Transform targetTestTransform;
    //声明寻路脚本组件
    private AStarPathfinding pathfinding;
    void Start()
    {
        //获取寻路脚本组件
        pathfinding = GetComponent<AStarPathfinding>();
        if(startTestTransform != null && targetTestTransform != null)
        {
            //调用寻路方法
            bool pathFound = pathfinding.TryFindPath(startTestTransform.position, targetTestTransform.position, out List<PathNode> path);
             //测试寻路方法
        if (!pathFound)
        {
            Debug.Log("没有找到路径");
        }
        else if (path.Count == 0)
        {
            Debug.Log("起点和终点在同一格");
        }
        else
        {
            Debug.Log("找到正常路径，节点数量：" + path.Count);
        }
        }
       
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

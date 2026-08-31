using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarPathfinding : MonoBehaviour
{
    //声明网格的AStarGrid
    private AStarGrid grid;
    //声明当前寻找到的路径
    private List<PathNode> currentPath;
    //声明测试的起点和终点
    [SerializeField]
    private Transform startTestTransform;
    [SerializeField]
    private Transform targetTestTransform;
    void Awake()
    {
         //获取网格的AStarGrid组件
        grid = GetComponent<AStarGrid>();
    }
    void Start()
    {
        if(startTestTransform != null && targetTestTransform != null)
        {
            //调用寻路方法
            FindPath(startTestTransform.position, targetTestTransform.position);
        }
    }
    //绘制当前寻找到的路径
    void OnDrawGizmos()
    {
        if(currentPath == null)
        {
            return;
        }
        else
        {
            //遍历当前寻找到的路径
            foreach (PathNode pathNode in currentPath)
            {
                //绘制当前寻找到的路径
                Gizmos.color = Color.yellow;
                Gizmos.DrawCube(pathNode.worldPosition, Vector2.one *0.4f);
            }
        }
    }
    //声明一个方法去寻路
    private void FindPath(Vector2 startPos, Vector2 targetPos)
    {
        currentPath = null;
        //获取起点的网格节点
        PathNode startNode = grid.NodeFromWorldPoint(startPos);
        //获取终点的网格节点
        PathNode targetNode = grid.NodeFromWorldPoint(targetPos);
        //声明一个开启列表
        List<PathNode> openSet = new List<PathNode>();
        //声明一个关闭列表
        HashSet<PathNode> closedSet = new HashSet<PathNode>();
        //初始化起点坐标的数据
        startNode.gCost = 0;
        startNode.hCost = GetDistance(startNode, targetNode);
        startNode.parent = null;
        //将起点加入开启列表
        openSet.Add(startNode);
        //将节点移入关闭列表
        while(openSet.Count > 0)
        {
            //获取当前集合中最低代价的节点
            PathNode currentNode = GetLowestCostNode(openSet);
            //将当前节点从开启列表中移除
            openSet.Remove(currentNode);
            //将当前节点加入关闭列表
            closedSet.Add(currentNode);
             //如果当前节点就是终点节点
            if(currentNode == targetNode)
            {
                //暂时直接结束
                currentPath = RetracePath(startNode, targetNode);
                return;
            }
            //对当前节点的邻居进行过滤
            foreach (PathNode neighbour in grid.GetNeighbours(currentNode))
            {
                //如果邻居节点在关闭列表中或者不可走，则跳过
                if (closedSet.Contains(neighbour) || !neighbour.Walkable)
                {
                    continue;
                }
                else
                {
                    //计算邻居的代价G
                    int neighbourGCost = currentNode.gCost + GetDistance(currentNode, neighbour);
                    //如果邻居节点不在开启列表中，或者计算出的代价G小于当前邻居节点的代价G
                    if (!openSet.Contains(neighbour) || neighbourGCost < neighbour.gCost)
                    {
                        //更新邻居节点的代价G
                        neighbour.gCost = neighbourGCost;
                        //更新邻居节点的代价H
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        //更新邻居节点的父节点为当前节点
                        neighbour.parent = currentNode;
                        //如果邻居节点不在开启列表中，则将其加入开启列表
                        if (!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
                        }
                    }
                }
            }
        }
    }
    //声明一个方法去选择节点
    private PathNode GetLowestCostNode(List<PathNode> pathNodes)
    {
        if (pathNodes.Count == 0)
        {
            return null;
        }
        else
        {
            //声明一个变量去保存最低代价的节点 像冒泡排序单轮的比较一样去比较每个节点的总代价F
            PathNode lowestCostNode = pathNodes[0];
            for (int i = 1; i < pathNodes.Count; i++)
            {
                if (pathNodes[i].fCost < lowestCostNode.fCost)
                {
                    lowestCostNode = pathNodes[i];
                }
                if (pathNodes[i].fCost == lowestCostNode.fCost)
                {
                    if (pathNodes[i].hCost < lowestCostNode.hCost)
                    {
                        lowestCostNode = pathNodes[i];
                    }
                }
            }
            return lowestCostNode;
        }
    }
    //建立一个方法去取得两个目标点的曼哈顿距离
    private int GetDistance(PathNode nodeA, PathNode nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);
        return dstX + dstY;
    }
    //声明一个方法获得路径
    private List<PathNode> RetracePath(PathNode startNode, PathNode endNode)
    {
        List<PathNode> pathNodes = new List<PathNode>();
        PathNode currentNode = endNode;
        while (currentNode != startNode)
        {
            pathNodes.Add(currentNode);
            currentNode = currentNode.parent;
        }
        pathNodes.Reverse();
        return pathNodes;
    }
}

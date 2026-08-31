using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    //声明格子能不能走
    public bool Walkable;
    //声明格子中心的世界坐标
    public Vector2 worldPosition;
    //声明格子在网格中的横向索引
    public int gridX;
    //声明格子在网格中的纵向索引
    public int gridY;
    //声明一个方法去保存数据
    //声明距离起点的代价G
    public int gCost;
    //声明距离终点的代价H
    public int hCost;
    //声明总代价F
    public int fCost
    {
        get{return gCost + hCost;}
    }
    //声明父节点
    public PathNode parent;
    public PathNode(bool _walkable, Vector2 _worldPos, int _gridX, int _gridY)
    {
        Walkable = _walkable;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
    }
    
}

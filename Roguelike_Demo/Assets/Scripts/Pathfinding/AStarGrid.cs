using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarGrid : MonoBehaviour
{
    //声明网格的世界大小
     [SerializeField]
    private Vector2 gridWorldSize = new Vector2(10, 6);
    //声明网格的节点大小
     [SerializeField]
    private float cellSize = 1f;
    //声明横向节点数
    private int gridSizeX;
    //声明纵向节点数
    private int gridSizeY;
    //声明一个网格节点的二维数组
    private PathNode[,] grid;
    //声明需要要过滤的图层
    [SerializeField]
    private LayerMask obstacleLayer ;
    //声明起点的Transform
    [SerializeField]
    private Transform startTransform;
    //声明终点的Transform
    [SerializeField]
    private Transform targetTransform;

    //计算节点数量
    void Awake()
    {
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / cellSize);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / cellSize);
        grid = new PathNode[gridSizeX, gridSizeY];
        //调用创建网格的方法
        CreateGrid();

    }
    private void OnDrawGizmos()
    {
        if(grid == null)
        {
            return;
        }
        else
        {
            //获取起始位置的网格坐标
            PathNode startNode = NodeFromWorldPoint(startTransform.position);
            //获取目标位置的网格坐标
            PathNode targetNode = NodeFromWorldPoint(targetTransform.position);
            for (int x = 0; x < gridSizeX; x++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    //设置Gizmos的颜色
                    Gizmos.color = (grid[x, y].Walkable) ? Color.white : Color.red;
                    //绘制网格节点
                    Gizmos.DrawWireCube(grid[x, y].worldPosition,Vector2.one * (cellSize - 0.1f));
                    if (grid[x, y] == startNode)
                    {
                        Gizmos.color = Color.green;
                        Gizmos.DrawWireCube(grid[x, y].worldPosition, Vector2.one * (cellSize - 0.1f));
                    }
                    if (grid[x, y] == targetNode)
                    {
                        Gizmos.color = Color.blue;
                        Gizmos.DrawWireCube(grid[x, y].worldPosition, Vector2.one * (cellSize - 0.1f));
                    }
                }
            }
        }
        
    }
    //声明一个方法去创建网格
    private void CreateGrid()
    {
        //计算网格左下角的世界坐标
        float worldBottomLeftX = transform.position.x - gridWorldSize.x / 2;
        float worldBottomLeftY = transform.position.y - gridWorldSize.y / 2;
        //遍历网格的每个节点
        for (int x = 0; x < gridSizeX; x++)
        {
            for(int y = 0; y < gridSizeY; y++)
            {
                //计算每个节点的世界坐标
                Vector2 worldPoint = new Vector2(worldBottomLeftX + x * cellSize + cellSize / 2, worldBottomLeftY + y * cellSize + cellSize / 2);
                //判断节点是否可走
                Collider2D obstacle = Physics2D.OverlapBox(worldPoint, Vector2.one * (cellSize * 0.8f), 0f, obstacleLayer);
                bool walkable = (obstacle == null);
                grid[x, y] = new PathNode(walkable, worldPoint, x, y);
            }
        }
    }
    //声明一个方法去获取网格节点
    public PathNode NodeFromWorldPoint(Vector2 worldPosition)
    {
        //计算节点在网格中的索引
        int x = Mathf.Clamp(Mathf.FloorToInt((worldPosition.x - transform.position.x + gridWorldSize.x / 2) / cellSize), 0, gridSizeX - 1);
        int y = Mathf.Clamp(Mathf.FloorToInt((worldPosition.y - transform.position.y + gridWorldSize.y / 2) / cellSize), 0, gridSizeY - 1);
        return grid[x, y];
    }
    //声明一个方法去获取网格的邻居节点
    public List<PathNode> GetNeighbours(PathNode node)
    {
        List<PathNode> neighbours = new List<PathNode>();
        int neighbourX1 = node.gridX - 1;
        int neighbourX2 = node.gridX + 1;
        int neighbourY1 = node.gridY - 1;
        int neighbourY2 = node.gridY + 1;
        if (neighbourX1 >= 0)
        {
            neighbours.Add(grid[neighbourX1, node.gridY]);
        }
        if (neighbourX2 < gridSizeX)
        {
            neighbours.Add(grid[neighbourX2, node.gridY]);
        }
        if (neighbourY1 >= 0)
        {
            neighbours.Add(grid[node.gridX, neighbourY1]);
        }
        if (neighbourY2 < gridSizeY)
        {
            neighbours.Add(grid[node.gridX, neighbourY2]);
        }
        return neighbours;
    }
}

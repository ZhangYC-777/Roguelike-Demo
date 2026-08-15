using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpwaner : MonoBehaviour
{
    //声明角色预制体
    public GameObject playerPrefab;
    //声明角色出生位置
    public Transform spwanPoint;

    void Start()
    {
        Instantiate(playerPrefab, spwanPoint.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

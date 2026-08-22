using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Health : MonoBehaviour,IDamageable
{
    //声明最大生命值
    [SerializeField]
    private int maxHealth;
    //声明当前生命值
    private int currentHealth;
    //声明一个生命改变事件
    public event Action<int, int> HealthChanged;
    //声明一个死亡事件
    public event Action Died;
   
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     void Awake()
    {
        //初始化最大生命值
        currentHealth = maxHealth;
    }
     //调用受伤接口，实现接口方法
    public void TakeDamage(int damage)
    {
        if (damage <= 0 || currentHealth <= 0)
        {
            return;
        }
        else
        {
            currentHealth -= damage;
            if(currentHealth <= 0)
            {
                currentHealth = 0;
            }
        }
        //事件发布
        HealthChanged?.Invoke(currentHealth, maxHealth);
        if(currentHealth <= 0)
        {
            Died?.Invoke();
        }
    }
}

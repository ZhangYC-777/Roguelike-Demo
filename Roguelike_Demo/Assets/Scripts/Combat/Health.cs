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
    //声明受伤无敌持续时间
    [SerializeField]
    private float invincibilityDuration = 0.5f;
    //声明受伤无敌计时器
    private float invincibilityTimer;
    //声明受伤是否无敌
    private bool isDamageImmune;


   
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //受伤无敌倒计时
        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
    }
     void Awake()
    {
        //初始化最大生命值
        currentHealth = maxHealth;
    }
     //调用受伤接口，实现接口方法
    public void TakeDamage(int damage)
    {
        if (damage <= 0 || currentHealth <= 0 || invincibilityTimer > 0 || isDamageImmune)
        {
            return;
        }
        else
        {
            currentHealth -= damage;
            //启动受伤无敌计时器
            invincibilityTimer = invincibilityDuration;
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
    //声明一个方法用于设置无敌状态
    public void SetDamageImmune(bool isImmune)
    {
        isDamageImmune = isImmune;
    }
}

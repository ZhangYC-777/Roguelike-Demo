using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Projectile : MonoBehaviour
{
    //声明子弹的Rigidbody组件
    private Rigidbody2D projectileRigidbody;
    //声明子弹伤害
    private int damage;
    //声明字段使子弹可以被回收
    private IObjectPool<Projectile> projectilePool;
    //声明bool值判断子弹是否被回收
    private bool hasReturned;
    //设置子弹的生命周期
    private float remainingLifetime;
    //处理子弹的生命周期
    void Update()
    {
        //减少子弹的剩余生命周期
        remainingLifetime -= Time.deltaTime;
        //如果子弹的剩余生命周期小于等于0，则回收子弹
        if (remainingLifetime <= 0f)
        {
            TryReturnToPool();
        }
    }
    void Awake()
    {
        //获取子弹的Rigidbody组件
        projectileRigidbody = GetComponent<Rigidbody2D>();
    }
    // 检查碰撞并设值子弹的销毁
    void OnTriggerEnter2D(Collider2D other)
    {
        //检测子弹碰撞的对象是否有IDamageable接口
        IDamageable damageable = other.GetComponentInParent<IDamageable>();
        if (damageable != null)
        {
            //调用受伤方法
            damageable.TakeDamage(damage);
            
        }
        //回收子弹
        TryReturnToPool();
    }
    //声明发方法初始化子弹
    public void Initialize(Vector2 direction, int projectileDamage, float speed, float lifetime)
    {
        //设置子弹为未回收状态
        hasReturned = false;
        //设置子弹的剩余生命周期
        remainingLifetime = lifetime;
        //设置子弹伤害
        damage = projectileDamage;
        //设置子弹速度
        projectileRigidbody.velocity = direction.normalized * speed;
        
    }
    //声明方法设置子弹对象池
    public void SetProjectilePool(IObjectPool<Projectile> pool)
    {
        projectilePool = pool;
    }
    //声明方法回收子弹
    private void TryReturnToPool()
    {
        if (hasReturned)
        {
            return;
        }
        else
        {
            hasReturned = true;
            projectileRigidbody.velocity = Vector2.zero;
            projectilePool.Release(this);
        }
    }
    
}

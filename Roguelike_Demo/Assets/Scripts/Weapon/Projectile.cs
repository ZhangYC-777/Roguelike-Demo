using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //声明子弹的Rigidbody组件
    private Rigidbody2D projectileRigidbody;
    //声明子弹伤害
    private int damage;
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
        //销毁子弹
            Destroy(gameObject);
    }
    //声明发方法初始化子弹
    public void Initialize(Vector2 direction, int projectileDamage, float speed, float lifetime)
    {
        //设置子弹伤害
        damage = projectileDamage;
        //设置子弹速度
        projectileRigidbody.velocity = direction.normalized * speed;
        //销毁子弹
        Destroy(gameObject, lifetime);
    }
}

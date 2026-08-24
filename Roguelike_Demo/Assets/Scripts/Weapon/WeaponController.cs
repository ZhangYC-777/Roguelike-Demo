using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    //声明武器定义
    [SerializeField]
    private WeaponDefinition weaponDefinition;
    //声明武器的发射点
    [SerializeField]
    private Transform shootPoint;
    //声明下一次开火时间
    private float nextFireTime;
    //声明角色的PlayerMove组件
    private PlayerMove playerMove;
    // Update is called once per frame
    void Update()
    {
        //处理开火逻辑
        if(Input.GetMouseButtonDown(0) && Time.time >= nextFireTime && playerMove.CanFire)
        {
            Fire();
            nextFireTime = Time.time + weaponDefinition.FireInterval;
        }
    }
    void Awake()
    {
        //获取角色的PlayerMove组件
        playerMove = GetComponentInParent<PlayerMove>();
    }
    //声明一个开火方法
    private void Fire()
    {
        //实例化子弹
        GameObject projectile = Instantiate(weaponDefinition.ProjectilePrefab, shootPoint.position, shootPoint.rotation);
        //获取子弹的Projectile组件
        Projectile projectileComponent = projectile.GetComponent<Projectile>();
        //初始化子弹
        projectileComponent.Initialize(shootPoint.right, weaponDefinition.Damage, weaponDefinition.ProjectileSpeed, weaponDefinition.ProjectileLifetime);
    }
}

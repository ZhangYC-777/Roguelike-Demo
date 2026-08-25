using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

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
    //声明一个子弹对象池
    private ObjectPool<Projectile> projectilePool;
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
        //创建一个新的对象池
        projectilePool = new ObjectPool<Projectile>(CreateProjectile, OnGetProjectile, OnReleaseProjectile, OnDestroyProjectile, true, 10, 50);
    }
    //声明一个开火方法
    private void Fire()
    {
        //从对象池中获取一个子弹
        Projectile projectileComponent = projectilePool.Get();
        //设置子弹的位置和旋转
        projectileComponent.transform.position = shootPoint.position;
        projectileComponent.transform.rotation = shootPoint.rotation;
        //初始化子弹
        projectileComponent.Initialize(shootPoint.right, weaponDefinition.Damage, weaponDefinition.ProjectileSpeed, weaponDefinition.ProjectileLifetime);
    }
    //声明一个方法得到预制体的子弹组件
    private Projectile CreateProjectile()
    {
        GameObject projectile = Instantiate(weaponDefinition.ProjectilePrefab);
        Projectile projectileComponent = projectile.GetComponent<Projectile>();
        projectileComponent.SetProjectilePool(projectilePool);
        return projectileComponent;
    }
    //声明方法激活子弹
    private void OnGetProjectile(Projectile projectile)
    {
        projectile.gameObject.SetActive(true);
    }
    //声明方法释放子弹
    private void OnReleaseProjectile(Projectile projectile)
    {
        projectile.gameObject.SetActive(false);
    }
    //声明方法销毁子弹
    private void OnDestroyProjectile(Projectile projectile)
    {
        Destroy(projectile.gameObject);
    }
}

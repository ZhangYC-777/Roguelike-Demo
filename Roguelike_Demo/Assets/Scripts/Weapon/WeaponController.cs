using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class WeaponController : MonoBehaviour
{
    //声明武器数组定义
    [SerializeField]
    private WeaponDefinition[] weaponDefinitions;
   //声明当前武器的索引
    private int currentWeaponIndex = 0;
    WeaponDefinition CurrentWeaponDefinition => weaponDefinitions[currentWeaponIndex];
    //声明武器的发射点
    [SerializeField]
    private Transform shootPoint;
    //声明下一次开火时间
    private float nextFireTime;
    //声明角色的PlayerMove组件
    private PlayerMove playerMove;
    //声明一个子弹对象池
    private ObjectPool<Projectile> projectilePool;
    //声明武器的sprite组件
    [SerializeField]
    private SpriteRenderer weaponSpriteRenderer;
    void Update()
    {
        //处理武器切换逻辑
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchWeapon(0);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchWeapon(1);
        }
        //处理开火逻辑
        if(Input.GetMouseButtonDown(0) && Time.time >= nextFireTime && playerMove.CanFire)
        {
            Fire();
            nextFireTime = Time.time + CurrentWeaponDefinition.FireInterval;
        }
    }
    void Awake()
    {
        //获取角色的PlayerMove组件
        playerMove = GetComponentInParent<PlayerMove>();
        //创建一个新的对象池
        projectilePool = new ObjectPool<Projectile>(CreateProjectile, OnGetProjectile, OnReleaseProjectile, OnDestroyProjectile, true, 10, 50);
        //初始化武器的sprite
        ApplyCurrentWeaponVisual();
    }
    //声明一个开火方法
    private void Fire()
    {
        float angleOffset;
        float startAngle = 0f;
        float angleStep = 0f;
        //根据武器定义的子弹数量来计算初始角度和角度间隔
       if(CurrentWeaponDefinition.ProjectileCount == 1)
        {
           startAngle = 0f;
           angleStep = 0f;
        }
        else
        {
             startAngle = -CurrentWeaponDefinition.SpreadAngle / 2f;
             angleStep = CurrentWeaponDefinition.SpreadAngle / (CurrentWeaponDefinition.ProjectileCount - 1);
        }
        //根据武器定义的子弹数量来获取子弹
        for(int i = 0; i < CurrentWeaponDefinition.ProjectileCount; i++)
    {
        //从对象池中获取一个子弹
        Projectile projectileComponent = projectilePool.Get();
        //计算每个子弹的角度
        angleOffset = startAngle + i * angleStep;
        Vector2 rotatedDirection = Quaternion.Euler(0, 0, angleOffset) * shootPoint.right;
        //设置子弹的位置和旋转
        projectileComponent.transform.position = shootPoint.position;
        projectileComponent.transform.rotation = shootPoint.rotation;
        //初始化子弹
        projectileComponent.Initialize(rotatedDirection, CurrentWeaponDefinition.Damage, CurrentWeaponDefinition.ProjectileSpeed, CurrentWeaponDefinition.ProjectileLifetime);
    }
    }
    //声明一个方法得到预制体的子弹组件
    private Projectile CreateProjectile()
    {
        GameObject projectile = Instantiate(CurrentWeaponDefinition.ProjectilePrefab);
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
    //声明一个方法去切换武器
    private void SwitchWeapon(int newIndex)
    {
        if(newIndex < 0 || newIndex >= weaponDefinitions.Length)
        {
            return;
        }
        else if(newIndex == currentWeaponIndex)
        {
            return;
        }
        else
        {
            currentWeaponIndex = newIndex;
            ApplyCurrentWeaponVisual();
            nextFireTime = Time.time;
        }
    }
    //声明一个方法去更新武器的sprite
    private void ApplyCurrentWeaponVisual()
    {
        if(weaponSpriteRenderer != null && CurrentWeaponDefinition.WeaponSprite != null)
        {
            weaponSpriteRenderer.sprite = CurrentWeaponDefinition.WeaponSprite;
        }
    }
}

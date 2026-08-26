using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//设置菜单
[CreateAssetMenu(fileName = "New Weapon Definition", menuName = "Weapons/Weapon Definition")]
public class WeaponDefinition : ScriptableObject
{
    //声明武器的名称
    [SerializeField]
    private string weaponName;
    public string WeaponName => weaponName;
    //声明子弹的预制体
    [SerializeField]
    private GameObject projectilePrefab;
    public GameObject ProjectilePrefab => projectilePrefab;
    //声明子弹伤害
    [SerializeField]
    private int damage;
    public int Damage => damage;
    //声明子弹速度
    [SerializeField]
    private float projectileSpeed;
    public float ProjectileSpeed => projectileSpeed;
    //声明子弹存活时间
    [SerializeField]
    private float projectileLifetime;
    public float ProjectileLifetime => projectileLifetime;
    //声明子弹之间的发射间隔
    [SerializeField]
    private float fireInterval;
    public float FireInterval => fireInterval;
    //声明子弹产生数量
    [SerializeField]
    private int projectileCount;
    public int ProjectileCount => projectileCount;
    //声明子弹的散射角度
    [SerializeField]
    private float spreadAngle;
    public float SpreadAngle => spreadAngle;
    //声明武器的sprite
    [SerializeField]
    private Sprite weaponSprite;
    public Sprite WeaponSprite => weaponSprite;


}

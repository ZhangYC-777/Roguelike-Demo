using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//声明敌人状态枚举
enum EnemyState
{
    Dormant,//休眠
    Chase,//追击
    Attack,//攻击
    Dead//死亡
}
public class EnemyController : MonoBehaviour
{
    //声明敌人的移动速度
    public float moveSpeed = 2f;
    //声明敌人的活跃路线
    public float activationDistance = 5f;
    //声明敌人的停止距离
    public float stopDistance = 1.2f;
    //声明敌人的RigidBody组件
    private Rigidbody2D enemyRigidbody;
    //声明敌人的当前状态
    private EnemyState currentState = EnemyState.Dormant;
    //声明敌人的动画组件
    private Animator enemyAnimator;
    //声明玩家的位置
    private Transform playerTransform;
    //声明敌人的Health组件
    private Health enemyHealth;
    //声明敌人攻击距离
    [SerializeField]
    private float attackDistance = 1.2f;
    //声明敌人攻击冷却时间
    [SerializeField]
    private float attackCooldown = 1f;
    //声明敌人的攻击伤害
    [SerializeField]
    private int attackDamage = 10;
    //声明敌人攻击计时器
    private float attackCooldownTimer = 0f;
    //声明玩家的IDamageable组件
    private IDamageable playerDamageable;
    // Start is called before the first frame update
    //初始获取必要组件
    void Awake()
    {
        //获取组件并初始化敌人动画与敌人速度
        enemyRigidbody = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
        enemyHealth = GetComponent<Health>();
        enemyRigidbody.velocity = Vector2.zero;
        enemyAnimator.SetBool("isMoving", false);
        enemyAnimator.SetBool("isIdle", true); 
        enemyAnimator.SetBool("aimDown", true);

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //初始查找玩家
        if(playerTransform == null)
        {
            TryFindPlayer();
        }
        //如果处在休眠状态，检查玩家是否在激活范围内
        if(currentState == EnemyState.Dormant && playerTransform != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
            if(distanceToPlayer <= activationDistance)
            {
                ChangeState(EnemyState.Chase);
            }
           
        }
        //处理计时器逻辑
        if(attackCooldownTimer > 0f)
        {
            attackCooldownTimer -= Time.deltaTime;
        }
    }
    void FixedUpdate()
    {
        //实现敌人有限状态机
        switch(currentState)
        {
                //处理休眠状态
            case EnemyState.Dormant:
                {
                    enemyAnimator.SetBool("isMoving", false);
                    enemyAnimator.SetBool("isIdle", true);
                }
               
                break;
                //处理追击状态
            case EnemyState.Chase:
                {
                    float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
                    
                    if(distanceToPlayer <= attackDistance)
                    {
                        enemyRigidbody.velocity = Vector2.zero;
                        enemyAnimator.SetBool("isMoving", false);
                        enemyAnimator.SetBool("isIdle", true);
                        ChangeState(EnemyState.Attack);
                    }
                    else
                    {
                        enemyRigidbody.MovePosition(Vector2.MoveTowards(enemyRigidbody.position, playerTransform.position, moveSpeed * Time.fixedDeltaTime));
                        enemyAnimator.SetBool("isMoving", true);
                        enemyAnimator.SetBool("isIdle", false);
                    }
                }
                
                break;
                //处理死亡状态
            case EnemyState.Dead:
                {
                    enemyAnimator.SetBool("isMoving", false);
                    enemyAnimator.SetBool("isIdle", true);
                    enemyRigidbody.velocity = Vector2.zero;
                }
                break;
            case EnemyState.Attack:
                {
                    //攻击逻辑可以在这里实现
                    float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
                    if(distanceToPlayer > attackDistance)
                    {
                        ChangeState(EnemyState.Chase);
                    }
                    else
                    {
                        enemyAnimator.SetBool("isMoving", false);
                        enemyAnimator.SetBool("isIdle", true);
                        enemyRigidbody.velocity = Vector2.zero;
                         //处理攻击逻辑
                        if(attackCooldownTimer <= 0f && playerDamageable != null)
                        {
                            playerDamageable.TakeDamage(attackDamage);
                            attackCooldownTimer = attackCooldown;
                        }
                    }    
                }
                break;
        }

    }
    //事件的订阅与退订
    void OnEnable()
    {
        enemyHealth.Died += Die;
    }
    void OnDisable()
    {
        enemyHealth.Died -= Die;
    }
    //声明方法寻找玩家的位置
    private void TryFindPlayer()
    {   GameObject playerTarget = GameObject.FindGameObjectWithTag("Player");
        if(playerTarget != null)
        {
            Debug.Log("玩家已找到");
            playerTransform = playerTarget.transform;
            playerDamageable = playerTarget.GetComponent<IDamageable>();
            return;
        }
        else
        {
            Debug.Log("玩家未找到");
        }
       
        
    }
    //声明方法改变敌人的状态
    private void ChangeState(EnemyState newState)
    {
        currentState = newState;
        Debug.Log("敌人状态已改变为：" + newState);
    }
    //声明一个方法处理敌人的死亡逻辑
    private void Die()
    {
        ChangeState(EnemyState.Dead);
    }
}

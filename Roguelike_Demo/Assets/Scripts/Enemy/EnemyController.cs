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
    //声明寻路组件
    private AStarPathfinding pathFinding;
    //声明获取到的路径列表
    private List<PathNode> currentPath;
    //声明当前路径的索引
    private int currentPathIndex;
    //声明重新计算路径的间隔
    [SerializeField]
    private float pathRecalculationInterval = 0.3f;
    //声明重新计算路径的倒计时
    private float pathRecalculationTimer = 0f;

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
        //获取寻路组件
        pathFinding = FindObjectOfType<AStarPathfinding>();

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
                        UpdatePathRecalculation();
                        FollowPath();
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
        //进入追击状态先进行一次寻路
        if(newState == EnemyState.Chase)
        {
            RequestPath();
            //将倒计时重置为重新计算路径的间隔
            pathRecalculationTimer = pathRecalculationInterval;
        }
        else
        {
            ClearPath();
        }
    }
    //声明一个方法处理敌人的死亡逻辑
    private void Die()
    {
        ChangeState(EnemyState.Dead);
    }
    //声明一个方法去请求路线
    private void RequestPath()
    {
        if(playerTransform == null || pathFinding == null)
        {
            currentPath = null;
            currentPathIndex = 0;
            return;
        }
        else
        {
            currentPathIndex = 0;
            pathFinding.TryFindPath(enemyRigidbody.position, playerTransform.position, out currentPath);
        }
    }
    //声明一个方法让敌人跟随节点移动
    private void FollowPath()
    {
        //判断无法移动的逻辑
        if(currentPath == null || currentPath.Count == 0 || currentPathIndex >= currentPath.Count)
        {
            enemyRigidbody.velocity = Vector2.zero;
            enemyAnimator.SetBool("isMoving", false);
            enemyAnimator.SetBool("isIdle", true);
            return;
        }
        else
        {
            //处理移动逻辑
            enemyRigidbody.MovePosition(Vector2.MoveTowards(enemyRigidbody.position, currentPath[currentPathIndex].worldPosition, moveSpeed * Time.fixedDeltaTime));
            enemyAnimator.SetBool("isMoving", true);
            enemyAnimator.SetBool("isIdle", false);

            float distanceToPathNode = Vector2.Distance(transform.position, currentPath[currentPathIndex].worldPosition);
            if(distanceToPathNode < 0.05f)
            {
                currentPathIndex ++;
            }

        }
    }
    //声明一个函数去处理路径实时更新逻辑
    private void UpdatePathRecalculation()
    {
        //计时器定时 间隔0.3秒寻路一次
        pathRecalculationTimer -= Time.fixedDeltaTime;
        if(pathRecalculationTimer <= 0)
        {
            RequestPath();
            pathRecalculationTimer = pathRecalculationInterval;
        }
    }
    //声明方法清理旧路径
    private void ClearPath()
    {
        currentPath = null;
        currentPathIndex = 0;
        pathRecalculationTimer = 0f;
    }
}

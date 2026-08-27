using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//声明敌人状态枚举
enum EnemyState
{
    Dormant,//休眠
    Chase,//追击
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
    // Start is called before the first frame update
    //初始获取必要组件
    void Awake()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        enemyAnimator = GetComponent<Animator>();
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
    }
    void FixedUpdate()
    {
        //实现敌人有限状态机
        switch(currentState)
        {
            case EnemyState.Dormant:
                {
                    enemyAnimator.SetBool("isMoving", false);
                    enemyAnimator.SetBool("isIdle", true);
                }
                //处理休眠状态
                break;
            case EnemyState.Chase:
                {
                    float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
                    
                    if(distanceToPlayer <= stopDistance)
                    {
                        enemyRigidbody.velocity = Vector2.zero;
                        enemyAnimator.SetBool("isMoving", false);
                        enemyAnimator.SetBool("isIdle", true);
                    }
                    else
                    {
                        enemyRigidbody.MovePosition(Vector2.MoveTowards(enemyRigidbody.position, playerTransform.position, moveSpeed * Time.fixedDeltaTime));
                        enemyAnimator.SetBool("isMoving", true);
                        enemyAnimator.SetBool("isIdle", false);
                    }
                    
                    

                }
                //处理追击状态
                break;
        }

    }
    //声明方法寻找玩家的位置
    private void TryFindPlayer()
    {   GameObject playerTarget = GameObject.FindGameObjectWithTag("Player");
        if(playerTarget != null)
        {
            Debug.Log("玩家已找到");
            playerTransform = playerTarget.transform;
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
}

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public enum EnemyState { Idle, Trace, Attack, RunAway }

    public EnemyState state = EnemyState.Idle;

    public float moveSpeed = 2f;

    public float traceRange = 15;

    public float attackRange = 6f;

    public float attackCooldown = 1.5f;

    public float EnemyHelth = 5f;

    public GameObject projectilePrefad;

    public Transform firePoint;

    private Transform player;

    private float IastAttackTime;

    public int maxHP = 5;

    private int currentHp;

    private Rigidbody2D rb;

      public Slider hpSlider;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        IastAttackTime = -attackCooldown;
        currentHp = maxHP;
        rb = GetComponent<Rigidbody2D>();
        hpSlider.value = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(player.position, transform.position);


        if ((float)currentHp / maxHP < .2f)
        {
            state = EnemyState.RunAway;
        }
          

        switch (state)
        {
            case EnemyState.Idle:
                if (dist < traceRange)
                    state = EnemyState.Trace;
                break;
            case EnemyState.Trace:
                if (dist < attackRange)
                    state = EnemyState.Attack;
                else if (dist > traceRange)
                    state = EnemyState.Idle;
                else
                    TracePlayer();
                break;

            case EnemyState.Attack:
                if (dist > attackRange)
                    state = EnemyState.Trace;
                else
                    AttackPlayer();
                break;

            case EnemyState.RunAway:
            
                 RunAway();
                break;
                
                 
            
        }
    }

                
    // 다른 스크립트가 적에게 데미지를 줄 때 호출하는 함수
    public void TakeDamage(float amount)
    {
        EnemyHelth -= amount;
        hpSlider.value = (float)currentHp / maxHP;

        // 만약 적의 체력이 0 이하가 되면, 적 오브젝트를 파괴합니다.
        if (EnemyHelth <= 0)
        {
            Destroy(gameObject);
        }
    }
    void RunAway()
    {
        if (player == null) return;
        Vector3 dir = (player.position - transform.position).normalized;
        transform.position += dir * -moveSpeed * Time.deltaTime;
        transform.LookAt(player.position);
    }
    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void TracePlayer()
    {
        Vector3 dir = (player.position - transform.position).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
        transform.LookAt(player.position);
    }

    void AttackPlayer()
    {
        if (Time.time >= IastAttackTime + attackCooldown)
        {
            IastAttackTime = Time.time;
            ShootProjectile();
        }
    }

    void ShootProjectile()
    {
        if (projectilePrefad != null && firePoint != null)
        {
            transform.LookAt(player.position);
            GameObject proj = Instantiate(projectilePrefad, firePoint.position, firePoint.rotation);
            EnemyProjectile ep = proj.GetComponent<EnemyProjectile>();

            if (ep != null)
            {
                Vector3 dir = (player.position - firePoint.position).normalized;
                ep.SetDirection(dir);
            }
        }
    }
    
    
}

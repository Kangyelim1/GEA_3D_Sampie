using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;

    private Transform player;

    public float EnemyHelth = 5f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        Vector3 direction = (player.position = transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
        transform.LookAt(player.position);
    }

    // 다른 스크립트가 적에게 데미지를 줄 때 호출하는 함수
    public void TakeDamage(float amount)
    {
        EnemyHelth -= amount;

        // 만약 적의 체력이 0 이하가 되면, 적 오브젝트를 파괴합니다.
        if (EnemyHelth <= 0)
        {
            Destroy(gameObject);
        }
    }
    
    
}

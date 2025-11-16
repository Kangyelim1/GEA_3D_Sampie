using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHarvester : MonoBehaviour
{
    public Inventory myInventory;
    public InventoryManager inventoryManager;

    public float rayDistance = 5f;        // 채집 가능 거리
    public LayerMask hitMask = ~0;      // 가능한 한 레이어 전부 다 (일단)
    public int toolDamage = 1;          // 타격 데미지
    public float hitCooldown = 0.15f;   // 연타 간격

    private float _nextHitTime;
    private Camera _cam;
    public Inventory inventory;         // 플레이어 인벤(없으면 자동 부착)

    void Awake()
    {
        _cam = Camera.main;
        if (inventory == null) inventory = gameObject.AddComponent<Inventory>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= _nextHitTime)
        {
            _nextHitTime = Time.time + hitCooldown;

            Ray ray = _cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // 화면 중앙
            if (Physics.Raycast(ray, out var hit, rayDistance, hitMask))
            {
                var block = hit.collider.GetComponent<Block>();
                if (block != null)
                {
                    block.Hit(toolDamage, inventory);
                }
            }
        }
    }

    void HarvestBlock(BlockType type) // 파괴된 블록의 타입 정보를 받음
    {
        int count = 1; // 획득 개수 (예시)

        // ----------------------------------------------------
        // 1. 📦 데이터에 아이템 추가 (교수님 힌트의 Inventory.Add 호출)
        // ----------------------------------------------------
        myInventory.Add(type, count);

        // ----------------------------------------------------
        // 2. 🔄 UI 갱신 요청 (마지막 코드의 위치! 이것이 화면에 나타나게 함)
        // ----------------------------------------------------
        inventoryManager.UpdateInventory(myInventory);

        // ... (나머지 로직: 블록 오브젝트 삭제, 이펙트 재생 등)
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject slotPrefab;       // SlotItemPrefab 스크립트가 붙은 프리팹
    public Transform inventoryContent;   // 슬롯들이 생성될 부모 Transform (Panel)
    public Sprite[] itemSprites;        // BlockType 순서대로 아이콘 Sprite 연결
                                        // Start is called before the first frame update

    // 인벤토리 데이터를 받아 UI를 갱신하는 함수 (채집 시 호출)
    public void UpdateInventory(Inventory myInven)
    {
        // 1. 기존 슬롯 초기화 (새로고침을 위해 모두 삭제)
        foreach (Transform child in inventoryContent)
        {
            Destroy(child.gameObject);
        }

        // 2. 인벤토리 데이터 전체 탐색 및 UI 생성
        foreach (var item in myInven.items)
        {
            BlockType itemType = item.Key;
            int itemCount = item.Value;

            if (itemCount > 0)
            {
                // 슬롯 생성 및 컴포넌트 가져오기
                GameObject newSlot = Instantiate(slotPrefab, inventoryContent);
                SlotItemPrefab slotUI = newSlot.GetComponent<SlotItemPrefab>();

                // 스프라이트 가져오기: Enum 값을 배열의 Index로 사용
                Sprite itemSprite = itemSprites[(int)itemType];

                // UI 업데이트
                slotUI.ItemSetting(itemSprite, itemCount.ToString());

                // 힌트에 따른 switch 로직 (추가 처리가 필요할 경우 사용)
                // switch (itemType) { ... }
            }
        }
    }
}

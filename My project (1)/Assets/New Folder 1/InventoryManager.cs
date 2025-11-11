using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    // 💡 Inspector에서 연결할 단 두 가지 필수 요소
    public GameObject slotPrefab;       // SlotItemPrefab 스크립트가 붙은 UI 프리팹
    public Transform inventoryContent;   // UI 슬롯들이 생성될 부모 패널 (Transform)
    
    // 💡 아이템 타입별 아이콘을 관리하는 배열 (Inspector에서 BlockType 순서대로 연결)
    public Sprite[] itemSprites; 

    // 인벤토리 데이터를 받아 UI를 갱신하는 함수
    public void UpdateInventory(Inventory myInven) 
    {
        // 1. 기존 슬롯 초기화
        foreach (Transform child in inventoryContent) 
        {
            Destroy(child.gameObject);
        }

        // 2. 인벤토리 데이터 탐색 및 UI 생성
        foreach (var item in myInven.items) 
        {
            BlockType itemType = item.Key; // 아이템 타입 (Enum)
            int itemCount = item.Value;    // 아이템 개수

            if (itemCount > 0) 
            {
                // 슬롯 생성 및 컴포넌트 가져오기
                GameObject newSlot = Instantiate(slotPrefab, inventoryContent);
                SlotItemPrefab slotUI = newSlot.GetComponent<SlotItemPrefab>();
                
                // 스프라이트 가져오기: Enum 값을 배열의 Index로 사용 (가장 간단한 매핑 방법)
                // (주의: BlockType enum 값과 itemSprites 배열 순서가 일치해야 함)
                Sprite itemSprite = itemSprites[(int)itemType];
                
                string itemText = itemCount.ToString();
                
                // UI 업데이트
                slotUI.ItemSetting(itemSprite, itemText);
            }
        }
    }
}

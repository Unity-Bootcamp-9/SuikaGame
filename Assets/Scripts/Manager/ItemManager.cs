using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemManager
{
    public int[] slot = new int[2];

    public bool isHaveRevival { get; private set; }

    public delegate void OnItemSlotChange(int slotIndex);
    public event OnItemSlotChange OnItemSlotChangeEvent;

    public delegate void OnRevivalUse(bool isHavaeRevival);
    public event OnRevivalUse OnRevivalToggleEvent;

    public int currentUsingItem;

    public void Init()
    {
        // 모든 슬롯을 -1로 초기화해서 비어있음을 정의
        for (int i = 0; i < slot.Length; ++i)
        {
            slot[i] = -1;
        }
    }

    public void ItemGet()
    {
        Debug.Log($"Combo count {Managers.ScoreManager.ComboCount}");
        if (Managers.ScoreManager.ComboCount % 5 == 0)
        {
            // 랜덤으로 지정된 아이템
            int selectedItem = UnityEngine.Random.Range(0, Enum.GetValues(typeof(Define.Item)).Length);
            int selectedSlot = Array.IndexOf(slot, -1);

            if (selectedItem == (int)Define.Item.Revive)
            {
                if (!isHaveRevival)
                {
                    isHaveRevival = true;
                    OnRevivalToggleEvent(isHaveRevival);
                }
                return;
            }

            // 아이템 슬롯에 아이템 추가
            // 아이템 슬롯이 꽉차지 않을 때만 실행 (조건에 부합하지 않으면 -1 반환)
            if (selectedSlot > -1)
            {
                slot[selectedSlot] = selectedItem;
                // 이벤트 실행 
                OnItemSlotChangeEvent(selectedSlot);
            }
        }
    }

    public void ItemUse(int slotIndex)
    {
        // 현재 사용된 아이템이 뭔지 체크
        currentUsingItem = slot[slotIndex];

        // 아이템 사용처리
        slot[slotIndex] = -1;
        OnItemSlotChangeEvent(slotIndex);
    }

    public void LevelUpItem(GameObject targetFruit)
    {
        // 과일 데이터 가져오기
        var fruitData = targetFruit.GetComponent<MergeFruit>().fruitData;
        int currentLevel = fruitData.level;
        int maxLevel = Managers.Data.fruits.Length - 1;

        // 최고 레벨인지 확인
        if (currentLevel > maxLevel)
        {
            Debug.Log("최고 레벨 과일");
            return; // 최고 레벨이면 레벨 업 중지
        }

        // 기존 과일 제거
        GameObject.Destroy(targetFruit);

        // 다음 레벨 과일 생성
        int nextLevel = currentLevel;
        Managers.FruitsManager.InstantiateFruit(Managers.Data.fruits[nextLevel], targetFruit.transform.position, true);

        // 선택한 아이템 할당 해제
        currentUsingItem = -1;
    }

    public void DeleteItem(GameObject targetFruit)
    {
        // 터치된 과일의 태그가 "Fruit" 이고 "InBowl"이 True 라면 해당 과일 삭제
        GameObject.Destroy(targetFruit);
        currentUsingItem = -1; // 선택한 아이템 할당해제
    }

    public void RevivalItem()
    {
        if (isHaveRevival)
        {
            isHaveRevival = false;
            OnRevivalToggleEvent?.Invoke(isHaveRevival);
        }
    }

    public void ResetState()
    {
        slot = new int[2];
        for (int i = 0; i < slot.Length; ++i) 
        {
            slot[i] = -1;
        }

        isHaveRevival = false;
        currentUsingItem = -1;
    }
}
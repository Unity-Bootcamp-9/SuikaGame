using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager
{
    public int[] slot = new int[2];

    public bool isHaveRevival { get; private set; }

    public delegate void OnItemSlotChange(int slotIndex);
    public event OnItemSlotChange OnItemSlotChangeEvent;

    public delegate void OnRevivalUse(bool isHavaeRevival);
    public event OnRevivalUse OnRevivalUseEvent;

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
                    OnRevivalUseEvent(isHaveRevival);
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
        Define.Item item = (Define.Item)slot[slotIndex];
        // 아이템 사용 후 UI 업데이트

        switch (item)
        {
            case Define.Item.LevelUp:
                LevelUpItem();
                break;
            case Define.Item.Delete:
                DeleteItem();
                break;
        }

        // 아이템 사용처리
        slot[slotIndex] = -1;
        OnItemSlotChangeEvent(slotIndex);
    }

    void LevelUpItem()
    {
        /*// "Fruits" 자식으로 되어있는 과일들 중 하나를 랜덤 선택하여 현재 레벨 + 1 해줌
        if (Managers.FruitsManager.fruitsParent != null && Managers.FruitsManager.fruitsParent.transform.childCount > 0)
        {
            int randomIndex = Random.Range(0, Managers.FruitsManager.fruitsParent.transform.childCount);
            Transform randomFruit = Managers.FruitsManager.fruitsParent.transform.GetChild(randomIndex);
            FruitsData fruitData = randomFruit.GetComponent<MergeFruit>().fruitData;
            fruitData.level += 1; // 과일 레벨업
        }*/
    }

    void DeleteItem()
    {
        // 터치된 과일의 태그가 "Fruit" 이고 "InBowl"이 True 라면 해당 과일 삭제

    }

    public void RevivalItem()
    {
        if (isHaveRevival)
        {
            isHaveRevival = false;
            OnRevivalUseEvent?.Invoke(isHaveRevival);
        }
    }
}
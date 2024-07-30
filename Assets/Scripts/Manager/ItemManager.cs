using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager
{
    public int[] slot = new int[2];

    public bool isHaveRevival { get; private set; }

    public delegate void OnItemSlotChange(int slotIndex, bool isActive);
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
        if (Managers.ScoreManager.ComboCount % 5 == 0)
        {
            // 랜덤으로 지정된 아이템
            int selectedItem = UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(Define.Item)).Length); // 0 1 2
            int newItemIndex = Array.IndexOf(slot, -1);
            // 아이템 슬롯에 아이템 추가
            // 아이템 슬롯이 꽉차지 않을 때만 실행 (조건에 부합하지 않으면 -1 반환)
            if (newItemIndex > -1)
            {
                slot[newItemIndex] = selectedItem;
                // 이벤트 실행
                OnItemSlotChangeEvent(newItemIndex, true);
            }

            // 회생 아이템애 대한 조건 제어, getItem의 index가 2고 isHaveRevival이 false일 때만 토글처리
            if ((Define.Item)selectedItem == Define.Item.Revive && isHaveRevival == false)
                isHaveRevival = true;
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
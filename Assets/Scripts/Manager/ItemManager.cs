using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager
{
    public string[] slot = new string[2];

    public bool isHaveRevival { get; private set; }

    public delegate void OnItemSlotChange(int slotIndex, bool isActive);
    public event OnItemSlotChange OnItemSlotChangeEvent;

    public delegate void OnRevivalUse(bool isHavaeRevival);
    public event OnRevivalUse OnRevivalUseEvent;

    public void ItemGet()
    {
        if (Managers.ScoreManager.ComboCount % 5 == 0 )
        {
            // 랜덤으로 지정된 아이템
            int selectedItem = UnityEngine.Random.Range(0, 3); // 0 1 2
            int newItemIndex = Array.IndexOf(slot, null);
            // 아이템 슬롯에 아이템 추가
            // 아이템 슬롯이 꽉차지 않을 때만 실행 (조건에 부합하지 않으면 -1 반환)
            if (newItemIndex > -1)
            {
                // 새로 추가할 슬롯의 index
                // 아이템의 이름을 string으로 지정
                switch (selectedItem)
                {
                    case 0:
                        slot[newItemIndex] = "LevelUp";
                    
                        break;
                    case 1:
                        slot[newItemIndex] = "Delete";

                        break;
                }
                // 이벤트 실행
                OnItemSlotChangeEvent(newItemIndex, true);
            }

            // 회생 아이템애 대한 조건 제어, getItem의 index가 2고 isHaveRevival이 false일 때만 토글처리
            if (selectedItem == 2 && isHaveRevival == false)
                isHaveRevival = true;
        }

        //if ( comboCount % 5 == 0 일때 && 슬롯에 아이템이 3개 미만일때 아이템 하나 씩 획득)
            // 아이템 랜덤 획득하는 함수 호출
            // ㄴ>아이템을 배열로 저장 해두고 그중 랜덤으로 하나를 획득
            // 획득한 아이템을 UIInGame에 이미지로 표시 -> 이미지 알파값 조정해서 보이게 하는거 대신, SetActive 사용하는게 직관적일듯
        
    }

    public void ItemUse(int slotIndex)
    {
        string item = slot[slotIndex];
        // 아이템 사용 후 UI 업데이트

        switch (item)
        {
            case "LevelUp":
                LevelUpItem();
                break;
            case "Delete":
                DeleteItem();
                break;
        }

        slot[slotIndex] = null;
        // switch case 문 사용해서 아이템 종류별로 기능 실행 해도 될것 같음..
        // if ( 레벨 업 아이템 사용 시)
        // 그릇에 과일들 생성할때 부모지정해서 부모 밑으로 생성
        // 부모 자식들 중 랜덤으로 하나 선택 현재 레벨에서 +1
        // if (과일 삭제 아이템 사용 시)
        //터치한 태그가 “Fruit” 일때 해당 으브젝트 삭제
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
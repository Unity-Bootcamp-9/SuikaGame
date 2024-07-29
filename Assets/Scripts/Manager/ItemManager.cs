using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager
{
    List<string> items = new List<string>();
    public bool isHaveRevival { get; private set; }

    public void ItemGet()
    {
        if (Managers.ScoreManager.ComboCount % 5 == 0 && items.Count < 3)
        {
            int getItem = Random.Range(0, items.Count);

            switch (getItem)
            {
                case 0:
                    if (!isHaveRevival) // 회생 아이템은 하나만 소지 가능
                    {
                        isHaveRevival = true;
                        Managers.UI.ShowPopupUI<UIInGame>().UpdateItemSlotUI(0, true); // 회생 아이템 UI 업데이트
                    }
                    break;
                case 1:
                    items.Add("LevelUp");
                    Managers.UI.ShowPopupUI<UIInGame>().UpdateItemSlotUI(1, true); // 레벨업 아이템 UI 업데이트
                    break;
                case 2:
                    items.Add("Delete");
                    Managers.UI.ShowPopupUI<UIInGame>().UpdateItemSlotUI(2, true); // 삭제 아이템 UI 업데이트
                    break;
            }
        }

        //if ( comboCount % 5 == 0 일때 && 슬롯에 아이템이 3개 미만일때 아이템 하나 씩 획득)
            // 아이템 랜덤 획득하는 함수 호출
            // ㄴ>아이템을 배열로 저장 해두고 그중 랜덤으로 하나를 획득
            // 획득한 아이템을 UIInGame에 이미지로 표시 -> 이미지 알파값 조정해서 보이게 하는거 대신, SetActive 사용하는게 직관적일듯
        
    }

    public void ItemUse(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= items.Count)
        {
            return;
        }

        string item = items[slotIndex];
        items.RemoveAt(slotIndex);
        Managers.UI.ShowPopupUI<UIInGame>().UpdateItemSlotUI(slotIndex, false); // 아이템 사용 후 UI 업데이트

        switch (item)
        {
            case "LevelUp":
                LevelUpItem();
                break;
            case "Delete":
                DeleteItem();
                break;
        }
        // switch case 문 사용해서 아이템 종류별로 기능 실행 해도 될것 같음..
        // if ( 레벨 업 아이템 사용 시)
        // 그릇에 과일들 생성할때 부모지정해서 부모 밑으로 생성
        // 부모 자식들 중 랜덤으로 하나 선택 현재 레벨에서 +1
        // if (과일 삭제 아이템 사용 시)
        //터치한 태그가 “Fruit” 일때 해당 으브젝트 삭제

    }

    void LevelUpItem()
    {
        // "Fruits" 자식으로 되어있는 과일들 중 하나를 랜덤 선택하여 현재 레벨 + 1 해줌
        GameObject fruitsParent = GameObject.Find("Fruits");
        if (fruitsParent != null && fruitsParent.transform.childCount > 0)
        {
            int randomIndex = Random.Range(0, fruitsParent.transform.childCount);
            Transform randomFruit = fruitsParent.transform.GetChild(randomIndex);
            FruitsData fruitData = randomFruit.GetComponent<MergeFruit>().fruitData;
            fruitData.level += 1; // 과일 레벨업
        }
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
            Managers.UI.ShowPopupUI<UIInGame>().UpdateItemSlotUI(0, false); // 회생 아이템 UI 업데이트
        }
    }
}
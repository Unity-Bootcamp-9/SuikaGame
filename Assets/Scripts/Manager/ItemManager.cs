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
        }
        //if ( comboCount % 5 == 0 일때 && 슬롯에 아이템이 3개 미만일때 아이템 하나 씩 획득)
            // 아이템 랜덤 획득하는 함수 호출
            // ㄴ>아이템을 배열로 저장 해두고 그중 랜덤으로 하나를 획득
            // 획득한 아이템을 UIInGame에 이미지로 표시 -> 이미지 알파값 조정해서 보이게 하는거 대신, SetActive 사용하는게 직관적일듯
        
    }

    public void ItemUse()
    {
        switch (items.Count)
        {
            case 0:
                // 회생 아이템
                break;
            case 1:
                // 레벨 업 아이템
                break;
            case 2:
                // 삭제 아이템
                break;
        }
        // switch case 문 사용해서 아이템 종류별로 기능 실행 해도 될것 같음..
        // if ( 레벨 업 아이템 사용 시)
        // 그릇에 과일들 생성할때 부모지정해서 부모 밑으로 생성
        // 부모 자식들 중 랜덤으로 하나 선택 현재 레벨에서 +1
        // if (과일 삭제 아이템 사용 시)
        //터치한 태그가 “Fruit” 일때 해당 으브젝트 삭제

    }

    void LevelUPItem()
    {
        // "Fruits" 자식으로 되어있는 과일들 중 하나를 랜덤 선택하여 현재 레벨 + 1 해줌
    }

    void DeleteItem()
    {
        // 터치된 과일의 태그가 "Fruit" 이고 "InBowl"이 True 라면 해당 과일 삭제
    }

    public void RevivalItem()
    {
        isHaveRevival = !isHaveRevival;
    }
}
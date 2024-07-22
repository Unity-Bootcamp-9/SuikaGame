using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInGame : UIPopup
{
    // 이벤트 변경, 할당할 요소 //
    // 텍스트
    enum Texts
    {
    }

    // 버튼
    enum Buttons 
    {
    }

    enum Images
    {
        NextFruitImage
    }

    // TODO: Managers에서 전역으로 관리하도록 처리
    private FruitRandomSpawnManager fruitRandomSpawnManager;

    private Image nextFruitImage;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindImage(typeof(Images));
        fruitRandomSpawnManager = GetComponent<FruitRandomSpawnManager>();

        nextFruitImage = GetImage((int)Images.NextFruitImage);
        if (fruitRandomSpawnManager != null ) 
        {
            fruitRandomSpawnManager.OnChangeRandomEvent += UpdateNextFruitImage;
        }

        return true;
    }

    private void UpdateNextFruitImage(string fruitName)
    {
        // 이미지 업데이트
        Sprite sprite = Resources.Load<Sprite>($"Images/Fruits/{fruitName}");
        if (sprite != null)
        {
            nextFruitImage.sprite = sprite;
        }
        else
        {
            Debug.Log($"스프라이트 없음 : Images/Fruits/{fruitName}");
        }
    }
}

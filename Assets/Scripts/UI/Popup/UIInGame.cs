using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInGame : UIPopup
{
    // �̺�Ʈ ����, �Ҵ��� ��� //
    // �ؽ�Ʈ
    enum Texts
    {
    }

    // ��ư
    enum Buttons 
    {
    }

    enum Images
    {
        NextFruitImage
    }

    // TODO: Managers���� �������� �����ϵ��� ó��
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
        // �̹��� ������Ʈ
        Sprite sprite = Resources.Load<Sprite>($"Images/Fruits/{fruitName}");
        if (sprite != null)
        {
            nextFruitImage.sprite = sprite;
        }
        else
        {
            Debug.Log($"��������Ʈ ���� : Images/Fruits/{fruitName}");
        }
    }
}

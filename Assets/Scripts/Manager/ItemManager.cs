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
                    if (!isHaveRevival) // ȸ�� �������� �ϳ��� ���� ����
                    {
                        isHaveRevival = true;
                        Managers.UI.ShowPopupUI<UIInGame>().UpdateItemSlotUI(0, true); // ȸ�� ������ UI ������Ʈ
                    }
                    break;
                case 1:
                    items.Add("LevelUp");
                    Managers.UI.ShowPopupUI<UIInGame>().UpdateItemSlotUI(1, true); // ������ ������ UI ������Ʈ
                    break;
                case 2:
                    items.Add("Delete");
                    Managers.UI.ShowPopupUI<UIInGame>().UpdateItemSlotUI(2, true); // ���� ������ UI ������Ʈ
                    break;
            }
        }

        //if ( comboCount % 5 == 0 �϶� && ���Կ� �������� 3�� �̸��϶� ������ �ϳ� �� ȹ��)
            // ������ ���� ȹ���ϴ� �Լ� ȣ��
            // ��>�������� �迭�� ���� �صΰ� ���� �������� �ϳ��� ȹ��
            // ȹ���� �������� UIInGame�� �̹����� ǥ�� -> �̹��� ���İ� �����ؼ� ���̰� �ϴ°� ���, SetActive ����ϴ°� �������ϵ�
        
    }

    public void ItemUse(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= items.Count)
        {
            return;
        }

        string item = items[slotIndex];
        items.RemoveAt(slotIndex);
        Managers.UI.ShowPopupUI<UIInGame>().UpdateItemSlotUI(slotIndex, false); // ������ ��� �� UI ������Ʈ

        switch (item)
        {
            case "LevelUp":
                LevelUpItem();
                break;
            case "Delete":
                DeleteItem();
                break;
        }
        // switch case �� ����ؼ� ������ �������� ��� ���� �ص� �ɰ� ����..
        // if ( ���� �� ������ ��� ��)
        // �׸��� ���ϵ� �����Ҷ� �θ������ؼ� �θ� ������ ����
        // �θ� �ڽĵ� �� �������� �ϳ� ���� ���� �������� +1
        // if (���� ���� ������ ��� ��)
        //��ġ�� �±װ� ��Fruit�� �϶� �ش� ������Ʈ ����

    }

    void LevelUpItem()
    {
        // "Fruits" �ڽ����� �Ǿ��ִ� ���ϵ� �� �ϳ��� ���� �����Ͽ� ���� ���� + 1 ����
        GameObject fruitsParent = GameObject.Find("Fruits");
        if (fruitsParent != null && fruitsParent.transform.childCount > 0)
        {
            int randomIndex = Random.Range(0, fruitsParent.transform.childCount);
            Transform randomFruit = fruitsParent.transform.GetChild(randomIndex);
            FruitsData fruitData = randomFruit.GetComponent<MergeFruit>().fruitData;
            fruitData.level += 1; // ���� ������
        }
    }

    void DeleteItem()
    {
        // ��ġ�� ������ �±װ� "Fruit" �̰� "InBowl"�� True ��� �ش� ���� ����
    }

    public void RevivalItem()
    {
        if (isHaveRevival)
        {
            isHaveRevival = false;
            Managers.UI.ShowPopupUI<UIInGame>().UpdateItemSlotUI(0, false); // ȸ�� ������ UI ������Ʈ
        }
    }
}
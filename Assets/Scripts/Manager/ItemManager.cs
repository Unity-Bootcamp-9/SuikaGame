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
            // �������� ������ ������
            int selectedItem = UnityEngine.Random.Range(0, 3); // 0 1 2
            int newItemIndex = Array.IndexOf(slot, null);
            // ������ ���Կ� ������ �߰�
            // ������ ������ ������ ���� ���� ���� (���ǿ� �������� ������ -1 ��ȯ)
            if (newItemIndex > -1)
            {
                // ���� �߰��� ������ index
                // �������� �̸��� string���� ����
                switch (selectedItem)
                {
                    case 0:
                        slot[newItemIndex] = "LevelUp";
                    
                        break;
                    case 1:
                        slot[newItemIndex] = "Delete";

                        break;
                }
                // �̺�Ʈ ����
                OnItemSlotChangeEvent(newItemIndex, true);
            }

            // ȸ�� �����۾� ���� ���� ����, getItem�� index�� 2�� isHaveRevival�� false�� ���� ���ó��
            if (selectedItem == 2 && isHaveRevival == false)
                isHaveRevival = true;
        }

        //if ( comboCount % 5 == 0 �϶� && ���Կ� �������� 3�� �̸��϶� ������ �ϳ� �� ȹ��)
            // ������ ���� ȹ���ϴ� �Լ� ȣ��
            // ��>�������� �迭�� ���� �صΰ� ���� �������� �ϳ��� ȹ��
            // ȹ���� �������� UIInGame�� �̹����� ǥ�� -> �̹��� ���İ� �����ؼ� ���̰� �ϴ°� ���, SetActive ����ϴ°� �������ϵ�
        
    }

    public void ItemUse(int slotIndex)
    {
        string item = slot[slotIndex];
        // ������ ��� �� UI ������Ʈ

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
        // switch case �� ����ؼ� ������ �������� ��� ���� �ص� �ɰ� ����..
        // if ( ���� �� ������ ��� ��)
        // �׸��� ���ϵ� �����Ҷ� �θ������ؼ� �θ� ������ ����
        // �θ� �ڽĵ� �� �������� �ϳ� ���� ���� �������� +1
        // if (���� ���� ������ ��� ��)
        //��ġ�� �±װ� ��Fruit�� �϶� �ش� ������Ʈ ����
    }

    void LevelUpItem()
    {
        /*// "Fruits" �ڽ����� �Ǿ��ִ� ���ϵ� �� �ϳ��� ���� �����Ͽ� ���� ���� + 1 ����
        if (Managers.FruitsManager.fruitsParent != null && Managers.FruitsManager.fruitsParent.transform.childCount > 0)
        {
            int randomIndex = Random.Range(0, Managers.FruitsManager.fruitsParent.transform.childCount);
            Transform randomFruit = Managers.FruitsManager.fruitsParent.transform.GetChild(randomIndex);
            FruitsData fruitData = randomFruit.GetComponent<MergeFruit>().fruitData;
            fruitData.level += 1; // ���� ������
        }*/
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
            OnRevivalUseEvent?.Invoke(isHaveRevival);
        }
    }
}
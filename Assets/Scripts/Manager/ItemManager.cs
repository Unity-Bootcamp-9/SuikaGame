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
        // ��� ������ -1�� �ʱ�ȭ�ؼ� ��������� ����
        for (int i = 0; i < slot.Length; ++i)
        {
            slot[i] = -1;
        }
    }

    public void ItemGet()
    {
        if (Managers.ScoreManager.ComboCount % 5 == 0)
        {
            // �������� ������ ������
            int selectedItem = UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(Define.Item)).Length); // 0 1 2
            int newItemIndex = Array.IndexOf(slot, -1);
            // ������ ���Կ� ������ �߰�
            // ������ ������ ������ ���� ���� ���� (���ǿ� �������� ������ -1 ��ȯ)
            if (newItemIndex > -1)
            {
                slot[newItemIndex] = selectedItem;
                // �̺�Ʈ ����
                OnItemSlotChangeEvent(newItemIndex, true);
            }

            // ȸ�� �����۾� ���� ���� ����, getItem�� index�� 2�� isHaveRevival�� false�� ���� ���ó��
            if ((Define.Item)selectedItem == Define.Item.Revive && isHaveRevival == false)
                isHaveRevival = true;
        }
    }

    public void ItemUse(int slotIndex)
    {
        Define.Item item = (Define.Item)slot[slotIndex];
        // ������ ��� �� UI ������Ʈ

        switch (item)
        {
            case Define.Item.LevelUp:
                LevelUpItem();
                break;
            case Define.Item.Delete:
                DeleteItem();
                break;
        }

        // ������ ���ó��
        slot[slotIndex] = -1;
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
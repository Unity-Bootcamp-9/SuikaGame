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
        // ��� ������ -1�� �ʱ�ȭ�ؼ� ��������� ����
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
            // �������� ������ ������
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

            // ������ ���Կ� ������ �߰�
            // ������ ������ ������ ���� ���� ���� (���ǿ� �������� ������ -1 ��ȯ)
            if (selectedSlot > -1)
            {
                slot[selectedSlot] = selectedItem;
                // �̺�Ʈ ���� 
                OnItemSlotChangeEvent(selectedSlot);
            }
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
        OnItemSlotChangeEvent(slotIndex);
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
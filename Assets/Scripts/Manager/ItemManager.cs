using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemManager
{
    public int[] slot = new int[2];

    public bool isHaveRevival { get; private set; }

    public delegate void OnItemSlotChange(int slotIndex);
    public event OnItemSlotChange OnItemSlotChangeEvent;

    public delegate void OnRevivalUse(bool isHavaeRevival);
    public event OnRevivalUse OnRevivalToggleEvent;

    public int currentUsingItem;

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
                    OnRevivalToggleEvent(isHaveRevival);
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
        // ���� ���� �������� ���� üũ
        currentUsingItem = slot[slotIndex];

        // ������ ���ó��
        slot[slotIndex] = -1;
        OnItemSlotChangeEvent(slotIndex);
    }

    public void LevelUpItem(GameObject targetFruit)
    {
        // ���� ������ ��������
        var fruitData = targetFruit.GetComponent<MergeFruit>().fruitData;
        int currentLevel = fruitData.level;
        int maxLevel = Managers.Data.fruits.Length - 1;

        // �ְ� �������� Ȯ��
        if (currentLevel > maxLevel)
        {
            Debug.Log("�ְ� ���� ����");
            return; // �ְ� �����̸� ���� �� ����
        }

        // ���� ���� ����
        GameObject.Destroy(targetFruit);

        // ���� ���� ���� ����
        int nextLevel = currentLevel;
        Managers.FruitsManager.InstantiateFruit(Managers.Data.fruits[nextLevel], targetFruit.transform.position, true);

        // ������ ������ �Ҵ� ����
        currentUsingItem = -1;
    }

    public void DeleteItem(GameObject targetFruit)
    {
        // ��ġ�� ������ �±װ� "Fruit" �̰� "InBowl"�� True ��� �ش� ���� ����
        GameObject.Destroy(targetFruit);
        currentUsingItem = -1; // ������ ������ �Ҵ�����
    }

    public void RevivalItem()
    {
        if (isHaveRevival)
        {
            isHaveRevival = false;
            OnRevivalToggleEvent?.Invoke(isHaveRevival);
        }
    }

    public void ResetState()
    {
        slot = new int[2];
        for (int i = 0; i < slot.Length; ++i) 
        {
            slot[i] = -1;
        }

        isHaveRevival = false;
        currentUsingItem = -1;
    }
}
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
        //if ( comboCount % 5 == 0 �϶� && ���Կ� �������� 3�� �̸��϶� ������ �ϳ� �� ȹ��)
            // ������ ���� ȹ���ϴ� �Լ� ȣ��
            // ��>�������� �迭�� ���� �صΰ� ���� �������� �ϳ��� ȹ��
            // ȹ���� �������� UIInGame�� �̹����� ǥ�� -> �̹��� ���İ� �����ؼ� ���̰� �ϴ°� ���, SetActive ����ϴ°� �������ϵ�
        
    }

    public void ItemUse()
    {
        switch (items.Count)
        {
            case 0:
                // ȸ�� ������
                break;
            case 1:
                // ���� �� ������
                break;
            case 2:
                // ���� ������
                break;
        }
        // switch case �� ����ؼ� ������ �������� ��� ���� �ص� �ɰ� ����..
        // if ( ���� �� ������ ��� ��)
        // �׸��� ���ϵ� �����Ҷ� �θ������ؼ� �θ� ������ ����
        // �θ� �ڽĵ� �� �������� �ϳ� ���� ���� �������� +1
        // if (���� ���� ������ ��� ��)
        //��ġ�� �±װ� ��Fruit�� �϶� �ش� ������Ʈ ����

    }

    void LevelUPItem()
    {
        // "Fruits" �ڽ����� �Ǿ��ִ� ���ϵ� �� �ϳ��� ���� �����Ͽ� ���� ���� + 1 ����
    }

    void DeleteItem()
    {
        // ��ġ�� ������ �±װ� "Fruit" �̰� "InBowl"�� True ��� �ش� ���� ����
    }

    public void RevivalItem()
    {
        isHaveRevival = !isHaveRevival;
    }
}
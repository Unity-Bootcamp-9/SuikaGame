using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class UITutorial : UIPopup
{
    enum Images
    {
        NormalMode,
        TimeAttackMode
    }

    bool isPause;
    int currentImageIndex = 0; // ���� �̹��� �ε����� ����

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindImage(typeof(Images));

        // ��� �̹����� ��Ȱ��ȭ
        foreach (Images image in Enum.GetValues(typeof(Images)))
        {
            GetImage((int)image).gameObject.SetActive(false);
        }

        // ù ��° �̹����� Ȱ��ȭ
        GetImage(currentImageIndex).gameObject.SetActive(true);

        // ��� �̹����� Ŭ�� �̺�Ʈ ���ε�
        foreach (Images image in Enum.GetValues(typeof(Images)))
        {
            GetImage((int)image).gameObject.BindEvent(() => OnClickScreen());
        }

        return true;
    }

    private void OnClickScreen()
    {
        GetImage(currentImageIndex).gameObject.SetActive(false);

        currentImageIndex++;

        // ��� �̹����� �� �� ��� �˾� ��Ȱ��ȭ
        if (currentImageIndex >= Enum.GetValues(typeof(Images)).Length)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            // ���� �̹����� Ȱ��ȭ
            GetImage(currentImageIndex).gameObject.SetActive(true);
        }
    }
}
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
    int currentImageIndex = 0; // 현재 이미지 인덱스를 저장

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindImage(typeof(Images));

        // 모든 이미지를 비활성화
        foreach (Images image in Enum.GetValues(typeof(Images)))
        {
            GetImage((int)image).gameObject.SetActive(false);
        }

        // 첫 번째 이미지를 활성화
        GetImage(currentImageIndex).gameObject.SetActive(true);

        // 모든 이미지에 클릭 이벤트 바인딩
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

        // 모든 이미지를 다 본 경우 팝업 비활성화
        if (currentImageIndex >= Enum.GetValues(typeof(Images)).Length)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            // 다음 이미지를 활성화
            GetImage(currentImageIndex).gameObject.SetActive(true);
        }
    }
}
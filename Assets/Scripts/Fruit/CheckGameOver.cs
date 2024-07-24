using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckGameOver : MonoBehaviour
{
    private bool inBowl = false;

    public void TopggleInBowl()
    {
        inBowl = !inBowl;
    }

    // TODO: 접시 닿은건 Trigger로 처리
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"충돌됨 {collision.gameObject.tag}");
        if (!inBowl && collision.gameObject.CompareTag("Bowl"))
        {
            inBowl = true;
        }

        if (inBowl && collision.gameObject.CompareTag("Plane"))
        {
            Managers.UI.ShowPopupUI<UIGameOver>().SetDialog(() => { SceneManager.LoadScene(0); }, "GameOver", Managers.ScoreManager.Score.ToString(), Managers.ScoreManager.BestScore.ToString(), "ReStart");
        }
    }
}

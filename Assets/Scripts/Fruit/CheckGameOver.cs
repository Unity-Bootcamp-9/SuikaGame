using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckGameOver : MonoBehaviour
{
    public bool InBowl { get; private set; }
    [SerializeField]
    private bool isRevival = false;

    public void ToggleInBowl()
    {
        InBowl = !InBowl;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!InBowl && collision.gameObject.CompareTag("Bowl"))
        {
            InBowl = true;
        }

        if (InBowl && collision.gameObject.CompareTag("Plane") && Managers.ItemManager.isHaveRevival)
        {
            Managers.SoundManager.Play(Define.Sound.UseItem, "UseItem");

            Destroy(gameObject);
            Managers.ItemManager.RevivalItem();
            isRevival = true;
        }

        if (InBowl && collision.gameObject.CompareTag("Plane") && !isRevival)
        {
            // °úÀÏ Distroy
            foreach (Transform child in Camera.main.transform)
            {
                Destroy(child.gameObject);
            }

            Managers.GameManager.EnableGameOverDialog();            
        }

        if (!InBowl && collision.gameObject.CompareTag("Plane"))
        {
            Destroy(gameObject, 3f);
        }

    }
}

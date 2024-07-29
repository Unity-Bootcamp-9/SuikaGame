using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckGameOver : MonoBehaviour
{
    public bool InBowl { get; private set; }

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

        if (InBowl && collision.gameObject.CompareTag("Plane"))
        {
            Managers.GameManager.EnableGameOverDialog();
        }

        if (!InBowl && collision.gameObject.CompareTag("Plane"))
        {
            Destroy(gameObject, 3f);
        }

        if (InBowl && collision.gameObject.CompareTag("Plane") && !Managers.ItemManager.isHaveRevival)
        {
            Debug.Log("È¸»ý");
            Managers.ItemManager.RevivalItem();
            Destroy(gameObject);
        }
    }
}

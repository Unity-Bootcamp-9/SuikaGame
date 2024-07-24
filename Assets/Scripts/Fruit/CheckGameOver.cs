using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckGameOver : MonoBehaviour
{
    public bool InBowl { get; private set; }

    public void TopggleInBowl()
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

        if (!inBowl && collision.gameObject.CompareTag("Plane"))
        {
            Destroy(gameObject, 3f);
        }
    }
}

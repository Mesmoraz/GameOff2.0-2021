using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitManager : MonoBehaviour
{
    private GameManager gameManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy" && other.gameObject.GetComponent<Enemy>().eggHeld == true)
        {
            gameManager = FindObjectOfType<GameManager>();
            if(gameManager != null)
            {
                gameManager.GameOver();
            }
            else
            {
                Debug.Log("Game Manager not assigned");
            }
        }
    }
   
}

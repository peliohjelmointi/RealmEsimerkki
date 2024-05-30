using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Realms;

public class Collectible : MonoBehaviour
{
    bool isCollected;

    private void Start()
    {
        isCollected = GameManager.gm.GetCollectedStatus();
        
        if (isCollected)
        {
            GameManager.gm.SetCollected();
            gameObject.SetActive(false);
          
        }
    }
}

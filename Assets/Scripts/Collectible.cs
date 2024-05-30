using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    bool isCollected;

    public string id;

    private void Start()
    {
        //Startissa ettei herjaa
        id = GameManager.gm.GetOrCreateCollectibleData(id).Id;      

        isCollected = GameManager.gm.GetCollectedStatus(id);

        if (isCollected)
        {            
            gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Realms;


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
            GameManager.gm.SetCollected(id);
            gameObject.SetActive(false);

        }
    }
}

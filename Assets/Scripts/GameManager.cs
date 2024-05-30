using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Realms;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    public GameObject player;

    Realm realm;

    private void Awake()
    {
        if (gm == null)
        {
            gm = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        if (realm == null)
        {
            realm = Realm.GetInstance(); //initialisoidaan realm
        }
    }

    private void OnDisable()
    {
        if (realm != null)
        {
            realm.Dispose();
        }
    }

    public PlayerData GetOrCreatePlayerData()
    {
        PlayerData playerData = realm.Find<PlayerData>("ks");

        if (playerData == null) //ei löytynyt kyseilellä käyttäjä id:llä
        {
            //luodaan uusi olio PlayerDatasta oletusarvoilla
            realm.Write(() =>
            {
                playerData = realm.Add(new PlayerData()
                {
                    Id = "ks",
                    X = 0,
                    Y = 0
                }); 
            });
        }
        return playerData;
    }

    public void SavePosition(Vector2 pos)
    {
        PlayerData playerData = GetOrCreatePlayerData();
        realm.Write(() =>
        {
            //mitä kirjoitetaan? kun halutaan saveta pelaajan positio
            playerData.X = pos.x;
            playerData.Y = pos.y;
        });
    }
}

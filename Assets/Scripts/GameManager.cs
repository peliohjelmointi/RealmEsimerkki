using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Realms;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    public GameObject player;
    public TextMeshProUGUI scoreText;

    Realm realm;

    bool isPlayerDataReady;

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

    private void Update()
    {
        if (isPlayerDataReady)
            scoreText.text = GetOrCreatePlayerData().Score.ToString();
    }

    //private void OnDisable()
    //{
    //    if (realm != null)
    //    {
    //        realm.Dispose();
    //    }
    //}

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
                    Y = 0,
                    Score = 0
                });
            });
        }
        isPlayerDataReady = true;
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

    //Luo GetPosition -metodi, joka palauttaa Vector2-position playerData:sta

    //Tee toiminallisuus, että kun pelin käynnistää,
    //pelaajan positio haetaan tallennetusta positiosta

    //Testaa!
    public Vector2 GetPosition()
    {
        PlayerData playerData = GetOrCreatePlayerData();
        return new Vector2(playerData.X, playerData.Y);
    }


    //Lisätkää joku kerättävä objekti, ja tehkää toiminnalliuus, 
    // jossa pelaajalla on myös score (teksti ruudulla), ja kun pelaaja kerää objektin,
    // score kasvaa (ja se myös tallennetaan ja haetaan ladattaessa)

    //(itse objekti tulee nyt aina kerättäväksi kun pelin käynnistää, mutta ei haittaa)
    //(jos saat toimimaan, niin koita tehdä vaikka uusi luokka kerättävälle objektille,
    // ja laittaa siihen esim. boolean-arvo, onko esine kerätty vai ei

    public void IncreaseScore(int value)
    {
        PlayerData playerData = GetOrCreatePlayerData();
        realm.Write(() =>
        {
            playerData.Score += value;
        });
    }

    public void SetCollected()
    {
        CollectibleData cd = GetOrCreateCollectibleData();
        realm.Write(() =>
        {
            cd.IsCollected = true;
        });
    }

    public CollectibleData GetOrCreateCollectibleData()
    {
        CollectibleData cd = realm.Find<CollectibleData>("obj1");
        if (cd == null) //ei löytynyt kyseilellä käyttäjä id:llä
        {
            //luodaan uusi olio PlayerDatasta oletusarvoilla
            realm.Write(() =>
            {
                cd = realm.Add(new CollectibleData()
                {
                    Id = "obj1",
                    IsCollected = false
                }); ;
            });
        }
        return cd;
    }

    public bool GetCollectedStatus()
    {
        CollectibleData cd = GetOrCreateCollectibleData();
        return cd.IsCollected;
    }
}

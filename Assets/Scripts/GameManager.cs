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

        if (playerData == null) //ei lˆytynyt kyseilell‰ k‰ytt‰j‰ id:ll‰
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
            //mit‰ kirjoitetaan? kun halutaan saveta pelaajan positio
            playerData.X = pos.x;
            playerData.Y = pos.y;
        });
    }

    //Luo GetPosition -metodi, joka palauttaa Vector2-position playerData:sta

    //Tee toiminallisuus, ett‰ kun pelin k‰ynnist‰‰,
    //pelaajan positio haetaan tallennetusta positiosta

    //Testaa!
    public Vector2 GetPosition()
    {
        PlayerData playerData = GetOrCreatePlayerData();
        return new Vector2(playerData.X, playerData.Y);
    }
    

    //Lis‰tk‰‰ joku ker‰tt‰v‰ objekti, ja tehk‰‰ toiminnalliuus, 
    // jossa pelaajalla on myˆs score (teksti ruudulla), ja kun pelaaja ker‰‰ objektin,
    // score kasvaa (ja se myˆs tallennetaan ja haetaan ladattaessa)

    //(itse objekti tulee nyt aina ker‰tt‰v‰ksi kun pelin k‰ynnist‰‰, mutta ei haittaa)
    //(jos saat toimimaan, niin koita tehd‰ vaikka uusi luokka ker‰tt‰v‰lle objektille,
    // ja laittaa siihen esim. boolean-arvo, onko esine ker‰tty vai ei
}

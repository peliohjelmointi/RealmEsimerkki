using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float direction;
    Rigidbody2D player;

    public float speed = 5f;

    private void Awake()
    {
        player = GetComponent<Rigidbody2D>();        
    }

    private void Start()
    {
        player.position = GameManager.gm.GetPosition();
    }

    void Update()
    {
        direction = Input.GetAxis("Horizontal");

        if (direction > 0f)
        {            
            player.velocity = new Vector2(direction * speed, player.velocity.y);
            //ruma ratkaisu(raskas kun tallentelee koko ajan ->siirrä esim. Save-buttoniin)
            GameManager.gm.SavePosition(player.position);
        }
        else if (direction < 0f)
        {
            player.velocity = new Vector2(direction * speed, player.velocity.y);
            //ruma ratkaisu(raskas kun tallentelee koko ajan ->siirrä esim. Save-buttoniin)
            GameManager.gm.SavePosition(player.position);
        }
        else
        {
            player.velocity = new Vector2(0, player.velocity.y);
        }

       
    }
}

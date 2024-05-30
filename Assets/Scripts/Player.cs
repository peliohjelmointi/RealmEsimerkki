using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float horizontalMovement;
    float verticalMovement;
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

    void FixedUpdate()
    {
        horizontalMovement = Input.GetAxis("Horizontal");
        verticalMovement = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(horizontalMovement, verticalMovement);

        player.velocity = new Vector2(movement.x, movement.y) * speed;

        GameManager.gm.SavePosition(player.position);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.gm.IncreaseScore(1);
        GameManager.gm.SetCollected(collision.GetComponent<Collectible>().id);
        Destroy(collision.gameObject);
    }


}

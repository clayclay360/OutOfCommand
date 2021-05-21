using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_controller : MonoBehaviour
{
    [Header("Bullet Variables:")]
    public float speed;

    [Header("Energy Info:")]
    public GameObject energy;

    private game_controller GameController;
    private dialogue_manager DialogueManager;
    private int loot;
    private void Start()
    {
        Destroy(gameObject, 15);
        DialogueManager = FindObjectOfType<dialogue_manager>();//get dialogue manager
        GameController = FindObjectOfType<game_controller>();
    }
    private void Update()
    {
        transform.position = transform.position + transform.right * speed *Time.deltaTime; //move forward by speed
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Player"))
        {
            GameController.GameStarted = false;
            player_controller controller = collision.collider.GetComponentInParent<player_controller>();
            controller.explosion_effect.Play();
            controller.explosion_audio_source.Play();

            collision.collider.gameObject.SetActive(false);
            DialogueManager.game_over();
        }

        if (collision.collider.gameObject.CompareTag("Enemy"))
        {
            loot = Random.Range(0, 2);
            if (loot == 0)
            {
                Instantiate(energy, collision.collider.gameObject.transform.position, Quaternion.identity);
            }
            GameController.score++;
            Destroy(collision.collider.gameObject);
        }

        Destroy(gameObject);
    }
}

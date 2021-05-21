using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rock_controller : MonoBehaviour
{
    [Header("Rock Forward Speed:")]
    public float forward_speed;
    public float forward_speed_min, forward_speed_max;

    [Header("Rock Rotation SPeed:")]
    public float rotation_speed;
    public float rotation_speed_min, rotation_speed_max;

    [Header("Rock Info:")]
    public GameObject rock_sprite_gameobject;

    [Header("Energy Info:")]
    public GameObject energy_gameobject;

    private dialogue_manager DialogueManager;
    private game_controller GameController;
    private int loot;
    void Start()
    {
        forward_speed = Random.Range(forward_speed_min, forward_speed_max);
        rotation_speed = Random.Range(rotation_speed_min, rotation_speed_max);

        rock_sprite_gameobject.GetComponent<GameObject>();
        DialogueManager = FindObjectOfType<dialogue_manager>();//get dialogue manager
        GameController = FindObjectOfType<game_controller>();

        Destroy(gameObject, 25);
    }

    void Update()
    {
        transform.position += -transform.right * forward_speed * Time.deltaTime;
        rock_sprite_gameobject.transform.Rotate(-transform.forward * rotation_speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameController.GameStarted = false;
            player_controller controller = collision.collider.GetComponentInParent<player_controller>();
            controller.explosion_audio_source.Play();
            controller.explosion_effect.Play();

            DialogueManager.game_over();
            collision.collider.gameObject.SetActive(false);
            GameController.score--;
        }

        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
        }

        loot = Random.Range(0, 3);
        if (loot == 0)
        {
            Instantiate(energy_gameobject, transform.position, Quaternion.identity);
        }

        GameController.score++;
        Destroy(gameObject);
    }
}

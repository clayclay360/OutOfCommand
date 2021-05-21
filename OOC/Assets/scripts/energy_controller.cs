using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class energy_controller : MonoBehaviour
{
    [Header("Energy Forward Speed:")]
    public float forward_speed;
    public float forward_speed_min, forward_speed_max;

    private player_controller PlayerController;
    void Start()
    {
        forward_speed = Random.Range(forward_speed_min, forward_speed_max);
        Destroy(gameObject, 25);

        PlayerController = FindObjectOfType<player_controller>();
    }

    void Update()
    {
        transform.position += -transform.right * forward_speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Player"))
        {
            PlayerController.EnergyLevelNumber += 35;
            Destroy(gameObject);
        }
    }
}

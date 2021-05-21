using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_controller : MonoBehaviour
{
    [Header("Player Variables:")]
    public float speed;
    public float fire_rate;
    public int move_cost, fire_cost;

    [Header("Player Components:")]
    public Text command_text;
    public Slider energy_level_slider;
    public GameObject bullet, energy_text_object;
    public Transform muzzle_position;
    [HideInInspector]
    public AudioSource explosion_audio_source;

    private float energy_level_number = 100;
    public float EnergyLevelNumber { get { return energy_level_number; } set { energy_level_number = value; } }

    public ParticleSystem explosion_effect;

    private game_controller GameController;
    private Animator animator;
    private float location_number, direction = 1, target_direction;
    private bool fire_ready = true, energy_text_ready = true;

    void Start()
    {
        animator = GetComponent<Animator>();//get animator
        explosion_audio_source = GetComponent<AudioSource>();//get audio source
        GameController = FindObjectOfType<game_controller>();//get game controller

        location_number = Mathf.Clamp(location_number, 0, 2);//clamp location number
        target_direction = Mathf.Clamp(target_direction, 0, 3);//clamp target direction
    }

    void Update()
    {
        animator.SetFloat("location", location_number);//set animator float location to location number
        movement();//run movement 

        energy_level_number = Mathf.Clamp(energy_level_number, 0, 100);//clamp energy level number
        energy_level_slider.value = energy_level_number;//equal energy level number
    }

    public void movement()
    {
        location_number = Mathf.Lerp(location_number, target_direction, speed);//clamp location number
    }

    public void shoot()
    {
        if (fire_ready)
        {
            Instantiate(bullet, muzzle_position.position, Quaternion.identity);//instantiate bullet
            fire_ready = false;//set false
            StartCoroutine(fire_cooldown());//start fire cooldown coroutine
        }
    }

    IEnumerator fire_cooldown()
    {
        yield return new WaitForSeconds(fire_rate);//wait for seconds
        fire_ready = true;//set true
    }

    public void get_command()
    {
        if (GameController.game_started)
        {
            switch (command_text.text)
            {
                case "/up":
                    if (energy_level_number >= move_cost)
                    {
                        target_direction = location_number + direction;//add one to target direction
                        energy_level_number -= move_cost;//subtract 15
                    }
                    else if (energy_text_ready && energy_level_number < move_cost)
                    {
                        StartCoroutine(energy_text_display());
                    }
                    break;
                case "/down":
                    if (energy_level_number >= move_cost)
                    {
                        target_direction = location_number - direction;//subtract one to target direction
                        energy_level_number -= move_cost;//subtract by fire cost
                    }
                    else if (energy_text_ready && energy_level_number < move_cost)
                    {
                        StartCoroutine(energy_text_display());
                    }
                    break;
                case "/shoot":
                    if (energy_level_number >= fire_cost && fire_ready)
                    {
                        shoot();//run method
                        energy_level_number -= fire_cost;//subtract by fire cost
                    }
                    else if (energy_text_ready && energy_level_number < fire_cost)
                    {
                        StartCoroutine(energy_text_display());
                    }
                    break;
            }
        }
    }

    private IEnumerator energy_text_display()
    {
        energy_text_ready = false;
        energy_text_object.SetActive(true);
        yield return new WaitForSeconds(1.25f);
        energy_text_object.SetActive(false);
        energy_text_ready = true;
    }
}
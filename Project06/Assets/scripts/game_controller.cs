using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class game_controller : MonoBehaviour
{
    public int score;
    public bool game_started = false;
    public bool GameStarted { get { return game_started; } set { game_started = value; } }
    public GameObject game_over_menu;
    public Text score_text; 
    public ParticleSystem human_particle_system;
    [HideInInspector]
    public AudioSource audio_source;

    private bool game_continued;
    private player_controller PlayerController;

    private void Start()
    {
        PlayerController = FindObjectOfType<player_controller>();
        audio_source = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (game_started)
        {
            game_continued = true;
        }

        if(!game_started && game_continued)
        {
            game_over_menu.SetActive(true);
            score_text.text = score.ToString();
        }
    }

    public void start_game()
    {
        human_particle_system.Stop();
        game_started = true;
        PlayerController.EnergyLevelNumber = 100;
        score = 0;
    }

    public void play_particle_system()
    {
        human_particle_system.Play();
    }
}

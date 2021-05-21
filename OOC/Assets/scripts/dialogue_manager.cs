using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dialogue_manager : MonoBehaviour
{
    [Header("Dialogue Info:")]
    public Text dialog_text;
    public Text space_to_continue_text;
    public string speaker_name;
    [TextArea(1, 3)]
    public string[] sentences;
    public float typing_speed, popup_speed;

    [Space]
    [Header("Command Prompt Info:")]
    public Animator command_prompt_animator;

    [Header("Audio Info")]
    public AudioClip upbeat_clip;
    public AudioSource space_interference;

    private int index;
    private bool in_conversation, next_dialogue_ready, end_dialogue;
    private game_controller GameController;

    public void Start()
    {
        GameController = FindObjectOfType<game_controller>();//find gamecontroller
    }
    public void play_dialogue()
    {
        StartCoroutine(dialogue());//start dialogue coroutine
        in_conversation = true;//set true
    }

    private void Update()
    {
        get_space_input();//run method
    }

    private IEnumerator dialogue()
    {
        foreach(char letter in sentences[index])
        {
            dialog_text.text += letter;//plus equal letter (add letter to current sentence)
            yield return new WaitForSeconds(typing_speed);//wait a certain time
        }

        yield return new WaitForSeconds(popup_speed);// wait a certain time
        space_to_continue_text.text = "space to continue";// change the text

        if(index == sentences.Length - 1)
        {
            in_conversation = false;//set to false
            end_dialogue = true;//set to true
        }

        next_dialogue_ready = true;//set to true
        index++;//increment by one
    }

    private void get_space_input()
    {
        if(in_conversation && Input.GetKeyDown(KeyCode.Space) && next_dialogue_ready)
        {
            dialog_text.text = "";// change the text
            space_to_continue_text.text = "";//change the text
            next_dialogue_ready = false;//set to false
            if(index == 2)
            {
                space_interference.Play();
            }
            StartCoroutine(dialogue());//start dialogue coroutine
        }

        if(!in_conversation && end_dialogue && Input.GetKeyDown(KeyCode.Space) && !GameController.game_started)
        {
            dialog_text.text = "";//change the text
            space_to_continue_text.text = "";//change the text
            command_prompt_animator.GetComponent<Animator>();//get the command prompt animator
            command_prompt_animator.SetTrigger("popup");//trigger the popup 
            GameController.game_started = true;//set true
            GameController.audio_source.clip = upbeat_clip;
            GameController.audio_source.volume = .35f;
            GameController.audio_source.Play();
        }

    }

    public void game_over()
    {
        if (!GameController.game_started)
        {
            command_prompt_animator.SetTrigger("popup");//trigger the popup
            GameController.play_particle_system();//run method
        }
    }
}

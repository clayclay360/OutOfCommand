using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn_controller : MonoBehaviour
{
    [Header("Hazards")]
    public GameObject[] hazards;

    [Header("Spawn Variables:")]
    public float wait_time;
    public float wait_time_min, wait_time_max;

    private GameObject current_hazard;
    private int hazard_number;
    private int rock_number;
    private bool game_started, check_ready = true;

    private game_controller GameController;
    private void Start()
    {
        GameController = FindObjectOfType<game_controller>();
    }

    public void Update()
    {
        if (GameController.GameStarted && current_hazard == null && check_ready)
        {
            check_ready = false;
            StartCoroutine(spawn_hazard());
        }

        if(!GameController.GameStarted && current_hazard != null)
        {
            Destroy(current_hazard, 1);
        }
    }

    public IEnumerator spawn_hazard()
    {
        wait_time = Random.Range(wait_time_min, wait_time_max);
        yield return new WaitForSeconds(wait_time);

        hazard_number = Random.Range(0, hazards.Length);
        current_hazard = Instantiate(hazards[hazard_number], transform.position, Quaternion.identity);

        check_ready = true;
    }

}

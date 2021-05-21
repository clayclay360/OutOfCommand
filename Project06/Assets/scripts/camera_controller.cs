using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_controller : MonoBehaviour
{
    [Header("Player Info:")]
    public GameObject player;

    [Header("Offset Info:")]
    public Vector3 offset;

    void Start()
    {
        player.GetComponent<GameObject>(); //get gameobject component
    }

    void Update()
    {
        transform.position = offset + player.transform.position; //set the position to the offset plus the players position
    }
}

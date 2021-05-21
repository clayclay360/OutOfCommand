using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_controller : MonoBehaviour
{
    [Header("Enemy Variables:")]
    public float speed;
    public float fire_rate, fire_rate_min, fire_rate_max;
    public float target_position;

    [Header("Enemy Info")]
    public GameObject bullet;
    public Transform muzzle_transform;

    private bool fire_ready;

    private void Start()
    {
        StartCoroutine(shoot());
    }

    void Update()
    {
        if (transform.position.x != target_position)
        {
            enter_scene();
        }
    }

    void enter_scene()
    {
        transform.position = Vector3.Lerp(transform.position,new Vector2(target_position,transform.position.y),speed);
    }

    IEnumerator shoot()
    {
        fire_rate = Random.Range(fire_rate_min, fire_rate_max);
        yield return new WaitForSeconds(fire_rate);
        Instantiate(bullet, muzzle_transform.position, Quaternion.identity);
        StartCoroutine(shoot());
    }
}

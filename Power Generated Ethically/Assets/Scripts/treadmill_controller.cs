﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class treadmill_controller : MonoBehaviour
{
    public GameObject hurdle, belt;
    public Transform hurdle_spawn, belt_spawn;

    public float minSpeed = 6f;
    public float maxSpeed = 16f;
    public float speedIncrease = 0.5f;

    public Slider speed_slider;

    public float speed; //speed controls the speed of the belt and obstacles
    private float time; //used for tracking time between obstacle spawns
    public float hurdle_interval; //how much time (minimum) do hurdles spawn
    public float randomizer_max_param; //tweakable parameter for randomizer
    private float randomizer;
    

    void Start()
    {
        time = 0.0f;
        randomizer = Random.Range(0, randomizer_max_param); //add a bit of variation of obstacle spawn-time
        speed_slider.maxValue = maxSpeed;
        speed_slider.minValue = minSpeed;
        speed = speed_slider.value;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= hurdle_interval + randomizer) //if time is >= to the spawn-time, then spawn obstacle
        {
            SpawnHurdle();
            time = 0;
            randomizer = Random.Range(0, randomizer_max_param); //reset the randomizer
        }

        // increase treadmill speed
        speed = Mathf.Min(maxSpeed, speed + (speedIncrease * Time.deltaTime));
        speed_slider.value = speed;
        // give treadmill speed to the central script
        CoolerCookieClicker.Instance.treadmillSpeed = speed;
    }

    public void SpawnHurdle()
    {
        Instantiate(hurdle, hurdle_spawn.position, hurdle_spawn.rotation);
    }
    public void SpawnBelt()
    {
        Instantiate(belt, belt_spawn.position, belt_spawn.rotation);
        
    }

    public void Set_Speed(float value)
    {
        speed = value;
    }
}

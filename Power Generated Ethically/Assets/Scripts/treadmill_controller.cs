using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treadmill_controller : MonoBehaviour
{
    public GameObject hurdle, belt;
    public Transform hurdle_spawn, belt_spawn;

    public float speed; //speed controls the speed of the belt and obstacles
    private float time; //used for tracking time between obstacle spawns
    public float hurdle_interval; //how much time (minimum) do hurdles spawn
    public float randomizer_max_param; //tweakable parameter for randomizer
    private float randomizer;
    

    void Start()
    {
        time = 0.0f;
        randomizer = Random.Range(0, randomizer_max_param); //add a bit of variation of obstacle spawn-time
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
    }

    public void SpawnHurdle()
    {
        Instantiate(hurdle, hurdle_spawn.position, hurdle_spawn.rotation);
    }
    public void SpawnBelt()
    {
        Instantiate(belt, belt_spawn.position, belt_spawn.rotation);
        
    }
}

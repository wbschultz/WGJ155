using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treadmill_controller : MonoBehaviour
{
    public GameObject hurdle;
    public Transform hurdle_spawn;

    private float time;
    public float hurdle_interval; //how much time (midpoint) do hurdles spawn
    public float randomizer_max_param;
    private float randomizer;
    // Start is called before the first frame update
    void Start()
    {
        time = 0.0f;
        randomizer = Random.Range(0, randomizer_max_param);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= hurdle_interval + randomizer)
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
}

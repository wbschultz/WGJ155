using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treadmill_belt : MonoBehaviour
{
    public Transform belt_spawn;
    private treadmill_controller treadmill;
    public bool can_spawn;
    // Start is called before the first frame update
    void Start()
    {
        treadmill = GameObject.FindGameObjectWithTag("ground").GetComponent<treadmill_controller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (can_spawn)
        {
            if (belt_spawn.position.x - transform.position.x >= transform.lossyScale.x)
            {
                treadmill.SpawnBelt();
                can_spawn = false;
            }
            
        }
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, 0.2f);
        foreach (Collider2D col in collisions)
        {
            if (col.tag == "belt_end")
            {
                Destroy(gameObject);
            }
        }
        transform.Translate(Vector2.left * Time.deltaTime * treadmill.speed);
    }
}

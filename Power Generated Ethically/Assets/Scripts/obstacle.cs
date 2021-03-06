﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class obstacle : MonoBehaviour
{
    private treadmill_controller treadmill;
    // Start is called before the first frame update
    private void Start()
    {
        treadmill = GameObject.FindGameObjectWithTag("ground").GetComponent<treadmill_controller>();
    }
    // Update is called once per frame
    void Update()
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, 0.2f);
        foreach(Collider2D col in collisions)
        {
            if(col.tag == "line_end")
            {
                Destroy(gameObject);
            }
        }
        transform.Translate(Vector2.left * Time.deltaTime * treadmill.speed);
    }
}

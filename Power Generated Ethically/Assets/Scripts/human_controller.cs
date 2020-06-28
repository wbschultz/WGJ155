using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class human_controller : MonoBehaviour
{

    public Transform ground_detection;
    public float detection_radius ,hurdle_detection_radius;
    private Controls controls;
    private Rigidbody2D rb;

    public float speed_penalty;
    private treadmill_controller treadmill;

    public float jump_force;
    private bool can_jump;
    void Awake()
    {
        treadmill = GameObject.FindGameObjectWithTag("ground").GetComponent<treadmill_controller>();
        rb = GetComponent<Rigidbody2D>();
        controls = new Controls();
        controls.Gameplay.Jump.performed += lmd => Jump();
        
    }
    private void FixedUpdate()
    {
        //Floor detection
        Collider2D[] collisions = Physics2D.OverlapCircleAll(ground_detection.position, detection_radius);
        can_jump = false;
        foreach(Collider2D col in collisions)
        {
            if (col.tag == "ground")
            {
                can_jump = true;
            }
        }

        //Hurdle Detection
        Collider2D[] hurdle_collisions = Physics2D.OverlapBoxAll(transform.position, new Vector2(transform.lossyScale.x * 4 + 0.01f, transform.lossyScale.y * 4), 0);
        foreach (Collider2D col in hurdle_collisions)
        {
            if (col.tag == "hurdle")
            {
                print("COLLIDED!");
                Destroy(col.gameObject);
                if (treadmill.speed - speed_penalty > 5)
                    treadmill.speed -= speed_penalty;
            }
        }
    }
    private void Jump()
    {
        if (can_jump)
        {
            rb.AddForce(Vector2.up * 100 * jump_force);
        }
    }

    private void OnEnable() => controls.Gameplay.Enable();

    private void OnDisable() => controls.Gameplay.Disable();
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(transform.lossyScale.x * 4 + 0.01f, transform.lossyScale.y * 4, 1));
        Gizmos.DrawWireSphere(ground_detection.position, detection_radius);
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class human_controller : MonoBehaviour
{

    public Transform ground_detection;
    public float detection_radius;
    private Controls controls;
    private Rigidbody2D rb;

    public float jump_force;
    private bool can_jump;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controls = new Controls();
        controls.Gameplay.Jump.performed += lmd => Jump();
        
    }
    private void FixedUpdate()
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(ground_detection.position, detection_radius);
        can_jump = false;
        foreach(Collider2D col in collisions)
        {
            if (col.tag == "ground")
            {
                can_jump = true;
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
        Gizmos.DrawSphere(ground_detection.position, detection_radius);
    }
}

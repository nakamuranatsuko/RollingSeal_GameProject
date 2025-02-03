using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Ishiyama : MonoBehaviour
{
    Rigidbody rb;
    public int Speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = transform.forward * Speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.velocity = -transform.forward * Speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity = transform.right * Speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity = -transform.right * Speed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Fish"))
        {
            Speed = Speed + 1;
            Debug.Log("Speed+1");
        }
        if(collision.gameObject.CompareTag("ice"))
        {
            Speed = Speed + 2;
            Debug.Log("Speed+2");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    public Rigidbody rb;

    bool carpa;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }





    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            carpa = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            carpa = false;
        }


    }

    private void FixedUpdate()
    {
        //
        if (carpa)
        {
            rb.velocity = new Vector3(0, -100 * Time.fixedDeltaTime * 7, 0);

        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!carpa)
        {
            rb.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);
        }
        else
        {
            if (collision.gameObject.tag == "enemy")
            {
                Destroy(collision.transform.parent.gameObject);
            }
            else if (collision.gameObject.tag == "plane")
            {
                Debug.Log("GameOver");
            }
           
        }   
    }


    private void OnCollisionStay(Collision collision)
    {
        if (!carpa)
        {
            rb.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);
        }
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTakip : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 cameraPos;
    private Transform player, win;

    private float cameraOffset = 4f;

    private void Awake()
    {
        player = FindObjectOfType<PlayerController>().transform;
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (win == null)
        {
            win = GameObject.Find("win(Clone)").GetComponent<Transform>();
           
        }

        if(transform.position.y>player.position.y && transform.position.y > win.position.y + cameraOffset)
        {
            cameraPos = new Vector3(transform.position.x, player.position.y, transform.position.z);
            transform.position = new Vector3(transform.position.x, cameraPos.y, -5);

        }

    }
}

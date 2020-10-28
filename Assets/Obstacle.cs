using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rigidbody;
    private MeshRenderer meshRenderer;
    private Collider collider;
    private ObstacleController obstacleController;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
        collider = GetComponent<Collider>();
        obstacleController = transform.parent.GetComponent<ObstacleController>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Shatter()
    {
        rigidbody.isKinematic = false;
        collider.enabled = false;

        Vector3 forcePoint = transform.parent.position;
        float parentXpos = transform.parent.position.x;
        float xPos = meshRenderer.bounds.center.x;

        Vector3 subdir = (parentXpos - xPos < 0) ? Vector3.right : Vector3.left;

        Vector3 dir = (Vector3.up *  1.5f + subdir).normalized;


        float force = Random.Range(20, 35);
        float torque = Random.Range(110, 180);

        rigidbody.AddForceAtPosition(dir * force, forcePoint, ForceMode.Impulse);

        rigidbody.AddTorque(Vector3.left * torque);

        rigidbody.velocity = Vector3.down;



    }




}

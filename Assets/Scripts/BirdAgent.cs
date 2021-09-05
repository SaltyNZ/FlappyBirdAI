using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;

public class BirdAgent : Agent
{
    [Header("References")]
    [SerializeField] private Rigidbody rb = null;
    [SerializeField] private PipeScript PipeHandler = null;
    [SerializeField] private Transform bodyTransform = null;

    [Header("Settings")]
    [SerializeField] private float jumpForce = 1.5f;
    [SerializeField] private float maxVelocityMagnitude = 5f;

    private Vector3 startingPos;

    public override void Initialize()
    {
        startingPos= transform.position;
    }

    public override void OnEpisodeBegin()
    {
        transform.position = startingPos;
        rb.velocity = Vector3.zero;

        PipeHandler.ResetPipes();
    }

    public override void OnActionReceived(float[] vetorAction)
    {
        AddReward(0.1f);
        
        if(Mathf.FloorToInt(vetorAction[0]) != 1){return;}
        
        Jump();
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocityMagnitude);
    }

    public override void Heuristic(float[] actionOut)
    {
        actionOut[0] = 0;
        if(!Input.GetKey(KeyCode.Space)) {return;}

        actionOut[0] = 1;
    }

    private void OnTriggerEnter(Collider other) 
    {
        AddReward(-1.0f);
        EndEpisode();   
    }

    private void Update() 
    {
        //bodyTransform.rotation = Quaternion.LookRotation(rb.velocity + new Vector3(10f, 0f, 0f), transform.up);
    }
}

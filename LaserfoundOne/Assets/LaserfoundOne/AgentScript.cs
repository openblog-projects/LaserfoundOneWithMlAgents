using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class AgentScript : Agent{

    //[SerializeField] private Transform targetTransform;

    public override void OnEpisodeBegin(){
        transform.position = Vector3.zero;
    }

    public override void CollectObservations(VectorSensor sensor){
        //agent position
        sensor.AddObservation(transform.position);
        Debug.Log(sensor);
        //sensor.AddObservation(targetTransform.position);
    }

     //receives either float or int values
    public override void OnActionReceived(ActionBuffers actions){
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];
        float moveSpeed = 3f;
        transform.position += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;
    }

    //just for testing
    public override void Heuristic(in ActionBuffers actionsOut){
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }

    private void OnTriggerEnter(Collider other){
        Debug.Log(other);
    }
}

    




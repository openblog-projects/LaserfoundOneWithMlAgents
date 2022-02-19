using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class AgentScript : Agent{

    public bool checkIfRayHitsObject = false;
    
    public override void OnEpisodeBegin()
    {
        transform.position = Vector3.zero;
    }

    //helper method to see if goal gets hit from RayPerceptionSensor
    private void RayCastInfo(RayPerceptionSensorComponent3D rayComponent)
    {
        var rayOutputs = RayPerceptionSensor
                .Perceive(rayComponent.GetRayPerceptionInput())
                .RayOutputs;
 
        if (rayOutputs != null)
        {
            var lengthOfRayOutputs = RayPerceptionSensor
                    .Perceive(rayComponent.GetRayPerceptionInput())
                    .RayOutputs
                    .Length;

            for (int i = 0; i < lengthOfRayOutputs; i++)
            {
                GameObject goHit = rayOutputs[i].HitGameObject;
                if (goHit != null)
                {
                    //am anfang habe ich kein laser (vor erstem hit)
                    //wenn ein hit dann durchgehend laser
                    //ich muss checken wann er immer in die erste if rein geht
                    //bin davon ausgegangen, dass das immer nur dann passiert wenn ray auch wirklich trifft
                    //set bool true if object gets hit
                    checkIfRayHitsObject = true;
                }
                //set bool for ray doesn't hit goal
                else
                {
                    checkIfRayHitsObject = false;
                }
            }
        }
    }

    //Agent observations which come with every (frame/updadte)
    public override void CollectObservations(VectorSensor sensor){
        RayCastInfo(GetComponent<RayPerceptionSensorComponent3D>());
    }

    //gets discrete or continous values for actions
    public override void OnActionReceived(ActionBuffers actions){
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];
        float moveSpeed = 10f;
        transform.position += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;
    }

    //just for testing
    public override void Heuristic(in ActionBuffers actionsOut){
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }
}

    




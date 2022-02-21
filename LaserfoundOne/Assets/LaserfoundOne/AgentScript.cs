using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Timers;
using System.Threading.Tasks;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;



public class AgentScript : Agent
{
    //Notes
    /*
    bevor ich endepisode mache kann ich ja einen timer einbauen

    problem ist dass ich addforce nicht sehe weil direkt endepisode eintrifft nach dem treffen vom laser
    ich könnten einen timer erstellen
    */
    //is true if lila ray moved goal
    public bool checkIfRayMovedGoal = false;

    //is true if rays sensor hits object 
    public bool checkIfRayHitsObject = false;

    //one episode is between Agent.reset and Agent.done
    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(0f, 0f, 0f);
    }

    //helper method to see if goal gets hit from RayPerceptionSensor takes as input ray sensor observations 
    private void RayCastInfo(RayPerceptionSensorComponent3D rayComponent)
    {
        //var contains all the outputs from every ray sensor obseravtions
        var rayOutputs = RayPerceptionSensor
                .Perceive(rayComponent.GetRayPerceptionInput())
                .RayOutputs;
        //if ray output is not null 
        if (rayOutputs != null)
        {
            //ray sensor output is available as array. the var contains the length of this array.
            var lengthOfRayOutputs = RayPerceptionSensor
                    .Perceive(rayComponent.GetRayPerceptionInput())
                    .RayOutputs
                    .Length;

            //goes through the array of ray sensor outputs
            for (int i = 0; i < lengthOfRayOutputs; i++)
            {
                //goHit is the gameobject if goal gets hit it is goal
                GameObject goHit = rayOutputs[i].HitGameObject;
                //if ray sensor hits the goal
                if (goHit != null)
                {
                    //is true if ray sensor hits goal
                    checkIfRayHitsObject = true;
                    //add reward
                    AddReward(1.0f);
                }
                //if ray sensor does not hit the goal
                else
                {
                    checkIfRayHitsObject = false;
                }
            }
        }
    }

    //current environment from the perspective of the agent
    public override void CollectObservations(VectorSensor sensor)
    {
        //adds transform coordinates of the agent into the input of CollectObservations
        sensor.AddObservation(transform.localPosition);
        //input of ray sensor into helper function
        RayCastInfo(GetComponent<RayPerceptionSensorComponent3D>());
    }

    //gets discrete or continous values for actions
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];
        float moveSpeed = 10f;
        transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;
    }

    //controls the agent with certain keyboard input
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }
}






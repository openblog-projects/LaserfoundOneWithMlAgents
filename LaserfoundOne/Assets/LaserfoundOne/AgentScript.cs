using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class AgentScript : Agent{

    public string test = "sssss";

    //[SerializeField] private Transform targetTransform;
    [SerializeField] public RayPerceptionOutput RayPerceptionOutput { get; }
    [SerializeField] public RayPerceptionOutput.RayOutput[] RayOutputs;

    
    public override void OnEpisodeBegin(){
        //Debug.Log(test);


        Debug.Log(transform.GetComponent<RayPerceptionSensorComponent3D>());
        //GetRayPerceptionInput();


        transform.position = Vector3.zero;

       // RayOutputs.GetLength;
    }

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
                    Debug.Log(goHit.name);
                }
            }
        }
    }

    /*void Update() {
         RaycastHit hit;
         if (Physics.Raycast(transform.position, -Vector3.up, out hit))
             hit.transform.SendMessage ("HitByRay");
         
     }*/
    public override void CollectObservations(VectorSensor sensor){
        //this.GetComponent< RayPerceptionSensorComponent3D >().RayPerceptionOutput.RayOutput.HasHit;
       // RayCastInfo(GetComponent<RayPerceptionSensorComponent3D>());
        //rayComponent = GetComponent<RayPerceptionSensorComponent3D>();
        //Debug.Log(RayPerceptionSensor.Perceive(GetComponent<RayPerceptionSensorComponent3D>().GetRayPerceptionInput()).RayOutputs);


        //Debug.Log(transform);
        //RayPerceptionSensorComponent3D;
        //Debug.Log(GetComponent<RayPerceptionSensorComponent3D>().RayPerceptionOutput.RayOutputs);
        //Debug.Log(GetComponent<RayPerceptionSensorComponent3D>().Perceive());
        //Debug.Log(Perceive(transform.GetComponent<RayPerceptionSensorComponent3D>().GetRayPerceptionInput()));
        //Debug.Log(RayPerceptionOutput.RayOutputs);
        //Debug.Log(transform.RayPerceptionOutput);
        //Debug.Log(transform.GetComponent<RayPerceptionSensorComponent3D>().GetRayPerceptionInput().DetectableTags[0]); 
        //Debug.Log(GetComponent<RayPerceptionSensorComponent3D>().GetRayPerceptionInput().DetectableTags[0]); 
        //Debug.Log(transform.GetComponent<RayPerceptionSensorComponent3D>().RayPerceptionOutput); 
        
        //Auflistung von allen möglichen methoden für raycast Sensor:

        //wenn etwas null wird, dann trifft der ray etwas

        //Debug.Log(HasHit);
        //Debug.Log(sensor.HitFraction);

        //sensor.AddObservation(hitObject);
        //sensor.AddObservation(transform.position);
        //transform.
        //Debug.Log(sensor);
        //transform ist mien agent, vielleicht kann ich darauf meine agent methoden aufrufen 
        //Debug.Log(sensor.transform.position);
        //Debug.Log(HitFraction);
        //agent position
        //sensor.AddObservation(HasHit);
        //Debug.Log(DetectableTags);
    }

     //receives either float or int values
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

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("skskksks");
    }

    private void OnTriggerEnter(Collider other){
        Debug.Log("AAAA");
        Debug.Log(other);
    }
}

    




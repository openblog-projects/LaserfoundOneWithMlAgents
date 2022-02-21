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



public class AgentScript : Agent{
    //Notes
    /*
    bevor ich endepisode mache kann ich ja einen timer einbauen

    problem ist dass ich addforce nicht sehe weil direkt endepisode eintrifft nach dem treffen vom laser
    ich könnten einen timer erstellen


    
    */
    //variable for    
    public bool checkLoopEntryForEndEpisode = false;
    public bool checkIfRayMovedGoal = false;
    public int countForEndEpisode = 0;

    public bool checkIfRayHitsObject = false;
    
    public override void OnEpisodeBegin()
    {
        Debug.Log("Kevin");
        this.transform.localPosition = new Vector3(0f, 0f, 0f);
        Debug.Log("Kevin2");
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
                    AddReward(1.0f);
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
        sensor.AddObservation(transform.localPosition);
        RayCastInfo(GetComponent<RayPerceptionSensorComponent3D>());
        //EndEpisode gets in CollectObservations called recursively
        //EndEpisode();
    }

    //gets discrete or continous values for actions
    public override void OnActionReceived(ActionBuffers actions){
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];
        float moveSpeed = 3f;
        transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;



        //i could accumulate reward and than 

       /*if(Convert.ToInt32((DateTime.Now.TimeOfDay - date).TotalSeconds) > 2){
            Debug.Log("Tst");
        }*/
        

        //abfrage ob timer eine certain zeit erreicht hat wenn ja dann endepisode (2sek)

        //wenn der Ray getroffen hat dann kommt es auch automatisch zu einem Treffer vom Laser und daher zum reward. nach jedem reward beginnt die Epsiode neu.

        //ich glaube weil die methode x mal aufgerufen wird...
        countForEndEpisode =+ 1;
        //Debug.Log(countForEndEpisode);
        if(checkIfRayMovedGoal == true /*&& countForEndEpisode == 1 && checkLoopEntryForEndEpisode == false*/){
            checkLoopEntryForEndEpisode = true;
            //var erstellen die es ermöglicht dass man nur einmal in die methode rein geht, solange EndEpisode noch nicht ausgeführt wurde
            checkLoopEntryForEndEpisode = true;
            Debug.Log("Drin");
            //EndEpisode();



            System.Timers.Timer aTimer = new System.Timers.Timer();
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 100;
            aTimer.Enabled = true;
;


            //wenn ich das gegenwärtige problem bekomme dann 


            //ich nehme mir datetime now und speicher in var
            //erstelle if loop mit differenz zwischen var aus step 1 und current wenn > 2sek endepisode check = false
            


            //ich kann auch die zeit von der current date time nehmen und diese in einer var speichern. in einem loop dann immer die differenz zwsichen beiden zeiten abfragen und bei 2sec ednepisode
            //start timer here and 
            /*EndEpisode();
            Task.Delay(new TimeSpan(0, 0, 2)).ContinueWith(o => { EndEpisode(); });
            System.Threading.Tasks.Task.Delay(1000).ContinueWith( (_) => checkLoopEntryForEndEpisode = false );
            System.Threading.Tasks.Task.Delay(1000).ContinueWith( (_) => EndEpisode() );
            System.Threading.Tasks.Task.Delay(1000).ContinueWith( (_) => Debug.Log("HALLALALLLALLLA") );*/
            countForEndEpisode = 0;
            //wenn es war ist dass das goal von einem laser getroffen wird dann könnte ich beim ersten mal einen timer starten 
            //in jeder runde von actions checke ich dann den satus von dem tmer und bei einer bestoimmten zeit endepisode und checkLoopEntryForEndEpisode = true
        }
        
    }

    public void OnTimedEvent(object source, ElapsedEventArgs e)
    {
        //Debug.Log("DASASASASAS");
        EndEpisode();
        Debug.Log("DASASASASAS");
    }
    //just for testing
    public override void Heuristic(in ActionBuffers actionsOut){
        ActionSegment<float> continuousActions = actionsOut.ContinuousActions;
        continuousActions[0] = Input.GetAxisRaw("Horizontal");
        continuousActions[1] = Input.GetAxisRaw("Vertical");
    }
}

    




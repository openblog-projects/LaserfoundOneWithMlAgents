using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    //object for lila ray
    private LineRenderer lr;

    //says if lila ray is enabled or not
    private bool checkForReach = false;

    //reference to my AgentScript
    AgentScript agentScript;

    //reference to my agent gameobject serialize field == private but still visible in editor
    [SerializeField] GameObject agent;

    //reference to the goal gameobject
    [SerializeField] GameObject goal;

    //rigidbody object
    public Rigidbody goalRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        agentScript = agent.GetComponent<AgentScript>();
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //lila ray gets enabled if ray sensor from agents hits goal and the variable which confirms is set to true
        if (agentScript.checkIfRayHitsObject == true)
        {
            this.GetComponent<LineRenderer>().enabled = true;
            checkForReach = true;
        }
        //every time the ray sensor from agents doesn't hit a goal the lila ray gets disabled and the confirming variable is set to false
        else
        {
            this.GetComponent<LineRenderer>().enabled = false;
            checkForReach = false;
        }
        //position from lila ray on the agent object
        lr.SetPosition(0, transform.parent.position);
        //hit object from lila ray
        RaycastHit hit;
        //if something gets hit (not clear if collider or not) 
        if (Physics.Raycast(transform.parent.position, transform.forward, out hit))
        {
            //checks if the hitted object has collider
            if (hit.collider)
            {
                //if collider object was the goal and the lila ray is enabled
                if (hit.collider.gameObject.name == "Goal" && checkForReach == true)
                {
                    //get rigidbody of goal
                    goalRigidbody = GameObject.Find("Goal").GetComponent<Rigidbody>();
                    //move goal with force
                    goalRigidbody.AddForce(0, 0, 1, ForceMode.Impulse);
                    //variable is true if goal was moved
                    agentScript.checkIfRayMovedGoal = true;
                }
                //if the goal was not moved var is false
                else agentScript.checkIfRayMovedGoal = false;
                //laser position between agent and hitted object
                lr.SetPosition(1, hit.point);
            }
        }
        //if no object is hit ray is "infinite long"
        else lr.SetPosition(1, transform.forward * 5000);
    }
}

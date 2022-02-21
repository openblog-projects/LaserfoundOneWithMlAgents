using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private LineRenderer lr;

    private bool checkForReach = false;

    //reference to my AgentScript
    AgentScript agentScript;

    //reference to my agent gameobject. serialize field == private but still visible in editor
    [SerializeField] GameObject agent;

    //reference to the goal gameobject
    [SerializeField] GameObject goal;

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
        //lila ray gets enabled 
        if(agentScript.checkIfRayHitsObject == true){
            this.GetComponent<LineRenderer>().enabled = true;
            checkForReach = true;
        }
        else {
            this.GetComponent<LineRenderer>().enabled = false;
            checkForReach = false;
        }

        lr.SetPosition(0, transform.parent.position);
        RaycastHit hit; 
        if(Physics.Raycast(transform.parent.position, transform.forward, out hit)){
            if(hit.collider){
                if(hit.collider.gameObject.name == "Goal" && checkForReach == true){
                    goalRigidbody = GameObject.Find("Goal").GetComponent<Rigidbody>();
                    goalRigidbody.AddForce(0,0,1,ForceMode.Impulse);
                    agentScript.checkIfRayMovedGoal = true;
                }
                else agentScript.checkIfRayMovedGoal = false;
                lr.SetPosition(1, hit.point);
            }
        }
        else lr.SetPosition(1, transform.forward*5000);
    }
}

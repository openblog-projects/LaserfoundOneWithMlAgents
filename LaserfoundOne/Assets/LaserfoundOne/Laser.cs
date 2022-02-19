using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private LineRenderer lr;

    //reference to my AgentScript
    AgentScript agentScript;

    //reference to my agent gameobject. serialize field == private but still visible in editor
    [SerializeField] GameObject agent;

    //reference to the goal gameobject
    [SerializeField] GameObject goal;

    public Rigidbody goalRigidbody;

//    rd = goal.GetComponent<RigidBody>();

    // Start is called before the first frame update
    void Start()
    {   

        //goalRigidbody.AddForce(5, 5, 5);
        //goal.GetComponent<Rigidbody>().AddForce(0, 0, 1);
        //Debug.Log(goal.GetComponent<Rigidbody>());        
        //before assigning data i wanna assign variables from other scripts
        agentScript = agent.GetComponent<AgentScript>();
        lr = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log
        //ich muss kinematic disablen weil ich sonst keine kraft auf mein goal ausüben kann, deswegen fliegt mein goal dann aber durch die Plattform
        //nach einem thread aus unity forum muss ich einen capsule collider nutzen um trozdem addforce hinzufügen zu können

        //ich muss kinematic bei meinem goal an lassen, weil er sonst durch die plattform fliegt, daher kann ich kein gravity basierendes addforce benutzen
        //ich muss etwas anderes finden für die bewegung

        //1. funktion von laser mit radius
        //2. funktion von laser mit radius und gravity
        //3. git push 

        //kann ich nicht einfach immer direkt disablen und enablen based on variable
        if(agentScript.checkIfRayHitsObject == true){
            this.GetComponent<LineRenderer>().enabled = true;
        }
        else this.GetComponent<LineRenderer>().enabled = false;
        //Debug.Log(agentScript.checkIfRayHitsObject);

        lr.SetPosition(0, transform.parent.position);
        RaycastHit hit; 
        if(Physics.Raycast(transform.parent.position, transform.forward, out hit)){
            if(hit.collider){
                //here das collision mit addforce bearbeiten
                Debug.Log("tets");
                goalRigidbody = GameObject.Find("Goal").GetComponent<Rigidbody>();
                //ich kann https://docs.unity3d.com/ScriptReference/Transform-position.html transform.position verwenden, um mein objekt zu bewegen
                //frage ist bloß ob ich die methode auf meinen rigidbody oder auf meinem gameobject aufrufen muss?
                //ich kann mit einem log einfach testen ob ich von rigidbody oder dem gameobject eine position bekomme, da wo ich sie bekomme, dass ist richtig
                //ich kann auf den rigidbody position zugreifen
                goalRigidbody.AddForce(0,0,2,ForceMode.Impulse);
                lr.SetPosition(1, hit.point);
            }
        }
        else lr.SetPosition(1, transform.forward*5000);
    
        /*if(agentScript.checkIfRayHitsObject == true){
            //his.GetComponent<LineRenderer>().enabled = true;
            lr.SetPosition(0, transform.parent.position);
            RaycastHit hit; 
            if(Physics.Raycast(transform.parent.position, transform.forward, out hit)){
                if(hit.collider){
                    //here das collision mit addforce bearbeiten
                    lr.SetPosition(1, hit.point);
                }
            }
            else lr.SetPosition(1, transform.forward*5000);
        }*/
        /*
        //if goal gets hit from ray sensor make ray (linerenderer visible)
        if(agentScript.checkIfRayHitsObject == true){

            /* testing von laser vorrübergehend
            goalRigidbody = GameObject.Find("Goal").GetComponent<Rigidbody>();
            goalRigidbody.AddForce(0,0,2,ForceMode.Impulse);
            this.GetComponent<LineRenderer>().enabled = true;
            lr.SetPosition(1, transform.parent.position);
            RaycastHit hit; 
            if(Physics.Raycast(transform.parent.position, transform.forward, out hit)){
                if(hit.collider){
                    lr.SetPosition(1, hit.point);
                }
            }
            //else lr.SetPosition(1, transform.forward*5000);
            //else this.GetComponent<LineRenderer>().enabled = false;
        }
        else
        {
            //else disable ray
            this.GetComponent<LineRenderer>().enabled = false;
        }*/
    }
}

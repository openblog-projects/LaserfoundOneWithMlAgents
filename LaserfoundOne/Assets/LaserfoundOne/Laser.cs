using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private LineRenderer lr;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        //Debug.Log(transform.parent.position);
    }

    // Update is called once per frame
    void Update()
    {
        
        lr.SetPosition(1, transform.parent.position);

        RaycastHit hit; 
        if(Physics.Raycast(transform.parent.position, transform.forward, out hit)){
            if(hit.collider){
                lr.SetPosition(1, hit.point);
            }
        }
        else lr.SetPosition(1, transform.forward*5000);
    }
}

using System;
using UnityEngine;

public class Test : MonoBehaviour{
    private RaycastHit[] _hits = new RaycastHit[10];
    // Update is called once per frame
    void Update(){
        if (Physics.RaycastNonAlloc(transform.position, transform.forward, _hits, 2)>0){
            
            var normal = _hits[0].normal;
            
            Debug.DrawRay(transform.position, _hits[0].normal*2, Color.cyan);
            Array.Clear(_hits,0,_hits.Length);
        }
        
        Debug.DrawRay(transform.position, transform.forward*2, Color.green);
    }
}

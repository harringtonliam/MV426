using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointCircuit : MonoBehaviour
{
    //Parameters 
    [SerializeField] GameObject waypointCircuit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<Transform> GetWayPoints()
    {
        var waveWayPoints = new List<Transform>();
        foreach (Transform child in waypointCircuit.transform)
        {
            waveWayPoints.Add(child);
        }
        return waveWayPoints;
    }
}

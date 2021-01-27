using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointPathing : MonoBehaviour
{
    //parameters

    [SerializeField] float moveSpeed = 100f;
    [SerializeField] WaypointCircuit waypointCircuit;
    [SerializeField] float smoothRotation = 100f;

    //Member variables
    int waypointIndex = 0;
    List<Transform> waypoints;
    //for camera rotation
    Quaternion targetRotation;


    // Start is called before the first frame update
    void Start()
    {
        waypoints = waypointCircuit.GetWayPoints();
        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
    }



    private void Move()
    {
        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[waypointIndex].transform.position;
      
            var movementthisFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementthisFrame);
            if (transform.position == targetPosition)
            {
                waypointIndex++;
                PointCamera();
            }
        }
        else
        {
            waypointIndex = 0;
        }
    }

    private void Rotate()
    {
        transform.rotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, smoothRotation * Time.deltaTime);
    }

    private void PointCamera()
    {
        if (waypointIndex > waypoints.Count -1)
        {
            waypointIndex = 0;
        }

        var targetPosition = waypoints[waypointIndex].transform.position;
        var currentPosition = transform.position;
        var headingDirection = (targetPosition - currentPosition).normalized;
        var headingChange = Quaternion.FromToRotation(transform.forward, headingDirection);
        targetRotation = transform.localRotation * headingChange;
    }
}

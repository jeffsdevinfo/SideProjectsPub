using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuatTester : MonoBehaviour
{    
    void Start()
    {
        //Quaternion demonstration - rotate a z based vector (0,0,1) 90 degrees on Vector3.up axis
        //  result is (1,0,0)
        //
        //looking down quaternion Axis of rotation from positive to negative direction
        //  positive degrees are clockwise
        //  negative degrees are counterclockwise
        Vector3 vel = new Vector3(0, 0, 1);
        Quaternion plannedRotation = Quaternion.AngleAxis(90, Vector3.up);
        Vector3 result = (plannedRotation * vel);
        Debug.Log($"Result = {result.x}, {result.y}, {result.z}");
    }
}

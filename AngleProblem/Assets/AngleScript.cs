using UnityEngine;

public class AngleScript : MonoBehaviour
{
    [SerializeField] GameObject mover;
    Transform cameraTransform;
    Rigidbody rb;    
    enum Direction { front, back, left, right, none };
    
    Direction spriteDirectionInst; 
    Vector3 intentDirection;

    void Start()
    {        
        rb = mover.GetComponent<Rigidbody>();
        cameraTransform = transform;        
    }

    //Update velocity of mover and calculate player's intentDirection
    // Note* Does not calculate relative direction from player's velocity
    //      Rather, this implementation hard codes the intentDirection for simplicity.
    // 
    // A solution to make it operate on a user's intended direction relative to its velocity 
    //      is listed below.
    //
    // Example to calculate intendedDirection relative to velocity and
    //      then angle between cam and intendedDirection:
    //  Player input chooses to go right from current heading
    //
    //      Quaternion plannedRotation = Quaternion.AngleAxis(90, Vector3.up);    
    //      Vector3 intentDirection = (plannedRotation * rb.velocity.normalized);
    //      Vector2 camFwd = new Vector2(cameraTransform.forward.x, cameraTransform.forward.z);
    //      Vector2 intentDirection2D = new Vector2(intentDirection.x, intentDirection.z).normalized;
    //      float relativeAngleBetweenCamAndPlayerIntentDirection = Vector2.SignedAngle(camFwd, intentDirection2D);
    void Update()
    {
        // controls to move the mover (essentially driving another player for testing)        
        if(Input.GetKeyDown(KeyCode.A))
        {
            rb.AddForce(new Vector3(-1, 0, 0),ForceMode.VelocityChange);
            intentDirection = new Vector3(-1, 0, 0); OutputAngle();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            rb.AddForce(new Vector3(1, 0, 0), ForceMode.VelocityChange);
            intentDirection = new Vector3(1, 0, 0); OutputAngle();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            rb.AddForce(new Vector3(0, 0, 1), ForceMode.VelocityChange);
            intentDirection = new Vector3(0, 0, 1); OutputAngle();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            rb.AddForce(new Vector3(0, 0, -1), ForceMode.VelocityChange);
            intentDirection = new Vector3(0, 0, -1); OutputAngle();
        }
       
        //mover velocity visual
        Debug.DrawLine(mover.transform.position, intentDirection.normalized * rb.velocity.normalized.magnitude + mover.transform.position, Color.red, .01f);
    }


    void OutputAngle()
    {        
        Vector2 camFwd = new Vector2(cameraTransform.forward.x, cameraTransform.forward.z);
        Vector2 intendedDirection2D = new Vector2(intentDirection.x, intentDirection.z).normalized;
        float relativeAngle = Vector2.SignedAngle(camFwd, intendedDirection2D);

        //cam fwd visual                
        Debug.DrawLine(cameraTransform.position, cameraTransform.forward.normalized * 20, Color.blue, 2f);    
        
        //determine direction of mover related to cam forward        
        if (relativeAngle > 45 && relativeAngle < 135)
        {
            spriteDirectionInst = Direction.left; // left sprite gets set here
        }
        else if (relativeAngle < -45 && relativeAngle > -135)
        {
            spriteDirectionInst = Direction.right; // right sprite gets set here
        }
        else if (relativeAngle >=-45 && relativeAngle <=45)
        {
            spriteDirectionInst = Direction.back; // back sprite gets set here (headed away from cam)
        }
        else if (relativeAngle <= -135 || relativeAngle >= 135)
        {
            spriteDirectionInst = Direction.front; // front sprite gets set here (headed towards cam)            
        }        
        Debug.Log($"Angle of {relativeAngle} with a heading in the {spriteDirectionInst.ToString()} direction");
    }
}

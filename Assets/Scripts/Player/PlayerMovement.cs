using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    GameObject Player;
    private Animator playerAnims;
    public Quaternion targetRotation;
    public bool IsMoving;

    Rigidbody rBody;
    public float forwardVel;
    float forwardInput, turnInput;
    float inputDelay = 0.1f;
    float rotateVel = 180;

    public Quaternion TargetRotation
    {
        get { return targetRotation; }
    }

    // Use this for initialization
    void Start ()
    {
        Cursor.visible = false;
        Player = GameObject.Find("swat");
        playerAnims = GetComponentInChildren<Animator>();

        if(GetComponentInChildren<Rigidbody>())
        {
            rBody = GetComponent<Rigidbody>();
        }
        else
        {
            Debug.LogError("The character needs a RigidBody!");
        }

        forwardInput = turnInput = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        GetButtonInput();
        GetInput();
        Turn();
	}

    void FixedUpdate()
    {
        Walk();
    }

    void GetInput()
    {
        forwardInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
    }

    void GetButtonInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift) && IsMoving == true)
            {
                forwardVel = 10;
                playerAnims.SetBool("IsRunningForward", true);
                playerAnims.SetBool("IsWalkingForward", false);
                playerAnims.SetBool("IsGoingBackwards", false);
            }
            else
            {
                forwardVel = 2;
                playerAnims.SetBool("IsWalkingForward", true);
                playerAnims.SetBool("IsRunningForward", false);
                playerAnims.SetBool("IsGoingBackwards", false);
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            forwardVel = 1;
            playerAnims.SetBool("IsGoingBackwards", true);
        }
        else
        {
            playerAnims.SetBool("IsWalkingForward", false);
            playerAnims.SetBool("IsRunningForward", false);
            playerAnims.SetBool("IsGoingBackwards", false);
        }
    }

    void Walk()
    {
        if(Mathf.Abs(forwardInput) > inputDelay)
        {
            //move
            rBody.velocity = transform.forward * forwardInput * forwardVel;
            IsMoving = true;
        }
        else
        {
            //zero velocity
            rBody.velocity = Vector3.zero;
            IsMoving = false;
        }
    }

    void Turn()
    {
        if(Mathf.Abs(turnInput) > inputDelay)
        {
            targetRotation *= Quaternion.AngleAxis(rotateVel * turnInput * Time.deltaTime, Vector3.up);
        }
        transform.rotation = targetRotation;
    }
}

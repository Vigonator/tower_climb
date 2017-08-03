using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovement : MonoBehaviour {

    
    public class Status
    {
        public
            bool inFrontOfDoorOutside, inFrontOfDoorInside, entering, exiting, movingToNode, inside, jumping, grounded, canEnter, canExit, onLadder, onNodeOut, onNodeIn, atEdge, knockedBack;

        private Vector3 position;

        CharacterController controller;
        Door door;
        

        public void init (CharacterController  _controller)
        {
            controller = _controller;
        }

        public void update(Vector3 _position)
        {
            position = _position;

            grounded = isGrounded();

            if (grounded && inFrontOfDoorOutside)
            {
                canEnter = true;
            }

            else
            {
                canEnter = false;
            }

            if (grounded && inFrontOfDoorInside)
            {
                canExit = true;
            }

            else
            {
                canExit = false;
            }

            

            if (entering || exiting)
            {
                Debug.Log("entering OR exiting");

                if (Vector3.Distance(position, new Vector3(door.moveNodeOut.transform.position.x, position.y, door.moveNodeOut.transform.position.z)) > 0.1F)
                {
                    onNodeOut = false;

                    Debug.Log(Vector3.Distance(position, new Vector3(door.moveNodeOut.transform.position.x, position.y, door.moveNodeOut.transform.position.z)));
                }
                else
                {
                    onNodeOut = true;
                }

                if (Vector3.Distance(position, new Vector3(door.moveNodeIn.transform.position.x, position.y, door.moveNodeIn.transform.position.z)) > 0.1F)
                {
                    onNodeIn = false;
                }
                else
                {
                    onNodeIn = true;
                }
            }
        }

        public void setDoor(Door _door)
        {
            door = _door;
        }

        private bool isGrounded()
        {


            if (controller.isGrounded)
            {

                return true;
            }

            else if (!jumping)
            {





                RaycastHit hit;
                if (Physics.Raycast(position, Vector3.down, out hit, controller.height / 2 + 0.25F))
                {
                    Debug.Log("Hit " + hit.transform.gameObject);

                    controller.Move(new Vector3(0, -hit.distance, 0));

                    return true;
                }




                else
                {
                    Debug.Log("Failure 1");

                    return false;

                    
                }
            }

            else
            {
                Debug.Log("Failure 2");

                return false;
            }

        }
    }

    public Status status;
    public  float horMov;
    protected GameObject tower, tutW, tutE;
    Rigidbody rb;
    public CharacterController controller;
    LadderScript ladder;
    Door door;
    public HealthBehaviour healthBehaviour;

    public GameObject outCam, inCam, win;

    

    

    public bool inPosition;


    protected int jumpCounter;

    public Vector3 moveDirection = Vector3.zero;
    protected int maxJumps = 2;
    protected float gravity = 25.0F;
    protected float jumpSpeed = 10.0F;
    public float inputSpeed = 7.0F;
    protected float climbSpeed = 3.0F;
    protected float radius, finalSpeed;
    // Use this for initialization
    void Start()
    {
        


        tower = GameObject.FindWithTag("Tower");
        rb = GetComponent<Rigidbody>();

        controller = GetComponent<CharacterController>();

        win.SetActive(false);

        status = new Status();
        status.init(controller);

        healthBehaviour = this.gameObject.GetComponent<HealthBehaviour>();

        radius = Vector3.Distance(this.transform.position, new Vector3(tower.transform.position.x, this.transform.position.y, tower.transform.position.z));

        
    }

    // Update is called once per frame
    void Update() {

        
        this.transform.LookAt(new Vector3(tower.transform.position.x, this.transform.position.y, tower.transform.position.z));

        Debug.Log("Jumping: " + status.jumping);

        finalSpeed = 0;


        if (status.entering)
        {
            enter();
        }

        else if (status.exiting)
        {
            exit();
        }



        else
        {
            jump();

            getFinalSpeed();
            move(finalSpeed);

            gravityAndLadder();

            Debug.Log("Grounded is " + status.grounded);
            
            if (status.canEnter && Input.GetButtonDown("Open"))
            {
                status.entering = true;
                status.movingToNode = true;
            }

            if (status.canExit && Input.GetButtonDown("Open"))
            {
                status.exiting = true;
                status.movingToNode = true;
            }

        }
    }

    private void LateUpdate()
    {
        status.update(this.transform.position);

        if (status.grounded)
        {
            jumpCounter = maxJumps;

            status.jumping = false;
            
        }
    }

    protected virtual void getFinalSpeed()
    {
       
        
    }

    protected virtual void jump()
    {

    }

    protected virtual void edgeEnterBehaviour()
    {

    }

    protected void move(float speed)
    {
        float angle;
        Vector3 velocity, proposedMovement;
        speed *= Time.deltaTime;

        angle = Mathf.Acos(speed / radius * 2) * Mathf.Rad2Deg;



        

        velocity = Vector3.Normalize(new Vector3(tower.transform.position.x, this.transform.position.y, tower.transform.position.z) - this.transform.position);


        if (status.inside)
        {
            velocity = Quaternion.Euler(0, -angle, 0) * velocity;
        }

        else
        {
            velocity = Quaternion.Euler(0, angle, 0) * velocity;
        }

        velocity *= speed;

        proposedMovement = new Vector3(velocity.x, moveDirection.y * Time.deltaTime, velocity.z);



        controller.Move(proposedMovement);
    }

    protected virtual void gravityAndLadder()
    {
        if (status.grounded)
        {
            //Stopping gravity
            moveDirection.y = 0F;
        }

        else
            if (status.onLadder)
            {
                moveDirection.y = 0F;
            }

            else
            {
                moveDirection.y -= gravity * Time.deltaTime;

                controller.stepOffset = 0.6F;
            }
    }



    void enter()
    {
        if (!status.onNodeOut && status.movingToNode)
        {
            controller.Move(Vector3.Normalize( new Vector3(door.moveNodeOut.transform.position.x, this.transform.position.y, door.moveNodeOut.transform.position.z) - this.transform.position) * Vector3.Distance(this.transform.position, new Vector3(door.moveNodeOut.transform.position.x, this.transform.position.y, door.moveNodeOut.transform.position.z)) * 3 * Time.deltaTime);
        }

        else if(!status.onNodeIn)
        {
            status.movingToNode = false;
            door.openDoor();

            controller.Move(Vector3.Normalize(new Vector3(door.moveNodeIn.transform.position.x, this.transform.position.y, door.moveNodeIn.transform.position.z) - this.transform.position) * Vector3.Distance(this.transform.position, new Vector3(door.moveNodeIn.transform.position.x, this.transform.position.y, door.moveNodeIn.transform.position.z)) * 3 * Time.deltaTime);

            if (Vector3.Distance(this.transform.position, new Vector3(door.moveNodeIn.transform.position.x, this.transform.position.y, door.moveNodeIn.transform.position.z)) < Vector3.Distance(new Vector3(door.moveNodeOut.transform.position.x, this.transform.position.y, door.moveNodeOut.transform.position.z), new Vector3(door.moveNodeIn.transform.position.x, this.transform.position.y, door.moveNodeIn.transform.position.z)) / 2)
            {
                outCam.SetActive(false);

                inCam.SetActive(true);
            }

        }

        else
        {
            door.closeDoor();

            status.entering = false;

            radius = Vector3.Distance(door.moveNodeIn.transform.position, new Vector3(tower.transform.position.x, door.moveNodeIn.transform.position.y, tower.transform.position.z));
            

            status.inside = true;
        }
    }

    void exit()
    {
        if (!status.onNodeIn && status.movingToNode)
        {
            controller.Move(Vector3.Normalize(new Vector3(door.moveNodeIn.transform.position.x, this.transform.position.y, door.moveNodeIn.transform.position.z) - this.transform.position) * Vector3.Distance(this.transform.position, new Vector3(door.moveNodeIn.transform.position.x, this.transform.position.y, door.moveNodeIn.transform.position.z)) * 3 * Time.deltaTime);
        }

        else if (!status.onNodeOut)
        {
            status.movingToNode = false;
            door.openDoor();

            controller.Move(Vector3.Normalize(new Vector3(door.moveNodeOut.transform.position.x, this.transform.position.y, door.moveNodeOut.transform.position.z) - this.transform.position) * Vector3.Distance(this.transform.position, new Vector3(door.moveNodeOut.transform.position.x, this.transform.position.y, door.moveNodeOut.transform.position.z)) * 3 * Time.deltaTime);

            if (Vector3.Distance(this.transform.position, new Vector3(door.moveNodeOut.transform.position.x, this.transform.position.y, door.moveNodeOut.transform.position.z)) < Vector3.Distance(new Vector3(door.moveNodeIn.transform.position.x, this.transform.position.y, door.moveNodeIn.transform.position.z), new Vector3(door.moveNodeOut.transform.position.x, this.transform.position.y, door.moveNodeOut.transform.position.z)) / 2)
            {
                outCam.SetActive(true);

                inCam.SetActive(false);
            }

        }

        else
        {
            door.closeDoor();

            status.exiting = false;

            radius = Vector3.Distance(door.moveNodeOut.transform.position, new Vector3(tower.transform.position.x, door.moveNodeOut.transform.position.y, tower.transform.position.z));

            status.inside = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Edge"))
        {


            status.atEdge = true;

            edgeEnterBehaviour();
        }

        if (other.CompareTag("LadderArea"))
        {
            status.onLadder = true;

            

            ladder = other.gameObject.GetComponentInParent<LadderScript>();

            ladder.iconOn(this.transform.position.y + 1.5F);

            
        }

        if (other.CompareTag("DoorAreaOut"))
        {
            status.inFrontOfDoorOutside = true;

            door = other.gameObject.GetComponentInParent<Door>();

            status.setDoor(door);

            door.iconOutOn(this.transform.position.y + 1.5F);
        }

        if (other.CompareTag("DoorAreaIn"))
        {
            status.inFrontOfDoorInside = true;

            door = other.gameObject.GetComponentInParent<Door>();

            status.setDoor(door);

            door.iconInOn(this.transform.position.y + 1.5F);
        }

        if (other.CompareTag("WinZone"))
        {
            win.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Edge"))
        {
            status.atEdge = false;
        }

        if (other.CompareTag("LadderArea"))
        {
            status.onLadder = false;


            ladder = other.gameObject.GetComponentInParent<LadderScript>();

            ladder.iconOff();
        }

        if (other.CompareTag("DoorAreaOut"))
        {
            status.inFrontOfDoorOutside = false;

            door = other.gameObject.GetComponentInParent<Door>();

            door.iconOutOff();
        }

        if (other.CompareTag("DoorAreaIn"))
        {
            status.inFrontOfDoorInside = false;

            door = other.gameObject.GetComponentInParent<Door>();

            door.iconInOff();
        }
    }

    
}

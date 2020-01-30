using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : MonoBehaviour
{
    //Player Movement Variables
    //public AudioSource walk;
    private CharacterController controller = null;
    private Animator animator = null;
    public AnimationClip throwClip;
    public bool canWalk;
    [SerializeField] private float movementSpeed = 5f;
    private float currentSpeed = 0f;
    private float speedSmoothVelocity = 0f;
    private float speedSmoothTime = 0.1f;
    private float rotationSpeed = 0.1f;
    private float gravity = 3f;

    //Camera Variables
    [SerializeField] private Camera player1Camera;
    private Transform mainCameraTransform = null;

    //Parcel Variables
    public Transform parcelSpawnPoint;
    public Transform parcelThrowPoint;
    public float packageForce = 200f;
    private bool collectedParcel;
    public bool holdingParcel;
    public float releaseTime;
    [SerializeField] private GameObject[] parcels;
    private GameObject currentParcel;
    public bool houseFound;
    public int index;
    public Vector3 intial;


    //GameManager 
    [SerializeField] private GameManager gameManager;
    [SerializeField] private HouseAndVanSelect houseSelect;
    [SerializeField] private Player1VanController van;
    void Start()
    {
        //lineRenderer.positionCount = segments;
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();  
        mainCameraTransform = player1Camera.transform;

        //Bool setup
        animator.SetBool("IsWalking", true);
        animator.SetBool("IsThrowing", false);
        animator.SetBool("IsRunning", false);
        //animator.SetBool("IsIdle", false);
        canWalk = true;
        collectedParcel = false;
        holdingParcel = false;
        houseFound = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!PauseMenu.gameIsPaused)
        {
            Move();

            if (!holdingParcel && collectedParcel)
            {
                SpawnParcel();
            }

            if (houseFound && holdingParcel && Input.GetKeyDown("joystick 1 button 2")) //If they player is holding a parcel and tries to throw it
            {
                StartCoroutine(PlayParcelThrowAnim(throwClip.length));
            }
        }
    }


    private void SpawnParcel()
    {
            Transform currentParcelTransform; 
            index = Random.Range(0, parcels.Length); //Selects a random parcel from the array
            Instantiate(parcels[index], parcelSpawnPoint); //Spawns the parcel on the player    
            currentParcelTransform = parcelSpawnPoint.GetChild(0); //Sets the current parcel transform as the spawn point child (spawned parcel)
            currentParcel = currentParcelTransform.gameObject; //Sets the current parcel as the transforms gameobject
            currentParcel.name = "holding-" + parcels[index];
            holdingParcel = true; //Set to holding an object to be true
    }

    private void Move()
    {
        
        Vector2 movementInput = new Vector2(Input.GetAxisRaw("Horizontal1"), Input.GetAxisRaw("Vertical1"));
        Vector3 forward = mainCameraTransform.forward;
        Vector3 right = mainCameraTransform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();
        Vector3 desiredMoveDirection = (forward * movementInput.y + right * movementInput.x).normalized;
        Vector3 gravityVector = Vector3.zero;


        if(canWalk)//if player can walk
        {

            if (!controller.isGrounded) //If the player isn't on a surface 
            {
                gravityVector.y -= gravity;
            }

            if (desiredMoveDirection != Vector3.zero) //if not standing still / if moving
            {
                
                if (Input.GetAxis("Drive1") > 0) // if right trigger down
                {
                    animator.SetBool("IsRunning", true); // move to run state
                    movementSpeed = 3f;
                }
                else
                {
                    //walk.Play();
                    animator.SetBool("IsWalking", true); // move to walking state
                    animator.SetBool("IsRunning", false); // disable running state
                    movementSpeed = 1.5f;
                }                
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(desiredMoveDirection), rotationSpeed);
            }
            else
            {
                //walk.Stop();
                animator.SetBool("IsRunning", false);
                animator.SetBool("IsWalking", false); //if still use idle anim
                animator.SetBool("IsIdle", false);
            }

            float targetSpeed = movementSpeed * movementInput.magnitude;
            currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

            controller.Move(desiredMoveDirection * currentSpeed * Time.deltaTime);
            controller.Move(gravityVector * Time.deltaTime);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "player1CurrentHouse")
        {
            //Debug.Log("House Found");
            
            houseFound = true;
        }
        if (other.tag == "Van1Back")
        {
            if (Input.GetKeyDown("joystick 1 button 0") && !collectedParcel)
            {
                collectedParcel = true;
            }
        }
       


    }
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "player1CurrentHouse")
    //    {
    //        //Debug.Log("House Left");
    //        houseFound = false;
    //    }
    //}


    private IEnumerator PlayParcelThrowAnim(float animTime)
    {
        canWalk = false; //TODO: So you can't walk while throwing but due to getting stuck in animation you cant walk at all
        collectedParcel = false; //Make it so they can collect another parcel
        holdingParcel = false; //Make it so they aren't holding the parcel  
        houseSelect.player1HouseAssigned = false; //TODO: Make throw
        houseSelect.player1currentHouse.gameObject.tag = "House";
        houseSelect.player1currentHouse = null;
        gameManager.player1Score += 1;
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsRunning", false);
        animator.SetBool("IsThrowing", true);
        
        yield return new WaitForSeconds(releaseTime);
        Destroy(currentParcel); //TODO: Needs a delay for after throwing - Destroys parcel in hand.
        ThrowPackage(); //Throws new instance of same package from hand spawn point
        yield return new WaitForSeconds(animTime - releaseTime);

        animator.SetBool("IsThrowing", false);
        canWalk = true;
    }

    private void ThrowPackage() //TODO Might change from transform to vector3
    {
        Transform currentParcelTransform;
        Instantiate(parcels[index], parcelThrowPoint);
        currentParcelTransform = parcelThrowPoint.GetChild(0);
        currentParcel = currentParcelTransform.gameObject; //Sets the current parcel as the transforms gameobject
        currentParcel.name = "Parcel-" + parcels[index] + "-" + "player1";
        currentParcel.transform.parent = null;
        currentParcel.AddComponent<ParcelCollider>();
        currentParcel.AddComponent<Rigidbody>();
        currentParcel.AddComponent<BoxCollider>();
        currentParcel.GetComponent<Rigidbody>().AddForce(transform.forward * packageForce);
        intial = (transform.forward * packageForce);
        houseSelect.player1HouseAssigned = false;
        houseFound = false;
    }
}
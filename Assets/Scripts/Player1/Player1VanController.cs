using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Player1VanController : MonoBehaviour
{
    //Game Manager
    GameManager gameManager;

    //Sound and SFX Variables 

    public GameObject smoke;
    public AudioSource engine;

    //Player Variables
    private Vector3 playerPos;
    private GameObject player;
    public Player1Controller player1Controller;
    [SerializeField] Transform exit;

    //Van Variables
    public bool inVan = false;
    private float horizontalInput;
    private float verticalInput;
    private GameObject van;
    public Vector3 com;
    public Rigidbody rb;
    public bool isDamaged = false;
    public bool playerFixing = false;
    private float startTime;
    public GameObject uiHolder;
    public DamageVan damageVan;
    [SerializeField] private WheelCollider frontLeft, frontRight, backLeft, backRight;
    [SerializeField] private Transform FL, FR, BL, BR;
    [SerializeField] private float maxSteerAngle;
    [SerializeField] private float drivingForce;
    [SerializeField] private float maxMotorTorque;
    [SerializeField] private float maxBrakeTorque;
    private bool isBreaking;
    float motor;
    [SerializeField] public RectTransform barForeground;
    public float repairDuration = 5f;
    public bool lockVanInteract;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = com;
        uiHolder = GameObject.FindGameObjectWithTag("Repair1");
        player = GameObject.FindGameObjectWithTag("Player1");
        van = GameObject.FindGameObjectWithTag("Van1");
        uiHolder.gameObject.SetActive(false);
       
    }
    private void FixedUpdate()
    {
        if (!isDamaged)
            smoke.SetActive(false);

        if (isDamaged)
        {
            smoke.SetActive(true);
            if (playerFixing)
            {
                uiHolder.gameObject.SetActive(true);
                if (damageVan.startTime + repairDuration > Time.time)
                {
                    player1Controller.canWalk = false;
                    barForeground.localScale = new Vector3(barForeground.localScale.x + Time.deltaTime / repairDuration, barForeground.localScale.y, barForeground.localScale.z);
                }
                else
                {
                    barForeground.localScale = new Vector3(1, barForeground.localScale.y, barForeground.localScale.z);
                    playerFixing = false;
                    isDamaged = false;
                    uiHolder.gameObject.SetActive(false);
                    player1Controller.canWalk = true;
                }

            }
        }


        if (inVan == true )
        {
            if(isDamaged == false)
            {
                GetInput();
                Steer();

                if (Input.GetAxis("Drive1") > -1 && Input.GetAxis("Drive1") < 0)
                {
                    isBreaking = true;
                }
                else
                {
                    isBreaking = false;
                }
                if (Input.GetKeyDown("joystick 1 button 6")) // Player presses Y
                {
                    OnResetCar();
                }
                Accelerate();
                Break();
            }

            if (Input.GetKeyDown("joystick 1 button 3") && !lockVanInteract) // Temp change to left bumper it broke
            {

               LeaveCar();
                StartCoroutine(WaitOnVanInteract());

            }
        }

        //float tempAngleZ = transform.eulerAngles.y;
        //if (tempAngleZ > 45)
        //{
        //    tempAngleZ = 45;
        //}
        //else if (tempAngleZ < 0)
        //{
        //    tempAngleZ = 0;
        //}


           
        
    }

    private void LeaveCar()
    {
        
        playerPos = exit.transform.position;
        //Needs to get facing position of van so always comes out back TODO
        player.transform.position = playerPos;
        player.SetActive(true);
        inVan = false;
        engine.Stop();

    }
    private void Accelerate()
    {
        if (!isBreaking)
        {
            motor = maxMotorTorque * Input.GetAxis("Drive1"); //Left trigger is represented with -1 to 0, right trigger is 0 to 1
            frontLeft.motorTorque = motor;
            frontRight.motorTorque = motor;
            backLeft.motorTorque = motor;
            backRight.motorTorque = motor;

        }
    }

    private void Break()
    {
        if (isBreaking)
        {
            //TODO: add car render thing (see video on trello)
            backLeft.brakeTorque = maxBrakeTorque;
            backRight.brakeTorque = maxBrakeTorque;
        }
        else
        {
            backLeft.brakeTorque = 0;
            backRight.brakeTorque = 0;
        }

    }

    private void Steer()
    {
        float steering = maxSteerAngle * horizontalInput;
        frontLeft.steerAngle = steering;
        frontRight.steerAngle = steering;

        RotateWheels(FL, frontLeft);
        RotateWheels(FR, frontRight);
        RotateWheels(BL, backLeft);
        RotateWheels(BR, backRight);
    }
    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal1"); // Preset for WASD and Arrow sideways movement under Unity Input
        verticalInput = Input.GetAxis("Vertical1"); // Preset for WASD and Arrow vertical movement under Unity Input
    }

    private void RotateWheels(Transform wheel, WheelCollider wheelCollider)
    {
        Vector3 position;
        Quaternion rotation;

        wheelCollider.GetWorldPose(out position, out rotation); // get position and rotation of wheelCollider

        wheel.position = position;
        wheel.rotation = rotation;
        wheel.Rotate(0, 90, 0); // stupid line fixed weird wheels
    }

    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("overlap");
        if (other.tag == "Player1")
        {

            //Debug.Log("tag");
            if (inVan == false && Input.GetKeyDown("joystick 1 button 3") && !lockVanInteract)
            {
                //Debug.Log("key");
                inVan = true;
                player.SetActive(false);
                engine.Play();
                StartCoroutine(WaitOnVanInteract());
            }

        }
    }
    //DEBUG TO SHOW VAN CENTRE OF MASS

        private IEnumerator WaitOnVanInteract()
    {

        lockVanInteract = true;
        yield return new WaitForSeconds(0.5f);
        lockVanInteract = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + transform.rotation * com, .1f);
    }

    private void OnResetCar()
    {

        transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
        transform.rotation = Quaternion.Euler(0, Random.Range(-180, 80), 0);
    }
}

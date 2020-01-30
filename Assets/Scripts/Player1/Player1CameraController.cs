using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1CameraController : MonoBehaviour
{
    public Transform playerTransform;

    private Vector3 cameraOffset;
    public Transform currentView;
    [SerializeField] Player1VanController vanController;
    [SerializeField] GameObject van;
    Vector3 newPos;
    bool updated;
    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;
    public float height;
    public float zIncrease;
    private Vector3 velocity = Vector3.zero;


    void Start()
    {

    }

    void LateUpdate()
    {
        if (vanController.inVan == true)
        {

            currentView = van.transform;
            height = 8;
            zIncrease = 6;

        }
        else
        {
            currentView = playerTransform;
            height = 6;
            zIncrease = 5;
        }

        moveCamera();

    }

    public void moveCamera()
    {
        Vector3 pos = new Vector3();
        pos.x = currentView.position.x;
        pos.y = currentView.position.y + height;
        pos.z = currentView.position.z - zIncrease;

        

        transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, 0.3f);

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Joystick input;
    public float moveSpeed = 10f;
    public float maxRotation = 25f;

    private Rigidbody rb;
    private float minX, maxX, minY, maxY;
    private Quaternion initialRotation;

    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody>();
       SetUpBoundries();
       initialRotation = transform.localRotation;

    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        RotatePlayer();

        CalculateBoundries();

    }
    private void RotatePlayer()
    {
        float currentX = transform.position.x;
        float newRotationX = 0f;

        if(currentX < 0)
        {
            newRotationX = Mathf.Lerp(0f, -maxRotation, currentX / minX);
        }
        else
        {
            newRotationX = Mathf.Lerp(0f, maxRotation, currentX / maxX);

        }
       // Vector3 currentRotationVector3 = new Vector3(newRotatinX, 0f, 0f);
        //Quaternion newRotation = Quaternion.Euler(currentRotationVector3);
        Quaternion newRotation = initialRotation * Quaternion.Euler(newRotationX, 0f, 0f);

        transform.localRotation = newRotation;
    }
     private void CalculateBoundries()
     { 
          Vector3 currentPosition = transform.position;

          currentPosition.x = Mathf.Clamp(currentPosition.x, minX, maxX);
          currentPosition.y = Mathf.Clamp(currentPosition.y, minY, maxY);

          transform.position = currentPosition;

     }

    private void SetUpBoundries()
    {
        float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        
        Vector2 bottomCorners = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, camDistance));
        Vector2 topCorners = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, camDistance));

        Bounds gameObjectBouds = GetComponent<Collider>().bounds;
        float objectWidth = gameObjectBouds.size.x;
        float objectHeight = gameObjectBouds.size.y;


        minX = bottomCorners.x + objectWidth;
        maxX = topCorners.x - objectWidth;

        minY =  bottomCorners.y + objectHeight;
        maxY = topCorners.y - objectHeight;

        AsteroidManager.Instance.maxX = maxX;
        AsteroidManager.Instance.minX = minX;
        AsteroidManager.Instance.minY = minY;
        AsteroidManager.Instance.maxY = maxY;


    }
    
    private void MovePlayer()
    {
        float horizontalMovement = input.Horizontal;
        float verticalMovement = input.Vertical;
        Vector3 movementVector = new Vector3(horizontalMovement, verticalMovement, 0f);

        rb.velocity = movementVector * moveSpeed;

    }
}

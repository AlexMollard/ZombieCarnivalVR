using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Made By: Steven Pham
//Description: So they can move left, right, Up and Down
  
public class PC_Movement : MonoBehaviour
{
    private float moveHorizontal = 0.5f;
    private float moveForwardAndBack = 0.5f;
    Vector3 Right;
    Vector3 Left;
    Vector3 Up;
    Vector3 Down;

    public float Camera_Move_Speed { get; private set; }

    private void Start()
    {
        Right = new Vector3(transform.position.x + Camera_Move_Speed, transform.position.y, transform.position.z);
        Left = new Vector3(transform.position.x - Camera_Move_Speed, transform.position.y, transform.position.z);
        Up = new Vector3(transform.position.x, transform.position.y, transform.position.z - Camera_Move_Speed);
        Down = new Vector3(transform.position.x, transform.position.y, transform.position.z + Camera_Move_Speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.position = Right;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position = Left;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            transform.position = Up;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            transform.position = Down;
        }

    }
}

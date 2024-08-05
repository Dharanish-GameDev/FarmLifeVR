using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFSW.QC;

public class Ox_Mover : MonoBehaviour
{
    public Vector3 forwardMovement = Vector3.forward; // Initial forward direction
    public float uTurnDuration = 1.0f; // Duration of the U-turn (seconds)
    public float moveSpeed = 5.0f; // Speed of the object
    public float rotationSpeed = 5.0f; // Speed of rotation
    public float uTurnOffset = 2.0f; // Offset in X direction for the U-turn

    private bool isTurning = false;
    private Quaternion targetRotation;
    private Vector3 targetPos;
    private Vector3 initialPosition;

    private void Update()
    {
        if (isTurning)
        {
            // Gradually rotate towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position,targetPos, rotationSpeed * Time.deltaTime);
            // Stop turning when reaching the target rotation
            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                transform.rotation = targetRotation;
                isTurning = false;
                forwardMovement = transform.forward; // Update forward direction after turn
            }
        }
        else
        {
            // Move forward in the current forward direction
            transform.position += forwardMovement * moveSpeed * Time.deltaTime;
        }
    }

    // Call this method to initiate the U-turn
    [Command]
    public void StartUTurn(bool turn180)
    {
        if (!isTurning)
        {
            isTurning = true;

            // Store the initial position for offset calculation
            initialPosition = transform.position;

            // Calculate the 180-degree rotated direction
            Vector3 newForward = -transform.forward;

            // Apply offset in the X direction for turning left or right
            Vector3 offset = transform.right * (turn180 ? -uTurnOffset : uTurnOffset);

            // Set the target direction and rotation for the U-turn
            targetRotation = Quaternion.LookRotation(newForward);

            // Calculate the new position with the offset
            targetPos = initialPosition + offset;
        }
    }
}

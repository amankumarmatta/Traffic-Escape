using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class RotationPosition
{
    public Transform position;
    public float rotationAmount = -90.0f;
}

public class Move : MonoBehaviour
{
    [SerializeField] Transform[] Positions;
    [SerializeField] float objectSpeed;
    [SerializeField] float rotationSpeed; // Rotation speed in degrees per second
    [SerializeField] RotationPosition[] rotationPositions;

    int nextPosIndex;
    Transform nextPos;
    bool shouldDestroy = false;
    bool isMoving = false;
    Vector3 originalPosition;
    Quaternion originalRotation;

    void Start()
    {
        nextPosIndex = 0;
        nextPos = Positions[nextPosIndex];

        // Save the original position of the GameObject
        originalPosition = transform.position;
        // Save the original rotation of the GameObject
        originalRotation = transform.rotation;
    }

    void OnMouseDown()
    {
        if (!isMoving)
        {
            isMoving = true;
        }
    }

    void Update()
    {
        if (isMoving)
        {
            MoveGameObject();
        }
    }

    void MoveGameObject()
    {
        if (shouldDestroy)
        {
            Destroy(gameObject);
            return;
        }

        // Check if the object is at the target position
        if (transform.position == nextPos.position)
        {
            // Check if we have reached a rotation position
            foreach (var rotationPos in rotationPositions)
            {
                if (nextPos == rotationPos.position)
                {
                    Rotate(rotationPos.rotationAmount);
                    return; // Exit the loop after rotating
                }
            }

            // If not at a rotation position, move to the next position
            nextPosIndex = (nextPosIndex + 1) % Positions.Length;

            if (nextPosIndex == 0)
            {
                shouldDestroy = true; // Set the flag to destroy the object after reaching the final position
            }
            else
            {
                nextPos = Positions[nextPosIndex];
            }
        }
        else
        {
            // Calculate the movement direction
            Vector3 moveDirection = (nextPos.position - transform.position).normalized;
            transform.position = Vector3.MoveTowards(transform.position, nextPos.position, objectSpeed * Time.deltaTime);
        }
    }

    void Rotate(float amount)
    {
        Quaternion targetRotation = Quaternion.Euler(0, 0, amount); // Rotate by the specified amount
        float step = rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);

        // Check if the rotation is complete and then proceed to the next position
        if (transform.rotation == targetRotation)
        {
            nextPosIndex = (nextPosIndex + 1) % Positions.Length;

            if (nextPosIndex == 0)
            {
                shouldDestroy = true; // Set the flag to destroy the object after reaching the final position
            }
            else
            {
                nextPos = Positions[nextPosIndex];
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        // Check if the collided GameObject has the tag "Car"
        if (collider.gameObject.CompareTag("Car"))
        {
            // Reset the GameObject's position to its original position
            transform.position = originalPosition;
            // Reset the GameObject's rotation to its original rotation
            transform.rotation = originalRotation;
            isMoving = false; // Stop moving until it's clicked again
            nextPosIndex = 0; // Reset the next position index
            nextPos = Positions[nextPosIndex]; // Set the next position to the initial position
        }
    }
}

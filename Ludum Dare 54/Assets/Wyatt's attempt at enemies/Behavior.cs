using UnityEngine;

public class BoidBehaviour : MonoBehaviour
{
    public BoidController controller;
    public float animationSpeedVariation = 0.2f;
    public float desiredRadius = 5.0f;  // Set your desired radius here
    public float moveSpeed = 2.0f;      // Set your desired movement speed here

    void Start()
    {

    }

    void Update()
    {
        // Ensure the controller is assigned
        if (controller == null) return;

        // Get the position of the controller and the current object
        Vector3 controllerPos = controller.transform.position;
        Vector3 pos = transform.position;

        // Calculate the current distance from the controller
        float distance = Vector3.Distance(pos, controllerPos);

        // Calculate the direction towards or away from the controller
        Vector3 direction;
        if (distance > desiredRadius)
        {
            // If outside the desired radius, move towards the controller
            direction = (controllerPos - pos).normalized;
        }
        else if (distance < desiredRadius)
        {
            // If inside the desired radius, move away from the controller
            direction = (pos - controllerPos).normalized;
        }
        else
        {
            // If already at the desired radius, no movement is needed
            return;
        }

        // Update the position based on the direction and move speed
        transform.position += direction * moveSpeed * Time.deltaTime;
    }
}

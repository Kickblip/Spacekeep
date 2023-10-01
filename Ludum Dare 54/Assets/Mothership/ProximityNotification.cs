using UnityEngine;

public class ProximityNotification : MonoBehaviour
{
    [SerializeField] private GameObject notificationObject;
    [SerializeField] private float notificationHeight = 1.0f;  // The height above the player to display the notification

    private void Start()
    {
        notificationObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered proximity");
            // Position the notification object above the player
            notificationObject.transform.position = other.transform.position + Vector3.up * notificationHeight;

            // Show the notification to the player
            notificationObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player exited proximity");
            // Hide the notification from the player
            notificationObject.SetActive(false);
        }
    }
}

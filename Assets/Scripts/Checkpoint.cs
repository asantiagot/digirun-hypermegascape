using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject restartFromCheckpointButton;
    
    // Commenting this code, this rotates the Cassette Model, but it's not working properly
    // public float rotationSpeed = 50f;

    // void Update()
    // {
    //     transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.Self);
    // }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (gameManager != null)
            {
                gameManager.SetCheckpoint(transform.position);
            }

            if (restartFromCheckpointButton != null)
            {
                restartFromCheckpointButton.SetActive(true);
            }
        }
    }
}

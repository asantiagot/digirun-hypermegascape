using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject restartFromCheckpointButton;
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

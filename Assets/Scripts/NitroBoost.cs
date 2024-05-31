using UnityEngine;

public class NitroBoost : MonoBehaviour
{
    public float boostDuration = 3f;
    public float boostMultiplier = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null)
            {
                Debug.Log("triggering boost speed");
                playerController.ActivateSpeedBoost(boostDuration, boostMultiplier);
                // Destroy(gameObject);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public AudioManager audioManager;
    private bool gameStarted = false;
    public float speed = 5.0f;
    public float laneSpeed = 2.0f;
    public float jumpForce = 1.5f;
    public float gravity = -9.81f;
    public float slideDuration = 2f;
    public float slideHeight = 5f;
    public float heightTransitionSpeed = 5f;

    private CharacterController controller;
    private Vector3 velocity;
    private Animator animator;
    private HealthManager healthManager;
    private ScoreManager scoreManager;
    private bool isGrounded;
    private bool isSliding = false;
    private float originalHeight;

    public GameObject collectible10PtsSparkleEffectPrefab;
    public GameObject collectible20PtsSparkleEffectPrefab;

    public void StartGame()
    {
        gameStarted = true;
        audioManager.Start();
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        originalHeight = controller.height;
        healthManager = GetComponent<HealthManager>();
        scoreManager = GetComponent<ScoreManager>();
    }

    void Update()
    {
        if (gameStarted)
        {
            Move();
            HandleJump();
            HandleSlide();

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }

    void Move()
    {
        isGrounded = controller.isGrounded;
        animator.SetBool("IsGrounded", isGrounded);

        Vector3 move = transform.right * Input.GetAxis("Horizontal") * laneSpeed + transform.forward;
        controller.Move(speed * Time.deltaTime * move);

        // Animation
        float moveSpeed = new Vector3(move.x, 0, move.z).magnitude;
        animator.SetFloat("Speed", moveSpeed);
    }

    void HandleJump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            animator.SetBool("IsJumping", true);
        }
        else {
            animator.SetBool("IsJumping", false);
        }
    }

    void HandleSlide()
    {
        if (isGrounded && !isSliding && Input.GetKeyDown(KeyCode.LeftControl))
        {
            StartCoroutine(Slide());
        }
    }

    private IEnumerator Slide()
    {
        isSliding = true;
        animator.SetBool("IsSliding", true);
        float currentHeight = controller.height;
        float elapsedTime = 0f;

        while (elapsedTime < slideDuration / 2)
        {
            controller.height = Mathf.Lerp(currentHeight, slideHeight, (elapsedTime / (slideDuration / 2)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        controller.height = slideHeight;
        yield return new WaitForSeconds(slideDuration / 2);

        elapsedTime = 0f;
        while (elapsedTime < slideDuration / 2)
        {
            controller.height = Mathf.Lerp(slideHeight, originalHeight, (elapsedTime / (slideDuration / 2)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        controller.height = originalHeight;
        isSliding = false;
        animator.SetBool("IsSliding", false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            StartCoroutine(HandleCrash());
        }
        else if (other.CompareTag("Collectible10PTS"))
        {
            ScoreManager.instance.AddScore(10);
            Instantiate(collectible10PtsSparkleEffectPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Collectible20PTS"))
        {
            ScoreManager.instance.AddScore(20);
            Instantiate(collectible20PtsSparkleEffectPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
    }

    private IEnumerator HandleCrash()
    {
        HealthManager.instance.TakeDamage(HealthManager.instance.maxHealth * 0.10f);
        animator.SetBool("IsCrashing", true);
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        animator.SetBool("IsCrashing", false);
    }
}

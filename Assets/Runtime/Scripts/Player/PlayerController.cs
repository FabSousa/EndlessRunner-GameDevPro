using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private PlayerAudioController audioController;

    [Header("Speed")]
    [SerializeField] private GameMode gameMode;
    [SerializeField] private float horizontalSpeedMultiplier = 1.5f;
    private float horizontalSpeed;
    private float forwardSpeed;

    [Header("Lane")]
    [SerializeField] private float laneDistanceX = 4;

    [Header("Jump")]
    [SerializeField] private float jumpDistanceZ = 5;
    [SerializeField] private float jumpHeightY = 2;
    [SerializeField] private float jumpLerpSpeed = 10;

    [Header("Roll")]
    [SerializeField] private float rollDistanceZ = 5;
    [SerializeField] private Collider regularCollider;
    [SerializeField] private Collider rollCollider;

    [Header("Save")]
    [SerializeField] private SaveGame saveGame;

    public Vector3 InitialPosition { get; private set; }
    float targetPositionX;

    private float jumpStartZ;
    private float rollStartZ;

    public bool IsJumping { get; private set; }
    public bool IsRolling { get; private set; }

    public float JumpDuration => jumpDistanceZ / forwardSpeed;
    public float RollDuration => rollDistanceZ / forwardSpeed;

    private float LeftLaneX => InitialPosition.x - laneDistanceX;
    private float RightLaneX => InitialPosition.x + laneDistanceX;

    public bool CanDie { get; set; } = true;
    private bool isDead = false;

    private bool CanJump => !IsJumping && !isDead;
    private bool CanRoll => !IsRolling && !isDead;

    public event Action PlayerDeathEvent;

    void Awake()
    {
        InitialPosition = transform.position;
        StopRoll();
    }

    void Update()
    {
        ProcessInput();

        Vector3 position = transform.position;

        ProcessSpeed();
        position.x = ProcessLaneMovement();
        position.y = ProcessJump();
        position.z = ProcessForwardMovement();
        ProcessRoll();

        transform.position = position;
    }

    private void ProcessInput()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            targetPositionX += laneDistanceX;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            targetPositionX -= laneDistanceX;
        }
        if (Input.GetKeyDown(KeyCode.Space) && CanJump)
        {
            StartJump();
        }
        if (Input.GetKeyDown(KeyCode.S) && CanRoll)
        {
            StartRoll();
        }

        targetPositionX = Mathf.Clamp(targetPositionX, LeftLaneX, RightLaneX);
    }

    private void ProcessSpeed()
    {
        forwardSpeed = gameMode.Speed;
        horizontalSpeed = gameMode.Speed * horizontalSpeedMultiplier;
    }

    private float ProcessLaneMovement()
    {
        return Mathf.Lerp(transform.position.x, targetPositionX, Time.deltaTime * horizontalSpeed);
    }

    private float ProcessForwardMovement()
    {
        return transform.position.z + forwardSpeed * Time.deltaTime;
    }

    private void StartJump()
    {
        IsJumping = true;
        jumpStartZ = transform.position.z;
        StopRoll();
        audioController.PlayJumpSound();
    }

    private void StopJump()
    {
        IsJumping = false;
    }

    private float ProcessJump()
    {
        float deltaY = 0;
        if (IsJumping)
        {
            float jumpCurrentProgress = transform.position.z - jumpStartZ;
            float jumpPercent = jumpCurrentProgress / jumpDistanceZ;
            if (jumpPercent >= 1)
            {
                StopJump();
            }
            else
            {
                deltaY = Mathf.Sin(Mathf.PI * jumpPercent) * jumpHeightY;
            }
        }
        float targetPositionY = InitialPosition.y + deltaY;
        return Mathf.Lerp(transform.position.y, targetPositionY, Time.deltaTime * jumpLerpSpeed);
    }

    private void StartRoll()
    {
        rollStartZ = transform.position.z;
        IsRolling = true;
        regularCollider.enabled = false;
        rollCollider.enabled = true;

        StopJump();

        audioController.PlayRollSound();
    }

    private void ProcessRoll()
    {
        if (IsRolling)
        {
            float percent = (transform.position.z - rollStartZ) / rollDistanceZ;
            if (percent >= 1)
            {
                StopRoll();
            }
        }
    }

    private void StopRoll()
    {
        IsRolling = false;
        regularCollider.enabled = true;
        rollCollider.enabled = false;
    }

    public void Die()
    {
        if(!CanDie) return;
        PlayerDeathEvent?.Invoke();
        isDead = true;
        StopRoll();
        StopJump();
        regularCollider.enabled = false;
        rollCollider.enabled = false;

        //TODO: Refazer sistema de save
        saveGame.Save();
    }
}

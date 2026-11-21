using UnityEngine;
using UnityEngine.SceneManagement;

public class Guard : MonoBehaviour
{
    public enum PatrolDirection
    {
        PositiveZ,
        PositiveX
    }

    public bool Awake = true;

    [Header("Patrol")]
    [Tooltip("World axis the guard walks along. Flip reverses to the opposite way.")]
    public PatrolDirection moveDirection = PatrolDirection.PositiveZ;
    public float moveSpeed = 2f;
    public float wallCheckDistance = 1.2f;
    public float eyeHeight = 1f;
    [Tooltip("Leave empty to hit any collider (walls).")]
    public LayerMask wallMask;

    [Header("Flip")]
    [Tooltip("Prevents flipping again while still next to the wall.")]
    public float flipCooldown = 0.35f;

    [Header("Player Detection (triangle / cone)")]
    public Transform player;
    [Tooltip("How far the guard can see.")]
    public float detectionDistance = 8f;
    [Tooltip("Full width of the vision triangle in degrees (e.g. 60 = 30 left + 30 right).")]
    [Range(1f, 180f)]
    public float detectionAngle = 60f;
    [Tooltip("Delay before loading the caught scene so the pointing anim can play.")]
    public float catchDelay = 1.2f;
    public string caughtSceneName = "Game Over (caught)";

    Animator animator;
    float cooldownTimer;
    bool caught;
    Vector3 currentMoveDir;

    public AudioSource audioSource;
    public AudioClip ShoutingHey;

    /// <summary>Same vector used for walking, wall checks, and the vision triangle.</summary>
    public Vector3 VisionDirection => currentMoveDir.sqrMagnitude > 0.001f
        ? currentMoveDir
        : GetInitialDirection();

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator != null)
            animator.SetBool("Detected", false);

        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
                player = p.transform;
        }

        currentMoveDir = GetInitialDirection();
        FaceMoveDirection();
    }

    void OnValidate()
    {
        // Keep editor preview aligned when you switch PositiveZ / PositiveX
        if (!Application.isPlaying)
            currentMoveDir = GetInitialDirection();
    }

    Vector3 GetInitialDirection()
    {
        return moveDirection == PatrolDirection.PositiveX
            ? Vector3.right   // +X
            : Vector3.forward; // +Z
    }

    void FaceMoveDirection()
    {
        Vector3 dir = VisionDirection;
        if (dir.sqrMagnitude < 0.001f) return;
        transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
    }

    void Update()
    {
        if (!Awake || caught) return;

        // Keep facing locked to patrol direction (animator won't desync vision)
        FaceMoveDirection();

        if (PlayerInVision())
        {
            CatchPlayer();
            return;
        }

        transform.position += VisionDirection * moveSpeed * Time.deltaTime;

        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
            return;
        }

        if (WallAhead())
            Flip();
    }

    bool PlayerInVision()
    {
        if (player == null) return false;

        Vector3 eye = transform.position + Vector3.up * eyeHeight;
        Vector3 toPlayer = player.position - eye;
        // Flat triangle on the ground plane so height doesn't break detection
        toPlayer.y = 0f;

        float distance = toPlayer.magnitude;
        if (distance > detectionDistance || distance < 0.01f)
            return false;

        // Cone aims along the guard's current walk direction (+Z/+X, or flipped)
        float halfAngle = detectionAngle * 0.5f;
        float angle = Vector3.Angle(VisionDirection, toPlayer.normalized);
        if (angle > halfAngle)
            return false;

        return true;
    }

    void CatchPlayer()
    {
        caught = true;

        if (animator != null)
            animator.SetBool("Detected", true);
        if (audioSource != null)
        {
            audioSource.pitch = 3.0f;
            audioSource.PlayOneShot(ShoutingHey);
            audioSource.pitch = 1.0f;
        }

        Invoke(nameof(LoadCaughtScene), catchDelay);
    }

    void LoadCaughtScene()
    {
        SceneManager.LoadScene(caughtSceneName);
    }

    bool WallAhead()
    {
        Vector3 dir = VisionDirection;
        Vector3 origin = transform.position + Vector3.up * eyeHeight + dir * 0.25f;
        int mask = wallMask.value == 0 ? ~0 : wallMask;

        Debug.DrawRay(origin, dir * wallCheckDistance, Color.red);

        return Physics.Raycast(origin, dir, wallCheckDistance, mask, QueryTriggerInteraction.Ignore);
    }

    void Flip()
    {
        currentMoveDir = -currentMoveDir;
        FaceMoveDirection();
        cooldownTimer = flipCooldown;
    }

    void OnDrawGizmosSelected()
    {
        Vector3 origin = transform.position + Vector3.up * eyeHeight;
        Vector3 dir = VisionDirection;
        if (dir.sqrMagnitude < 0.001f)
            dir = Vector3.forward;

        float halfAngle = detectionAngle * 0.5f;

        Quaternion leftRot = Quaternion.Euler(0f, -halfAngle, 0f);
        Quaternion rightRot = Quaternion.Euler(0f, halfAngle, 0f);

        Vector3 leftDir = leftRot * dir;
        Vector3 rightDir = rightRot * dir;
        Vector3 leftEnd = origin + leftDir * detectionDistance;
        Vector3 rightEnd = origin + rightDir * detectionDistance;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(origin, leftEnd);
        Gizmos.DrawLine(origin, rightEnd);
        Gizmos.DrawLine(leftEnd, rightEnd);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(origin, origin + dir * detectionDistance);
    }
}

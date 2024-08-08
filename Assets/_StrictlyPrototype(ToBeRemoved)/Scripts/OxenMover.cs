using UnityEngine;
using DG.Tweening;
using QFSW.QC;

public class OxenMover : MonoBehaviour
{
    #region Private Variables

    [Tooltip("X Offset for the U-Turn")]
    [SerializeField] private float uTurnOffset = 2.0f;

    [Tooltip("Speed of making the U-Turn")]
    [SerializeField]
    [Range(0, 10)] private float turnSpeed = 6.0f;

    [Tooltip("Forward Move Speed")]
    [SerializeField]
    [Range(0,5)] private float moveSpeed = 1.0f;

    [SerializeField] private OxenTurnDetector oxenTurnDetector;

    private bool isTurning = false;
    private Vector3[] uTurnWaypoints;
    private Quaternion initialRotation;
    private Quaternion targetRotation;


    #endregion

    #region Private Methods

    private void StartUTurnAnimation()
    {
        if (uTurnWaypoints == null || uTurnWaypoints.Length < 2) return;

        // Create a DOTween sequence for U-turn
        Sequence uTurnSequence = DOTween.Sequence();

        // Define the duration for each waypoint
        float durationPerWaypoint = turnSpeed / (uTurnWaypoints.Length - 1);

        // Create a path tween for movement and rotation
        for (int i = 0; i < uTurnWaypoints.Length; i++)
        {
            Vector3 targetPosition = uTurnWaypoints[i];

            if (i == 0)
            {
                // Immediate jump to the start position
                transform.position = targetPosition;
            }
            else
            {
                // Add a move tween to the sequence
                uTurnSequence.Append(transform.DOMove(targetPosition, durationPerWaypoint).SetEase(Ease.Linear));

                // Calculate the direction to the next waypoint
                Vector3 directionToNextWaypoint = (uTurnWaypoints[Mathf.Min(i + 1, uTurnWaypoints.Length - 1)] - transform.position).normalized;
                Quaternion waypointRotation = Quaternion.LookRotation(directionToNextWaypoint);

                // Interpolate the rotation from initial to final
                Quaternion intermediateRotation = Quaternion.Slerp(initialRotation, targetRotation, (float)i / (uTurnWaypoints.Length - 1));

                // Add rotation tween to the sequence
                uTurnSequence.Join(transform.DORotateQuaternion(intermediateRotation, durationPerWaypoint).SetEase(Ease.Linear));
            }
        }

        // On complete, resume moving forward if needed
        uTurnSequence.OnComplete(() => isTurning = false);
    }
    private void CalculateUTurnPath(Vector3 turnCenter, float turnRadius, bool turnLeft, int numPoints = 10)
    {
        uTurnWaypoints = new Vector3[numPoints];
        float angleStep = 180f / (numPoints - 1);
        float startAngle = turnLeft ? 0f : 180f;

        for (int i = 0; i < numPoints; i++)
        {
            float angle = startAngle + i * angleStep;
            if (!turnLeft)
            {
                angle = 180f - angle; // Reverse the direction for right turns
            }
            float rad = Mathf.Deg2Rad * angle;
            float x = turnCenter.x + turnRadius * Mathf.Cos(rad);
            float z = turnCenter.z + turnRadius * Mathf.Sin(rad);
            uTurnWaypoints[i] = new Vector3(x, transform.position.y, z);
        }
    }

    #endregion

    private void Start()
    {
        oxenTurnDetector.OnHit += OxenTurnDetector_OnHit;
    }

    private void OxenTurnDetector_OnHit(OxenBoundary obj)
    {
        StartUTurn(obj.OxenBoundaryType == OxenBoundaryType.TurnLeft);
    }

    private void Update()
    {
        if (isTurning) return;
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    #region Public Methods

    [Command]
    public void StartUTurn(bool turnLeft)
    {
        if (!isTurning)
        {
            isTurning = true;

            // Store the initial and target rotations
            initialRotation = transform.rotation;

            // Determine the target rotation based on direction
            float turnAmount = turnLeft ? 180f : -180f;
            targetRotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + turnAmount, 0f);

            // Define the center of the U-turn curve
            Vector3 turnCenter = transform.position + (turnLeft ? -transform.right : transform.right) * uTurnOffset;

            // Calculate the waypoints for the U-turn path
            CalculateUTurnPath(turnCenter, uTurnOffset, turnLeft);

            // Start the DOTween animation
            StartUTurnAnimation();
        }
    }

    #endregion

    #region Debugging

    private void OnDrawGizmos()
    {
        if (uTurnWaypoints != null)
        {
            Gizmos.color = Color.red;
            for (int i = 0; i < uTurnWaypoints.Length; i++)
            {
                Gizmos.DrawSphere(uTurnWaypoints[i], 0.1f);
            }

            Gizmos.color = Color.green;
            for (int i = 0; i < uTurnWaypoints.Length - 1; i++)
            {
                Gizmos.DrawLine(uTurnWaypoints[i], uTurnWaypoints[i + 1]);
            }
        }
    }

    #endregion
}

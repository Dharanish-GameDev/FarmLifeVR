using UnityEngine;
using QFSW.QC;

namespace FARMLIFEVR.OXEN
{
    public partial class OxenMover : MonoBehaviour
    {
        #region Private Variables

        // Editor Exposed

        [Header("Values")] [Space(5)] [Tooltip("X Offset for the U-Turn")] 
        [SerializeField]
        private float uTurnOffset = 2.0f;
        
        [Tooltip("Speed of making the U-Turn")]
        [SerializeField]
        [Range(0, 10)] private float turnSpeed = 6.0f;

        [Tooltip("Forward Move Speed")]
        [SerializeField]
        [Range(0, 5)] private float moveSpeed = 1.0f;

        [Tooltip("No of Waypoints that are Created to make a U Turn")]
        [SerializeField]
        [Range(5,15)] private int noOfTurnWayPoints = 10;

        [Space(10)]
        [Header("References")]
        [Space(5)]


        [Tooltip("Component that Detects the end of the Field and by OverLap Box")]
        [Required]
        [SerializeField] private OxenTurnDetector oxenTurnDetector;

        // Editor Hidden

        private bool isTurning = false;
        private Vector3[] uTurnWaypoints;
        private Quaternion initialRotation;
        private Quaternion targetRotation;
        private float turnAmount;
        private Vector3 turnCenter;


        #endregion
        
        public bool IsOxenTurning => isTurning;

        #region Private Methods
        private void OxenTurnDetector_OnHit(OxenBoundary obj)
        {
            StartUTurn(obj.OxenBoundaryType == OxenBoundaryType.TurnLeft);
        }

        #endregion

        #region LifeCycle Methods

        private void Start()
        {
            oxenTurnDetector.OnHit += OxenTurnDetector_OnHit;
        }
        private void Update()
        {
            if (isTurning) return;
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }

        #endregion

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
                turnAmount = turnLeft ? 180f : -180f;
                targetRotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + turnAmount, 0f);

                // Define the center of the U-turn curve
                turnCenter = transform.position + (turnLeft ? -transform.right : transform.right) * uTurnOffset;

                // Calculate the waypoints for the U-turn path
                CalculateUTurnPath(turnCenter, uTurnOffset, turnLeft,noOfTurnWayPoints);

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
}


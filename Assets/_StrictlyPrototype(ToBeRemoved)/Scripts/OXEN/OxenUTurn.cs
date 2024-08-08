using DG.Tweening;
using UnityEngine;

namespace FARMLIFEVR.OXEN
{
    public partial class OxenMover
    {
        #region Private Variables

        private Sequence uTurnSequence;
        float durationPerWaypoint;
        Vector3 targetPosition;
        Vector3 directionToNextWaypoint;
        Quaternion waypointRotation;
        Quaternion intermediateRotation;
        float angleStep;
        float startAngle;

        #endregion

        /// <summary>
        /// This Method makes the Object or Oxen to Move towards the waypoint and interpolate the Rotation towards 180 or 0.
        /// </summary>
        private void StartUTurnAnimation()
        {
            if (uTurnWaypoints == null || uTurnWaypoints.Length < 2) return;

            // Create a DOTween sequence for U-turn
            uTurnSequence = DOTween.Sequence();

            // Define the duration for each waypoint
            durationPerWaypoint = turnSpeed / (uTurnWaypoints.Length - 1);

            // Create a path tween for movement and rotation
            for (int i = 0; i < uTurnWaypoints.Length; i++)
            {
                targetPosition = uTurnWaypoints[i];

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
                    directionToNextWaypoint = (uTurnWaypoints[Mathf.Min(i + 1, uTurnWaypoints.Length - 1)] - transform.position).normalized;
                    waypointRotation = Quaternion.LookRotation(directionToNextWaypoint);

                    // Interpolate the rotation from initial to final
                    intermediateRotation = Quaternion.Slerp(initialRotation, targetRotation, (float)i / (uTurnWaypoints.Length - 1));

                    // Add rotation tween to the sequence
                    uTurnSequence.Join(transform.DORotateQuaternion(intermediateRotation, durationPerWaypoint).SetEase(Ease.Linear));
                }
            }

            // On complete, resume moving forward if needed
            uTurnSequence.OnComplete(() =>
            {
                isTurning = false;
                uTurnWaypoints = new Vector3[0]; // Clearing the waypoints Array after completion.
            });
        }

        /// <summary>
        /// It Calculates the Waypoints for making U-Turn and sets it to the waypoint array.
        /// </summary>
        /// <param name="turnCenter"></param>
        /// <param name="turnRadius"></param>
        /// <param name="turnLeft"></param>
        /// <param name="numPoints"></param>
        private void CalculateUTurnPath(Vector3 turnCenter, float turnRadius, bool turnLeft, int numPoints = 10)
        {
            uTurnWaypoints = new Vector3[numPoints];
            angleStep = 180f / (numPoints - 1);
            startAngle = turnLeft ? 0f : 180f;
            float angle;
            float rad;
            float x;
            float z;

            for (int i = 0; i < numPoints; i++)
            {
                angle = startAngle + i * angleStep;
                if (!turnLeft)
                {
                    angle = 180f - angle; // Reverse the direction for right turns
                }
                rad = Mathf.Deg2Rad * angle;
                x = turnCenter.x + turnRadius * Mathf.Cos(rad);
                z = turnCenter.z + turnRadius * Mathf.Sin(rad);
                uTurnWaypoints[i] = new Vector3(x, transform.position.y, z);
            }
        }
    }
}


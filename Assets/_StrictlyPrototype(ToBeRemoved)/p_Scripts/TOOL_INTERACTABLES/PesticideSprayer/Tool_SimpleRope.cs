using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace FARMLIFEVR.FARMTOOLS
{
    public class Tool_SimpleRope : MonoBehaviour
    {
        //Objects that will interact with the rope
        public Transform whatTheRopeIsConnectedTo;
        public Transform whatIsHangingFromTheRope;

        //Line renderer used to display the rope
        private LineRenderer lineRenderer;

        //A list with all rope sections
        public List<Vector3> allRopeSections = new List<Vector3>();

        void Start()
        {
            //Init the line renderer we use to display the rope
            lineRenderer = GetComponent<LineRenderer>();
        }

        void Update()
        {
            //Display the rope with a line renderer
            DisplayRope();
        }

        //Display the rope with a line renderer
        private void DisplayRope()
        {
            //This is not the actual width, but the width use so we can see the rope
            float ropeWidth = 0.01f;

            lineRenderer.startWidth = ropeWidth;
            lineRenderer.endWidth = ropeWidth;


            //Update the list with rope sections by approximating the rope with a bezier curve
            //A Bezier curve needs 4 control points
            Vector3 A = whatTheRopeIsConnectedTo.position;
            Vector3 D = whatIsHangingFromTheRope.position;

            //Upper control point
            //To get a little curve at the top than at the bottom
            Vector3 B = A + whatTheRopeIsConnectedTo.up * (-(A - D).magnitude * 0.1f);
            //B = A;

            //Lower control point
            Vector3 C = D + whatIsHangingFromTheRope.up * ((A - D).magnitude * 0.5f);

            //Get the positions
            BezierCurve.GetBezierCurve(A, B, C, D, allRopeSections);


            //An array with all rope section positions
            Vector3[] positions = new Vector3[allRopeSections.Count];

            for (int i = 0; i < allRopeSections.Count; i++)
            {
                positions[i] = allRopeSections[i];
            }


            //Add the positions to the line renderer
            lineRenderer.positionCount = positions.Length;

            lineRenderer.SetPositions(positions);
        }
    }

    public static class BezierCurve
    {
        //Update the positions of the rope section
        public static void GetBezierCurve(Vector3 A, Vector3 B, Vector3 C, Vector3 D, List<Vector3> allRopeSections)
        {
            //The resolution of the line
            //Make sure the resolution is adding up to 1, so 0.3 will give a gap at the end, but 0.2 will work
            float resolution = 0.1f;

            //Clear the list
            allRopeSections.Clear();


            float t = 0;

            while (t <= 1f)
            {
                //Find the coordinates between the control points with a Bezier curve
                Vector3 newPos = DeCasteljausAlgorithm(A, B, C, D, t);

                allRopeSections.Add(newPos);

                //Which t position are we at?
                t += resolution;
            }

            allRopeSections.Add(D);
        }

        //The De Casteljau's Algorithm
        static Vector3 DeCasteljausAlgorithm(Vector3 A, Vector3 B, Vector3 C, Vector3 D, float t)
        {
            //Linear interpolation = lerp = (1 - t) * A + t * B
            //Could use Vector3.Lerp(A, B, t)

            //To make it faster
            float oneMinusT = 1f - t;

            //Layer 1
            Vector3 Q = oneMinusT * A + t * B;
            Vector3 R = oneMinusT * B + t * C;
            Vector3 S = oneMinusT * C + t * D;

            //Layer 2
            Vector3 P = oneMinusT * Q + t * R;
            Vector3 T = oneMinusT * R + t * S;

            //Final interpolated position
            Vector3 U = oneMinusT * P + t * T;

            return U;
        }
    }
}
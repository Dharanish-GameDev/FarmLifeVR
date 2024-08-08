using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace FARMLIFEVR.HANDS
{
    [RequireComponent(typeof(XRGrabInteractable))]
    public class CustomGrabPose : MonoBehaviour
    {
        #region Private Variables

        [SerializeField] private HandData rightHandPoseData;
        [SerializeField] private HandData leftHandPoseData;
        [SerializeField] private float poseTransitionDuration = 0.35f;

        private Vector3 startingHandPos;
        private Vector3 finalHandPos;
        private Quaternion startingHandRot;
        private Quaternion finalHandRot;

        private Quaternion[] startingFingerBonesRotArray;
        private Quaternion[] finalFingerBonesRotArray;

        private float routineTimer = 0;

        #endregion

        #region Properties



        #endregion

        #region LifeCycle Methods

        private void Start()
        {
            XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
            grabInteractable.selectEntered.AddListener(SetUpPose);
            grabInteractable.selectExited.AddListener(UnSetPose);
            rightHandPoseData.gameObject.SetActive(false);
            leftHandPoseData.gameObject.SetActive(false);
        }

        #endregion

        #region Private Methods

        private void SetUpPose(SelectEnterEventArgs args)
        {
            if (args.interactorObject is XRDirectInteractor)
            {
                HandData handData = args.interactorObject.transform.GetComponentInChildren<HandData>();
                handData.Animator.enabled = false;
                if (handData.handType == HandData.HandModelType.RightHand)
                {
                    SetHandDataValues(handData, rightHandPoseData);
                }
                else
                {
                    SetHandDataValues(handData, leftHandPoseData);
                }
                StartCoroutine(SetHandDataRoutine(handData, finalHandPos, finalHandRot, finalFingerBonesRotArray, startingHandPos, startingHandRot, startingFingerBonesRotArray));
            }
        }
        private void UnSetPose(SelectExitEventArgs args)
        {
            if (args.interactorObject is XRDirectInteractor)
            {
                HandData handData = args.interactorObject.transform.GetComponentInChildren<HandData>();
                handData.Animator.enabled = true;
                StartCoroutine(SetHandDataRoutine(handData, startingHandPos, startingHandRot, startingFingerBonesRotArray, finalHandPos, finalHandRot, finalFingerBonesRotArray));
            }
        }
        private void SetHandDataValues(HandData h1, HandData h2)
        {
            startingHandPos = h1.Root.localPosition;
            finalHandPos = h2.Root.localPosition;

            startingHandRot = h1.Root.localRotation;
            finalHandRot = h2.Root.localRotation;

            startingFingerBonesRotArray = new Quaternion[h1.FingerBonesTransformArray.Length];
            finalFingerBonesRotArray = new Quaternion[h1.FingerBonesTransformArray.Length];

            for (int i = 0; i < h1.FingerBonesTransformArray.Length; i++)
            {
                startingFingerBonesRotArray[i] = h1.FingerBonesTransformArray[i].localRotation;
                finalFingerBonesRotArray[i] = h2.FingerBonesTransformArray[i].localRotation;
            }
        }
        private void SetHandData(HandData h1, Vector3 newPos, Quaternion newRot, Quaternion[] newFingerRotationArray)
        {
            h1.Root.localPosition = newPos;
            h1.Root.localRotation = newRot;

            for (int i = 0; i < newFingerRotationArray.Length; i++)
            {
                h1.FingerBonesTransformArray[i].localRotation = newFingerRotationArray[i];
            }
        }
        private IEnumerator SetHandDataRoutine(HandData h1, Vector3 newPos, Quaternion newRot, Quaternion[] newFingerRotationArray, Vector3 startPos, Quaternion startRot, Quaternion[] startFingerRotationArray)
        {
            routineTimer = 0;
            while (routineTimer < poseTransitionDuration)
            {
                h1.Root.localPosition = Vector3.Lerp(startPos, newPos, routineTimer / poseTransitionDuration);
                h1.Root.localRotation = Quaternion.Lerp(startRot, newRot, routineTimer / poseTransitionDuration);

                for (global::System.Int32 i = 0; i < newFingerRotationArray.Length; i++)
                {
                    h1.FingerBonesTransformArray[i].localRotation = Quaternion.Lerp(startFingerRotationArray[i], newFingerRotationArray[i], routineTimer / poseTransitionDuration);
                }

                routineTimer += Time.deltaTime;
                yield return null;
            }
        }
        private void MirrorPose(HandData poseToMirror, HandData poseUsedToMirror)
        {
            Vector3 mirroredPos = poseUsedToMirror.Root.localPosition;
            mirroredPos.x *= -1;

            Quaternion mirroredQuaternion = poseUsedToMirror.Root.localRotation;
            mirroredQuaternion.z *= -1;
            mirroredQuaternion.y *= -1;

            poseToMirror.Root.localPosition = mirroredPos;
            poseToMirror.Root.localRotation = mirroredQuaternion;

            for (int i = 0; i < poseUsedToMirror.FingerBonesTransformArray.Length; i++)
            {
                poseToMirror.FingerBonesTransformArray[i].localRotation = poseUsedToMirror.FingerBonesTransformArray[i].localRotation;
            }
        }

        #endregion

        #region Public Methods

#if UNITY_EDITOR
        [MenuItem("Tools/MirrorRightPose")]
        public static void MirrorRightPose()
        {
            CustomGrabPose grabHandPose = Selection.activeGameObject.GetComponent<CustomGrabPose>();
            grabHandPose.MirrorPose(grabHandPose.leftHandPoseData, grabHandPose.rightHandPoseData);
        }
#endif

        #endregion
    }
}




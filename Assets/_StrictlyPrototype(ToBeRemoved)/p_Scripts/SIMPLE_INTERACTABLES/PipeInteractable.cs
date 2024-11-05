using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using FARMLIFEVR.EVENTSYSTEM;

namespace FARMLIFEVR.SIMPLEINTERACTABLES
{
	public class PipeInteractable : BaseSimpleInteractable
	{
        #region Private Variables

        [SerializeField]
        private XRDirectInteractor interactor;

        [SerializeField] private Transform handle;
        [SerializeField] private float minAngle;
        [SerializeField] private float maxAngle;

        [SerializeField]
        private Vector3 dir;
        [SerializeField]
        private float angle;

        private bool hasReachedAngle = false;

        #endregion

        #region Properties

        #endregion

        #region Overridden Methods 

        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);
            if(args.interactorObject is XRDirectInteractor)
            {
                interactor = args.interactorObject as XRDirectInteractor;
            }
        }

        protected override void OnSelectExited(SelectExitEventArgs args)
        {
            base.OnSelectExited(args);
            if (args.interactorObject is XRDirectInteractor)
            {
                interactor = null;
            }
        }

        public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
        {
            base.ProcessInteractable(updatePhase);

            if(interactor)
            {
                if (hasReachedAngle) return;
                UpdateHandleAngle();
            }
        }

        #endregion

        #region Public Methods

        public void EnablePipeInteractable()
        {
            this.enabled = true;
            hasReachedAngle = false;
        }

        public void DisablePipeInteractable()
        {
            this.enabled = false;
        }

        #endregion

        #region Private Methods
        private void UpdateHandleAngle()
        {
            angle = Mathf.Atan2(CalculateDirection().z, CalculateDirection().y) * Mathf.Rad2Deg;

            if (angle > minAngle)
            {
                handle.localRotation = Quaternion.Euler(maxAngle, 0, 0);
            }

            if(handle.localEulerAngles.x == maxAngle)
            {
                hasReachedAngle = true;
                EventManager.TriggerEvent(EventNames.StartIrrigation);
            }
        }

        private Vector3 CalculateDirection() // Returns the Direction between the interactor and the Handle
        {
            dir = interactor.GetAttachTransform(this).position - handle.position;
            dir = transform.InverseTransformDirection(dir); // Converts WorldSpace Direction into LocalSpace Direction
            return dir.normalized; // Returning Normalized Direction
        }

        #endregion

        public void ResetPipeInteractable()
        {
            hasReachedAngle = false;
            handle.localRotation = Quaternion.Euler(minAngle, 0, 0);
            this.enabled = false;
        }

    }
}

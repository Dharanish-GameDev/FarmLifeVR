using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace FARMLIFEVR.INPUT
{
    [RequireComponent(typeof(XRRayInteractor))]
    public class ToggleRayInteractor : MonoBehaviour
    {
        #region Private Variables

        [Tooltip("The Direct its need to switch with")]
        [SerializeField] private XRDirectInteractor directInteractor;

        [Tooltip("Toggling ray interactor even if an Object is selected on Direct Interactor")]
        [SerializeField] private bool forceToggle = false;

        private bool isSwitched = false;
        private XRRayInteractor rayInteractor;

        List<IXRInteractable> interactableList = new List<IXRInteractable>();  // List of interactable that direct interactor is selected.


        private bool canActivateRay = true;
        #endregion

        #region Properties



        #endregion

        #region LifeCycle Methods

        private void Awake()
        {
            rayInteractor = GetComponent<XRRayInteractor>();
            SwitchRayInteractor(false);
        }
        private void Start()
        {

        }
        private void Update()
        {

        }

        #endregion

        #region Private Methods

        private void SwitchRayInteractor(bool val)
        {
            isSwitched = val;
            rayInteractor.enabled = val;
            directInteractor.enabled = !val;
        }

        private bool IsHavingInteractablesOnDirectInteractor()  // Checking if the Direct Interactor has any Objects Selected.
        {
            interactableList.Clear();
            directInteractor.GetValidTargets(interactableList);
            return (interactableList.Count > 0);
        }

        #endregion

        #region Public Methods
        public void ActivateRayInteractor()
        {
            if (!canActivateRay) return;
            if (forceToggle || !IsHavingInteractablesOnDirectInteractor())
            {
                SwitchRayInteractor(true);
            }
        }
        public void DeactivateRayInteractor()
        {
            if (!isSwitched) return;
            SwitchRayInteractor(false);
        }

        public void SetCanActivateRayBoolean(bool val)
        {
            canActivateRay = val;
        }
        #endregion
    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace FARMLIFEVR.SIMPLEINTERACTABLES
{
    public class SeedPlanterInteractable : BaseSimpleInteractable
    {
        public event Action OnTriedToPlant;

        [Space(10)]
        [Header("References")]
        [Space(3)]
        [SerializeField][Required] private GameObject indicatorObject;

        protected override void Awake()
        {
            base.Awake();
        }
        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            OnTriedToPlant?.Invoke();
        }
        protected override void OnHoverEntered(HoverEnterEventArgs args)
        {
            base.OnHoverEntered(args);
            EnableIndicator();
        }
        protected override void OnHoverExited(HoverExitEventArgs args)
        {
            base.OnHoverExited(args);
            DisableIndicator();
        }

        #region Public Methods

        public void EnableIndicator()
        {
            indicatorObject.SetActive(true);
        }
        public void DisableIndicator()
        {
            indicatorObject.SetActive(false);
        }

        #endregion
    }
}

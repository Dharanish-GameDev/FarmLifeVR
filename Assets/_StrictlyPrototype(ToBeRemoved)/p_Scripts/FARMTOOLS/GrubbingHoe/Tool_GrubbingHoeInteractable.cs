using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace FARMLIFEVR.FARMTOOLS
{
	public class Tool_GrubbingHoeInteractable : XRGrabInteractable
    { 
        #region Private Variables

        // Exposed in Editor

        [SerializeField]
        private XRDirectInteractor firstDirectInteractor;

        [SerializeField]
        private XRDirectInteractor secondDirectInteractor;

        [Space(5)]
        [SerializeField] [Required] private Tool_GrubbingHoeOverlapChecker tool_GrubbingHoeOverlapChecker;

        // Hidden from Editor
        private IXRSelectInteractor selectInteractor;

        #endregion

        #region Properties

        public bool IsGrabbedByTwoHands => firstDirectInteractor != null || secondDirectInteractor != null;  // Need to change this ( OR || ) to ( AND && )


        #endregion

        #region Protected Methods

        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            base.OnSelectEntered(args);

            if(firstDirectInteractor)
            {
                selectInteractor = args.interactorObject;
                if(selectInteractor is XRDirectInteractor)
                {
                    secondDirectInteractor = selectInteractor as XRDirectInteractor;
                    tool_GrubbingHoeOverlapChecker.StartOverlapCheckingByInterval();
                }
                return;
            }

            selectInteractor = args.interactorObject;

            if (selectInteractor is XRDirectInteractor)
            {
                firstDirectInteractor = selectInteractor as XRDirectInteractor;
                tool_GrubbingHoeOverlapChecker.StartOverlapCheckingByInterval();
            }

        }

        protected override void OnSelectExited(SelectExitEventArgs args)
        {
            base.OnSelectExited(args);

            selectInteractor = args.interactorObject;

            if(selectInteractor is XRDirectInteractor)
            {
                XRDirectInteractor directInteractor = selectInteractor as XRDirectInteractor;
                if (directInteractor == firstDirectInteractor)
                {
                    firstDirectInteractor = null;
                }

                if (directInteractor == secondDirectInteractor)
                {
                    secondDirectInteractor = null;
                }

                if(firstDirectInteractor == null && secondDirectInteractor == null)
                {
                    tool_GrubbingHoeOverlapChecker.StopOverlapCheckingByInterval();
                }
            }
        }

        #endregion
    }
}

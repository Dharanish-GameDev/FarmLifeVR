using FARMLIFEVR.CROPS.MAIZE;
using UnityEngine;


namespace FARMLIFEVR.FARMTOOLS
{
    public class Tool_PesticideOverlapChecker : OverLapChecker
    {
        protected override void HitCallBack(Collider collider)
        {
            if (collider == null) return;
            Maize maize = collider.GetComponentInParent<Maize>();
            if (maize == null) return;

            if (maize.IsPestSprayed) return;
            maize.IsPestSprayed = true;
        }

        /// <summary>
        /// It Starts to Checks for the Overlapping of The PestIndicator
        /// </summary>
        public void StartCheckingForPlants()
        {
            StartOverlapCheckingByInterval();
        }

        /// <summary>
        /// It Stops for the Checking of the Overlapping to the Pest Indicator
        /// </summary>
        public void StopCheckingForPlants()
        {
            StopOverlapCheckingByInterval();
            Debug.Log("<color=red> Stopped Checking </color>");
        }
    }
}

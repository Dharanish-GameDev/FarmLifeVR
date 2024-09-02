using System;
using UnityEngine;

namespace FARMLIFEVR.OXEN
{
    public class OxenTurnDetector : OverLapChecker
    {
        public event Action<OxenBoundary> OnHit;
        protected override void HitCallBack(Collider collider)
        {
            if (collider.TryGetComponent(out OxenBoundary boundary))
            {
                OnHit?.Invoke(boundary); // Invoke the OnHit event only if the OxenBoundary component is found
            }
        }
    }
}


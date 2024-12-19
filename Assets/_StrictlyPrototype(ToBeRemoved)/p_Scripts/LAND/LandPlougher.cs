using FARMLIFEVR.OXEN;
using UnityEngine;

namespace FARMLIFEVR.LAND
{
    public class LandPlougher : OverLapChecker
    {
        [SerializeField] private OxenMover oxenMover;
        protected override void HitCallBack(Collider collider)
        {
            if(oxenMover.IsOxenTurning) return;
            if (collider == null) return;
            if(collider.TryGetComponent(out Land land))
            {
                land.ChangeLandState(LandState.Ploughed);
            }    
        }
    }
}


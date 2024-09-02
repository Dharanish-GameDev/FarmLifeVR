using UnityEngine;

namespace FARMLIFEVR.LAND
{
    public class LandPlougher : OverLapChecker
    {
        protected override void HitCallBack(Collider collider)
        {
            if (collider == null) return;
            if(collider.TryGetComponent(out Land land))
            {
                land.ChangeLandState(LandState.Ploughed);
            }    
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Tool_GrubbingHoeOverlapChecker : OverLapChecker
{
    protected override void HitCallBack(Collider collider)
    {
        collider.gameObject.TryGetComponent<WaterCanalMain>(out WaterCanalMain waterCanalMain);
        waterCanalMain.GrubLand();
    }

}

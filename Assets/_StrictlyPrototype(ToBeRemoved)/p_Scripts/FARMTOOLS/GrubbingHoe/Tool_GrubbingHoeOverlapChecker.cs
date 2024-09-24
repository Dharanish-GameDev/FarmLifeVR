using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FARMLIFEVR.LAND;
using UnityEngine.Assertions;

namespace FARMLIFEVR.FARMTOOLS
{
    public class Tool_GrubbingHoeOverlapChecker : OverLapChecker
    {
        [Space(3)]

        [SerializeField] [Required] private Tool_GrubbingHoeInteractable grubbingHoeInteractable;

        protected override void HitCallBack(Collider collider)
        {   
            if (!grubbingHoeInteractable.IsGrabbedByTwoHands) return;
            collider.gameObject.TryGetComponent<WaterCanal>(out WaterCanal waterCanalMain);
            waterCanalMain.GrubLand();
        }

        private void Awake()
        {
            ValidateConstraints();
        }

        private void ValidateConstraints()
        {
            Assert.IsNotNull(grubbingHoeInteractable,"Hoe interactable in Null!");
        }

    }
}

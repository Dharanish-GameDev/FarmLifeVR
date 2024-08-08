using UnityEngine;

namespace FARMLIFEVR.OXEN
{
    public class OxenBoundary : MonoBehaviour
    {
        #region Private Variables

        [SerializeField] private OxenBoundaryType boundaryType;

        #endregion

        #region Properties

        public OxenBoundaryType OxenBoundaryType => boundaryType;

        #endregion

        #region LifeCycle Methods

        private void Awake()
        {

        }
        private void Start()
        {

        }
        private void Update()
        {

        }

        #endregion

        #region Private Methods


        #endregion

        #region Public Methods


        #endregion
    }
    public enum OxenBoundaryType
    {
        TurnLeft,
        TurnRight
    }
}



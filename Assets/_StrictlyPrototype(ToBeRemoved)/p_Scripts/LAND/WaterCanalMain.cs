using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCanalMain : MonoBehaviour
{
    [Header("Materials")]
    [Space(3)]
    [SerializeField][Required] private Material dirtMat;
    [SerializeField][Required] private Material grubbedMat;
    [SerializeField][Required] private Material WateredMat;

    [Space(5)]

    [Header("References")]
    [Space(3)]
    [SerializeField][Required] private Renderer meshRenderer;
    [SerializeField] private LayerMask waterCanalMainLayerMask;
    [SerializeField] private LayerMask invalidLayerMask;


    private bool isWaterCanalEnabled = false;

    #region Public Methods

    public void GrubLand()
    {
        if (isWaterCanalEnabled) return;
        isWaterCanalEnabled = true;
        ChangeMaterial(grubbedMat);
        gameObject.layer = invalidLayerMask; // Its To Avoid Further OverLapping Registed
    }

    public void ResetWaterCanalMain()
    {
        isWaterCanalEnabled = false;
        ChangeMaterial(dirtMat);
        gameObject.layer = waterCanalMainLayerMask;
    }
    #endregion

    #region Private Methods

    private void ChangeMaterial(Material mat)
    {
        meshRenderer.material = mat;
    }

    #endregion
}

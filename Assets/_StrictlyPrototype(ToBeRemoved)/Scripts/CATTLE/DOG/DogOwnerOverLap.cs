using UnityEngine;

public class DogOwnerOverLap : OverLapChecker
{
    protected override void HitCallBack(Collider collider)
    {
        Debug.Log("Player Found By Dog Within Radius");
    }
}

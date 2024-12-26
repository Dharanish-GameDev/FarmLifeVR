using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerTriggerChecker : MonoBehaviour
{
    private GameObject player;
    private void Awake()
    {
        player = GameManager.Instance?.PlayerTransform.gameObject;
        CustomAwake();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != player) return;
        OnPlayerTriggered();
    }
    
    public abstract void OnPlayerTriggered();

    public virtual void CustomAwake() {}

}

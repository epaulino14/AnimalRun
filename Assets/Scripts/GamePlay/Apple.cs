using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponentInParent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            PickupApple();
    }

    private void PickupApple()
    {
        anim.SetTrigger("Pickup");
        GameStat.Instance.CollectApple();
    }
    private void OnShowChunk()
    {
        anim?.SetTrigger("Ilde");
    }
}

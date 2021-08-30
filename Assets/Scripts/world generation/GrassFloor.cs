using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassFloor : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Material material;
    public float offsetSpeed = 0.25f;
    
   
    private void Update()
    {
        transform.position = Vector3.forward * player.transform.position.z;
        material.mainTextureOffset = new Vector2(0, -transform.position.z * offsetSpeed);
        
    }
}

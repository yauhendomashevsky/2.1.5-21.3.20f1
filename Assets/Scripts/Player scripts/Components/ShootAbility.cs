using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class ShootAbility : MonoBehaviour, Ability
{
    public GameObject bulletPrefab;
    public float shootDelay;
    private float shootTime = float.MinValue;
    public Vector3 offset;

    public void Execute()
    {
        if(Time.time < shootDelay + shootTime) {return;}

        shootTime = Time.time; 

        if (bulletPrefab != null)
        {
            var newBullet = Instantiate(bulletPrefab, transform.position + offset, Quaternion.LookRotation(transform.forward));
        }
        else
        {
            Debug.LogError("No bullet");
        }
    }
}

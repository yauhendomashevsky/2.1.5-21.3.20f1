using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAbility : MonoBehaviour, Ability
{
    public GameObject bulletPrefab;
    public float shootDelay;
    private float shootTime = float.MinValue;

    public void Execute()
    {
        if(Time.time < shootDelay + shootTime) {return;}

        shootTime = Time.time;

        if (bulletPrefab != null) 
        { 
            var newBullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        }
        else
        {
            Debug.LogError("No bullet");
        }
    }
}

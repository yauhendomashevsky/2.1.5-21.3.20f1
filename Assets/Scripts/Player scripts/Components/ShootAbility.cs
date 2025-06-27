using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class ShootAbility : MonoBehaviour, Ability
{
    public GameObject bulletPrefab;
    public float shootDelay = 0.5f;
    private float shootTime = 2f;
    public Vector3 offset;

    public void Execute()
    {
        if (shootDelay < shootTime)
        {
            return;
        }

        //shootTime = Time.time; 

        if (shootDelay >= shootTime)
        {
            var newBullet = Instantiate(bulletPrefab, transform.position + offset, Quaternion.LookRotation(transform.forward));
            shootDelay = 0f;
            
        }
        else
        {
            Debug.LogError("No bullet");
        }
        Debug.Log("EXE");
    }

    private void Update()
    {
        shootDelay += Time.deltaTime;
    }
}

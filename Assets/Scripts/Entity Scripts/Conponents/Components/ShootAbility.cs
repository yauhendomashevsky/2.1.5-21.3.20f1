using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class ShootAbility : MonoBehaviour, Ability
{
    public GameObject bulletPrefab;
    public float nextShootTime = 0;
    private float shootInterval = 2f;
    public Vector3 offset;

    public void Execute()
    {
        if (nextShootTime < shootInterval)
        {
            return;
        }

        if (nextShootTime >= shootInterval)
        {
            var newBullet = Instantiate(bulletPrefab, transform.position + offset, Quaternion.LookRotation(transform.forward));
            nextShootTime = 0;
        }
        else
        {
            Debug.LogError("No bullet");
        }
        Debug.Log("EXE");
    }

    private void Update()
    {
        nextShootTime += Time.deltaTime;
    }
}
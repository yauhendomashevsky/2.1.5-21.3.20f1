using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using System.Linq;
using UnityEngine;

public static class Utils
{
    //public static List<Collider> GetAllColliders(this GameObject go)
    //{
    //    return go == null ? null : new List<go.GetComponent<Collider>>()>;
    //}

    public static Collider GetCollider(this GameObject go)
    {
        return go.GetComponent<Collider>();
    }

    public static void ToWorldSpaceBox(this BoxCollider box, out float3 center, out float3 halfExtends, out Quaternion orientation)
    {
        Transform transform = box.transform;
        orientation = transform.rotation;
        center = transform.TransformPoint(box.center);
        var lossyScale = transform.lossyScale;
        var scale = Abs(lossyScale);
        halfExtends = Vector3.Scale(scale, box.size) * 0.5f;
    }

    public static void ToWorldSpaceCapsule(this CapsuleCollider capsule, out float3 point0, out float3 point1, out float radius)
    {
        Transform transform = capsule.transform;
        var centre = (float3)transform.TransformPoint(capsule.center);
        radius = 0f;
        float height = 0f;
        float3 lossyScale = Abs(transform.lossyScale);
        float3 dir = float3.zero;

        switch (capsule.direction)
        {
            case 0:
                radius = Mathf.Max(lossyScale.y, lossyScale.z) * capsule.radius;
                height = lossyScale.x * capsule.height;
                dir = capsule.transform.TransformPoint(Vector3.right);
                break;
            case 1:
                radius = Mathf.Max(lossyScale.x, lossyScale.z) * capsule.radius;
                height = lossyScale.y * capsule.height;
                dir = capsule.transform.TransformPoint(Vector3.up);
                break;
            case 2:
                radius = Mathf.Max(lossyScale.x, lossyScale.y) * capsule.radius;
                height = lossyScale.z * capsule.height;
                dir = capsule.transform.TransformPoint(Vector3.forward);
                break;
        }

        if(height < radius * 2f)
        {
            dir = Vector3.zero;
        }

        point0 = centre + dir * (height * 0.5f - radius);
        point1 = centre - dir * (height * 0.5f - radius);
    }

    public static void ToWorldSpaceSphere(this SphereCollider sphere, out float3 canter, out float radius)
    {
        Transform transform = sphere.transform;
        canter = transform.TransformPoint(sphere.center);
        radius = sphere.radius * Max(Abs(transform.lossyScale));
    }

    public static float3 Abs(float3 vec)
    {
        return new float3(Mathf.Abs(vec.x), Mathf.Abs(vec.y), Mathf.Abs(vec.z));
    }

    public static float Max(float3 v)
    {
        return Mathf.Max(v.x, Mathf.Max(v.y, v.z));
    }
}

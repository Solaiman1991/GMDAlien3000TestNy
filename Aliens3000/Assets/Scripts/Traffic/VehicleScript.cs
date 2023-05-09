using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class VehicleScript : MonoBehaviour
{
    [Header("speed movement:")]
    public float CarSpeed = 5;
    [Header("distance: start - destroy")]
    public float CarLife = 900;
   [FormerlySerializedAs("_HCTrafficLinked")] [HideInInspector]
    public TrafikScript TrafikLinked;
    Vector3 StartPosition;

    void Start()
    {
        StartPosition = transform.position;
    }

    void Update()
    {
        transform.localPosition += transform.TransformDirection(Vector3.forward) * CarSpeed * Time.deltaTime;
        if (FastDistance(transform.position, StartPosition, CarLife))
        {
            TrafikLinked.VehicleCount--;
            Destroy(this.gameObject);
        }
    }

    bool FastDistance(Vector3 Self, Vector3 Target, float Radius)
    {
        bool Xpass = false;
        bool Zpass = false;

        //x
        if ((Self.x >= 0 & Target.x >= 0) | (Self.x < 0 & Target.x < 0))
        {
            if (Mathf.Abs(Mathf.Abs(Self.x) - Mathf.Abs(Target.x)) > Radius) Xpass = true;
        }
        else
        {
            if (Mathf.Abs(Self.x) + Mathf.Abs(Target.x) > Radius) Xpass = true;
        }

        //z
        if ((Self.z >= 0 & Target.z >= 0) | (Self.z < 0 & Target.z < 0))
        {
            if (Mathf.Abs(Mathf.Abs(Self.z) - Mathf.Abs(Target.z)) > Radius) Zpass = true;
        }
        else
        {
            if (Mathf.Abs(Self.z) + Mathf.Abs(Target.z) > Radius) Zpass = true;
        }

        if (Xpass | Zpass) return true;
        else return false;
    }
}

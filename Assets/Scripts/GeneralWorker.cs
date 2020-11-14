using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  static class GeneralWorker
{
   
    public static void DisabledRagDoll(this Transform transform)
    {
        foreach (Transform  item in transform)
        {
            if(item.GetComponent<Collider>())
            {
                item.GetComponent<Collider>().enabled = false;
            }
            if(item.GetComponent<Rigidbody>())
            {
                item.GetComponent<Rigidbody>().useGravity = false;
            }
            DisabledRagDoll(item);
        }

    }
    public static void EnabledRagDoll(this Transform transform)
    {
        foreach (Transform item in transform)
        {
            if (item.GetComponent<Collider>())
            {
                item.GetComponent<Collider>().enabled = true;
            }
            if (item.GetComponent<Rigidbody>())
            {
                item.GetComponent<Rigidbody>().useGravity = true;
            }
            EnabledRagDoll(item);
        }

    }
    public static void AllRagdollForce(this Transform transform,Vector3 force)
    {
        foreach (Transform item in transform)
        {
            if(item.GetComponent<Rigidbody>())
            {
                item.GetComponent<Rigidbody>().AddForce(force);
            }
            AllRagdollForce(item,force);

        }
    }
  
}


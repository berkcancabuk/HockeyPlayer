using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asd : MonoBehaviour
{
   
    public Transform prefab;
    public int numberOfObjects = 20;
    public float radius = 5f;

    void Start()
    {
        //for (int i = 0; i < numberOfObjects; i++)
        //{
        //    float angle = i * Mathf.PI * 2 / numberOfObjects;
        //    float x = Mathf.Cos(angle) * radius;
        //    float z = Mathf.Sin(angle) * radius;
        //    Vector3 pos = transform.position + new Vector3(x, 0, z);
        //    float angleDegrees = -angle * Mathf.Rad2Deg;
        //    Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);
        //    Instantiate(prefab, pos, rot);
        //}
        this.gameObject.transform.RotateAroundLocal(prefab.position,3f);
            }
}

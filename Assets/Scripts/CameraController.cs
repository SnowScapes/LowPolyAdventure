using System;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float distance = 5;
    [SerializeField] private float minRot;
    [SerializeField] private float maxRot;

    private float curRot;

    public float y_input { get; set; }

    private void LateUpdate()
    {
        SetPos();
    }

    private void SetPos()
    {
        curRot = transform.eulerAngles.x;
        curRot += y_input;
        if (curRot < 180) 
            curRot = Mathf.Clamp(curRot, -1, maxRot);
        else
            curRot = Mathf.Clamp(curRot, 360 + minRot, 361);
        transform.eulerAngles = new Vector3(curRot, transform.eulerAngles.y, transform.eulerAngles.z);
        /*Debug.Log(transform.eulerAngles.x);
        if (transform.eulerAngles.x < maxRot)
            transform.eulerAngles += new Vector3(y_input, 0, 0);*/
        //transform.eulerAngles = new Vector3(Mathf.Clamp(transform.eulerAngles.x, minRot, maxRot),
        //transform.eulerAngles.y, transform.eulerAngles.z);
    }
}

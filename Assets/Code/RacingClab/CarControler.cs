using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControler : MonoBehaviour
{
    [SerializeField] private Transform FLWheel;
    [SerializeField] private Transform BLWheel;
    [SerializeField] private Transform FRWheel;
    [SerializeField] private Transform BRWheel;

    [SerializeField] private WheelCollider FLWheelCollider;
    [SerializeField] private WheelCollider BLWheelCollider;
    [SerializeField] private WheelCollider FRWheelCollider;
    [SerializeField] private WheelCollider BRWheelCollider;

    [SerializeField] private float force;
    [SerializeField] private float maxAngle;

    private int direction = 1;

    private float angle;

    public void GetMove()
    {
        FLWheelCollider.brakeTorque = 0f;
        FRWheelCollider.brakeTorque = 0f;

        FLWheelCollider.motorTorque += force * Time.fixedDeltaTime * direction;
        FRWheelCollider.motorTorque += force * Time.fixedDeltaTime * direction;
    }

    public void SwitchDirection()
    {
        if (direction == 1)
            direction = -1;
        else direction = 1;
    }

    public void GetStop()
    {
        FLWheelCollider.brakeTorque = 3000f;
        FRWheelCollider.brakeTorque = 3000f;
    }

    public void GetStoped()
    {
        FLWheelCollider.motorTorque = 0;
        FRWheelCollider.motorTorque = 0;

        FLWheelCollider.brakeTorque = 200f;
        FRWheelCollider.brakeTorque = 200f;
    }

    public void GetRotation(int direction)
    {        
        angle += maxAngle * direction * Time.fixedDeltaTime;
        angle = Mathf.Clamp(angle, -maxAngle, maxAngle);

        FLWheelCollider.steerAngle = angle;
        FRWheelCollider.steerAngle = angle;
    }

    public void GetZeroRotation()
    {
        angle = 0;
        FLWheelCollider.steerAngle = 0;
        FRWheelCollider.steerAngle = 0;
    }

    public void UpdateVisual()
    {
        rotateWheels(FLWheelCollider, FLWheel);
        rotateWheels(FRWheelCollider, FRWheel);
        rotateWheels(BLWheelCollider, BLWheel);
        rotateWheels(BRWheelCollider, BRWheel);
    }

    private void rotateWheels(WheelCollider collider, Transform transformW)
    {
        Vector3 pos;
        Quaternion rot;

        collider.GetWorldPose(out pos, out rot);

        transformW.position = pos;
        transformW.rotation = rot;
    }
}

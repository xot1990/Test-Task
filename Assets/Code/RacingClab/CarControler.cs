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

    public Transform cameraPos;

    private bool leftRotation;
    private bool rightRotation;
    private bool move;
    private bool stop;

    private int direction = 1;

    private float angle;

    private void Update()
    {
        if (stop)
        {
            FLWheelCollider.brakeTorque = 3000f;
            FRWheelCollider.brakeTorque = 3000f;
            BRWheelCollider.brakeTorque = 3000f;
            BLWheelCollider.brakeTorque = 3000f;
        }
        else
        {
            FLWheelCollider.brakeTorque = 0f;
            FRWheelCollider.brakeTorque = 0f;
            BRWheelCollider.brakeTorque = 0f;
            BLWheelCollider.brakeTorque = 0f;
        }

        if (move)
        {
            FLWheelCollider.motorTorque = force * direction;
            FRWheelCollider.motorTorque = force * direction;
        }
        else
        {
            FLWheelCollider.motorTorque = 0;
            FRWheelCollider.motorTorque = 0;
        }

        if (leftRotation)
        {
            angle += maxAngle * -1 * Time.fixedDeltaTime;
            angle = Mathf.Clamp(angle, -maxAngle, maxAngle);

            FLWheelCollider.steerAngle = angle;
            FRWheelCollider.steerAngle = angle;
        }

        if (rightRotation)
        {
            angle += maxAngle * 1 * Time.fixedDeltaTime;
            angle = Mathf.Clamp(angle, -maxAngle, maxAngle);

            FLWheelCollider.steerAngle = angle;
            FRWheelCollider.steerAngle = angle;
        }

        if (!leftRotation && !rightRotation)
        {
            angle = 0;
            FLWheelCollider.steerAngle = 0;
            FRWheelCollider.steerAngle = 0;
        }

        UpdateVisual();
    }
    
    public int SwitchDirection()
    {
        if (direction == 1)
            direction = -1;
        else direction = 1;

        return direction;
    }

    public void GetMove(bool value)
    {
        move = value;
    }


    public void GetStop(bool value)
    {
        stop = value;
    }

    public void GetRotationLeft(bool value)
    {
        leftRotation = value;
    }

    public void GetRotationRight(bool value)
    {
        rightRotation = value;
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

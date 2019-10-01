using UnityEngine;

public class SBAgent : MonoBehaviour
{
    public Vector3 velocity = Vector3.zero;
    public float maxSpeed = 1f;
    public float maxSteer = 1f;
    Vector3 desired;
    Vector3 steer;
}

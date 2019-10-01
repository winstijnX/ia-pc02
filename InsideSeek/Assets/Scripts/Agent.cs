using UnityEngine;

public class Agent : SBAgent
{
    public Transform target;

    public enum INSIDE_TYPE
    {
        CIRCLE,
        RECTANGLE
    }

    public INSIDE_TYPE type = INSIDE_TYPE.CIRCLE;

    private Vector3 start_pos;
    private float radius = 2f;

    private float limitX = 10f;
    private float limitY = 5f;

    void Start()
    {
        maxSpeed = 2f;
        maxSteer = 1f;
        start_pos = transform.position;    
    }

    void Update()
    {
        velocity += SteeringBehaviour.Seek(this, target, 2f);
        if(type == INSIDE_TYPE.CIRCLE)
            velocity += SteeringBehaviour.InsideCircle(this, start_pos, radius, target);
        if(type == INSIDE_TYPE.RECTANGLE){}
            velocity += SteeringBehaviour.InsideRectangle(this, start_pos, limitX, limitY, target);

        transform.position += velocity * Time.deltaTime;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        if(type == INSIDE_TYPE.CIRCLE)
            Gizmos.DrawWireSphere(start_pos, radius);
        if(type == INSIDE_TYPE.RECTANGLE)
            Gizmos.DrawWireCube(start_pos, new Vector3(limitX, limitY));
    }
}

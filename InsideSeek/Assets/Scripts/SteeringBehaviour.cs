using UnityEngine;
using System.Collections.Generic;

public class SteeringBehaviour
{
    public static Vector3 Seek(SBAgent agent, Transform target, float range = 99999)
    {
        //Cálculo del vector deseado
        Vector3 desired = Vector3.zero;
        Vector3 difference = (target.position - agent.transform.position);
        float distance = difference.magnitude;
        desired = difference.normalized * agent.maxSpeed;

        //if(distance < range)
        //{
            return Vector3.ClampMagnitude(desired - agent.velocity, agent.maxSteer);
        //}
        //else
        //{
        //    return Vector3.zero;
        //}
    }

    public static Vector3 Flee(SBAgent agent, Transform target, float range = 99999)
    {
        Vector3 desired = Vector3.zero;
        Vector3 difference = -(target.position - agent.transform.position);
        float distance = difference.magnitude;
        desired = difference.normalized * agent.maxSpeed;

        //if(distance < range)
        //{
            return Vector3.ClampMagnitude(desired - agent.velocity, agent.maxSteer);
        //}
        //else
        //{
        //    return Vector3.zero;
        //}
    }

    public static Vector3 Arrive(SBAgent agent, Transform target, float range)
    {
        Vector3 desired;
        Vector3 difference = (target.position - agent.transform.position);
        float distance = difference.magnitude;

        desired = difference.normalized * agent.maxSpeed * ((distance > range)?1:(distance/range));

        //Cálculo de vectores
        Vector3 steer = Vector3.ClampMagnitude(desired - agent.velocity, agent.maxSteer);

        return steer;
    }

    public static Vector3 Separate(SBAgent agent, List<GameObject> agentsToAvoid, float range)
    {
        Vector3 steer = Vector3.zero;
        
        for(int i = 0; i < agentsToAvoid.Count; i++)
        {
            steer += Flee(agent, agentsToAvoid[i].transform, range);
        }

        return steer;
    }

    public static Vector3 InsideCircle(SBAgent agent, Vector3 center, float radius, Transform target)
    {
        Vector3 steer = Vector3.zero;

        float offset = (agent.transform.position - center).magnitude;

        if(offset > radius)
        {
            Vector3 desired = Vector3.zero;
            Vector3 difference = (center - agent.transform.position);
            float distance = difference.magnitude;
            desired = difference.normalized * agent.maxSpeed;

            steer = 2 * Vector3.ClampMagnitude(desired - agent.velocity, agent.maxSteer);
        }

        return steer;
    }

    public static Vector3 InsideRectangle(SBAgent agent, Vector3 center, float limitX, float limitY, Transform target)
    {
        Vector3 steer = Vector3.zero;
        Vector2 limits_x = new Vector2(center.x - limitX/2, center.x + limitX/2);
        Vector2 limits_y = new Vector2(center.y + limitY/2, center.y - limitY/2);

        if(agent.transform.position.x < limits_x.x || agent.transform.position.x > limits_x.y || agent.transform.position.y > limits_y.x || agent.transform.position.y < limits_y.y)
        {
            Vector3 desired = Vector3.zero;
            Vector3 difference = (center - agent.transform.position);
            float distance = difference.magnitude;
            desired = difference.normalized * agent.maxSpeed;

            steer = 2 * Vector3.ClampMagnitude(desired - agent.velocity, agent.maxSteer);
        }

        return steer;
    }
}

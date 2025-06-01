using UnityEngine;

public class HomingProjectileMovement : ProjectileMovement
{
    float turn_rate;

    public HomingProjectileMovement(float speed) : base(speed)
    {
        // turnRate in degrees per second, convert to radians per second
        turn_rate = 90f * Mathf.Deg2Rad;
    }

    public override void Movement(Transform transform, Vector3 unusedDirection)
    {
        GameObject target = GameManager.Instance.GetClosestEnemy(transform.position);

        Vector3 currentDirection = transform.forward;
        Vector3 newDirection = currentDirection;

        if (target != null)
        {
            Vector3 toTarget = (target.transform.position - transform.position).normalized;
            newDirection = Vector3.RotateTowards(currentDirection, toTarget, turn_rate * Time.deltaTime, 0.0f);
        }

        // Apply new rotation
        transform.rotation = Quaternion.LookRotation(newDirection);

        // Move forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
    }
}

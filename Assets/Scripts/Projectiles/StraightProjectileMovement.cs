using UnityEngine;

public class StraightProjectileMovement : ProjectileMovement
{
    public StraightProjectileMovement(float speed) : base(speed)
    {

    }

    public override void Movement(Transform transform, Vector3 direction)
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.Self);
    }
}

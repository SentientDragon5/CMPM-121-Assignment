using UnityEngine;

public class StraightProjectileMovement : ProjectileMovement
{
    public StraightProjectileMovement(float speed) : base(speed)
    {

    }

    public override void Movement(Transform transform, Vector3 direction)
    {
        transform.GetComponent<Rigidbody>().MovePosition(transform.position + direction * speed * Time.deltaTime);
    }
}

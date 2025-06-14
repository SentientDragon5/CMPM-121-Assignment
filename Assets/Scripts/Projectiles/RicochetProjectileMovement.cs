using UnityEngine;

public class RicochetProjectileMovement : ProjectileMovement
{
    private int bouncesRemaining = 1;

    public RicochetProjectileMovement(float speed) : base(speed) { }

    public override void Movement(Transform transform, Vector3 direction)
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    public bool TryBounce(Collision collision, Transform transform)
    {
        if (bouncesRemaining <= 0) return false;

        ContactPoint contact = collision.contacts[0];
        Vector3 newDirection = Vector3.Reflect(transform.forward, contact.normal);
        transform.rotation = Quaternion.LookRotation(newDirection, Vector3.up);

        bouncesRemaining--;
        return true;
    }

}
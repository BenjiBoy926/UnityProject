using UnityEngine;

public static class RigidbodyExtensions
{
    // 2D 

    public static void SetVelocity(this Rigidbody2D rigidbody, float velocity, Dimension dimension)
    {
        rigidbody.velocity = rigidbody.velocity.Set(velocity, dimension);
    }
}
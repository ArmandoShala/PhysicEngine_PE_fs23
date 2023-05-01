using UnityEngine;

public class SpringController : MonoBehaviour
{
    public float springConstant;
    public float initialLength;
    public float compressionSpeed;
    private Rigidbody collidingRigidbody;
    private Rigidbody _springRigidbody;

    private bool _isCompressed;

    private void Start()
    {
        springConstant = 1f;
        compressionSpeed = 0.038f;
        _springRigidbody = GetComponent<Rigidbody>();
        initialLength = _springRigidbody.position.x - transform.position.x;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!collider.attachedRigidbody) return;
        collidingRigidbody = collider.attachedRigidbody;
        _isCompressed = true;
    }

    private void OnTriggerExit(Collider collider)
    {
        if (!collider.attachedRigidbody) return;
        _isCompressed = false;
    }

    private void FixedUpdate()
    {
        if (!_isCompressed) return;
        var velocity = collidingRigidbody.velocity;
        collidingRigidbody.velocity = new Vector3(velocity.x - compressionSpeed, velocity.y, velocity.z);
        var compressionLength = initialLength - Mathf.Abs(transform.position.x - collidingRigidbody.position.x);
        collidingRigidbody.AddForce(springConstant * compressionLength * Vector3.right);
    }
}
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MortarShell : MonoBehaviour 
{
    public float Speed => _speed;
    [SerializeField] private float _speed = 15f;

    private Rigidbody _rigidbody = null;

    private void Awake()
    {
        _rigidbody = this.GetComponent<Rigidbody>();
    }

    public void SetVelocity(Vector3 velocity)
    {
        _rigidbody.AddForce(velocity, ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }
}

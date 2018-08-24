using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MortarShell : MonoBehaviour 
{
    public float Speed => _speed;
    [SerializeField] private float _speed = 15f;
    [SerializeField] private GameObject _explosionPrefab = null;

    private Rigidbody _rigidbody = null;

    public Transform Xform => _transform;
    private Transform _transform = null;

    private void Awake()
    {
        _rigidbody = this.GetComponent<Rigidbody>();
        _transform = this.GetComponent<Transform>();
    }

    public void SetVelocity(Vector3 velocity)
    {
        _rigidbody.AddForce(velocity, ForceMode.VelocityChange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_explosionPrefab != null && collision.contacts != null && collision.contacts.Length > 0)
        {
            ContactPoint contactPoint = collision.contacts[0];
            GameObject explosion = GameObject.Instantiate(_explosionPrefab, contactPoint.point, Quaternion.LookRotation(contactPoint.normal));

            Destroy(explosion, explosion.GetComponent<ParticleSystem>()?.main.duration ?? 2f);
        }
        Destroy(this.gameObject);
    }
}

using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour 
{
    [SerializeField] private float _speed = 2.5f;
    [SerializeField] private float _lifeSpan = 2f;

	private Transform _transform = null;
    public Vector3 Direction => _direction;
    private Vector3 _direction = Vector3.zero;

    public float Speed { get { return _speed; } }

    // Use this for initialization
	private void Awake() 
	{
		_transform = this.transform;
	}
	
	// Update is called once per frame
	private void Update () 
	{
        RaycastHit hitinfo;
        float step = _speed * Time.deltaTime;
        if (!Physics.Raycast(_transform.position, Direction, out hitinfo, step))
        {
            _transform.position += Direction * step;
        }
        else
        {
            if (hitinfo.collider.gameObject.GetComponent<Projectile>() != null)
            {
                Destroy(hitinfo.collider.gameObject);
            }
            Destroy(this.gameObject);
        }

        

        _lifeSpan -= Time.deltaTime;
        if (_lifeSpan <= 0)
        {
            Destroy(this.gameObject);
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
    }

    public void SetDirection(Vector3 direction)
    {
        if (direction.sqrMagnitude > 1)
        {
            direction.Normalize();
        }

        _direction = direction;
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }
}

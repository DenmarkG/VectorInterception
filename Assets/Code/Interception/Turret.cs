using UnityEngine;

public abstract class Turret : MonoBehaviour
{
    [SerializeField] protected GameObject _projectilePrefab = null;
    [SerializeField] protected Transform _spawnPoint = null;

    protected Projectile _target = null;
    protected Transform _transform = null;

    protected LineRenderer _lineRenderer = null;

    private void Awake()
    {
        _transform = this.transform;
        _lineRenderer = this.GetComponent<LineRenderer>();

        if (_lineRenderer != null)
        {
            _lineRenderer.startWidth = _lineRenderer.endWidth = .15f;
        }
    }

    public void SetTarget(Projectile target)
    {
        _target = target;
    }

    public abstract void FireProjectile();
}
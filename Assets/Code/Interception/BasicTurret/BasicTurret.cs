using UnityEngine;

public class BasicTurret : Turret
{
    public void Update()
    {
        if (_target != null)
        {
            Projectile projectile = _projectilePrefab.GetComponent<Projectile>();
            Vector3 direcion;
            bool canReachTarget = GetInterceptPosition(projectile, out direcion);
            if (canReachTarget)
            {
                Debug.DrawLine(_spawnPoint.position, _spawnPoint.position + direcion, Color.green);
            }
            else
            {
                Debug.DrawLine(_spawnPoint.position, _spawnPoint.position + direcion, Color.red);
            }

            _transform.rotation = Quaternion.LookRotation(direcion);
            ShowMarchingAnts(direcion, canReachTarget);
        }
    }

    public override void FireProjectile()
    {
        if (_target != null)
        {
            Projectile myProjectile = _projectilePrefab.GetComponent<Projectile>();
            Vector3 direction;
            if (GetInterceptPosition(myProjectile, out direction))
            {
                GameObject myProjectileObj = (GameObject)GameObject.Instantiate(_projectilePrefab, _spawnPoint.position, _spawnPoint.rotation);
                myProjectileObj.GetComponent<Projectile>().SetDirection(direction);
            }
        }
    }

    private bool GetInterceptPosition(Projectile projectile, out Vector3 direction)
    {
        // Velocity = Direction * Speed
        Vector3 targetVel = _target.Direction * _target.Speed;

        // relative pos = b - a
        Vector3 relativePosition = _target.transform.position - _spawnPoint.position;

        // a = (targetVel * targetVel) - (mySpeed * mySpeed)
        float a = Vector3.Dot(targetVel, targetVel) - (projectile.Speed * projectile.Speed);

        // b = 2 * (targetVel * relativePosition)
        float b = 2 * Vector3.Dot(targetVel, relativePosition);

        // c = (relativePos * relativePos)
        float c = Vector3.Dot(relativePosition, relativePosition);

        // calculate the determinant;
        float d = (b * b) - (4 * a * c);

        if (d > 0 && a != 0)
        {
            float det = Mathf.Sqrt(d);
            float t1 = (-b + det) / (2 * a);
            float t2 = (-b - det) / (2 * a);

            float t = 0;
            if (t1 > 0 || t2 > 0)
            {
                if (t1 > t2)
                {
                    t = t1;
                    direction = (_target.transform.position + (targetVel * t)) - _spawnPoint.position;
                    return true;
                }
                else
                {
                    t = t2;
                    direction = (_target.transform.position + (targetVel * t)) - _spawnPoint.position;
                    return true;
                }
            }
        }

        direction = relativePosition;
        return false;
    }

    private int _segments = 5;
    private float _marchSpeed = 2f;
    private Vector3[] _predictionLinePoints = null;

    private void ShowMarchingAnts(Vector3 direction, bool canReachTarget)
    {
        if (_lineRenderer != null)
        {
            int numPoints = _segments * 2;
            _predictionLinePoints = new Vector3[numPoints];

            Vector3 start;
            Vector3 next;
            for (int i = 0; i < _segments; ++i)
            {
                start = _spawnPoint.position + direction * (i / _segments);
            }


            _lineRenderer.SetPositions(new Vector3[] { _spawnPoint.position, _spawnPoint.position + direction });
            Color color = canReachTarget ? Color.green : Color.red;
            _lineRenderer.startColor = _lineRenderer.endColor = color;
        }
    }
}

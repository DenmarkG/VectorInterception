using UnityEngine;

public class MortarTurret : Turret 
{
    [SerializeField] private int _numCurvePoints = 8;

    private Vector3[] _curvePoints;

    private void Start()
    {
        if (_lineRenderer != null)
        {
            _lineRenderer.positionCount = _numCurvePoints + 1;
            _curvePoints = new Vector3[_numCurvePoints];
        }
    }

    private void Update()
    {
        if (_lineRenderer != null && _target != null)
        {
            Vector3 currentPoint;
            Vector3 direction = _target.Xform.position;
            MortarShell shell = _projectilePrefab.GetComponent<MortarShell>();
            for (int i = 1; i <= _numCurvePoints; ++i)
            {
                float gravity = Mathf.Abs(Physics.gravity.y);

                float range = direction.magnitude;
                direction.Normalize();
                direction *= shell.Speed;

                float flightTime = range / shell.Speed;
                float yVel = .5f * gravity * flightTime;

                currentPoint = new Vector3(direction.x, yVel, direction.z);
                Debug.Log($"CurrentPoint[{i}] : {currentPoint}");
                _curvePoints[i - 1] = currentPoint;
            }

            _lineRenderer.SetPositions(_curvePoints);
        }
    }

    public override void FireProjectile()
    {
        if (_target != null)
        {
            MortarShell shell = _projectilePrefab.GetComponent<MortarShell>();
            Vector3 direction = _target.Xform.position;

            //bool canReachTarget = GetInterceptPosition(shell, out direction);

            Vector3 initialVelocity = CalculateInitialVelocity(direction, shell.Speed);
            GameObject liveShell = GameObject.Instantiate(_projectilePrefab, _spawnPoint.position, _spawnPoint.rotation);
            liveShell.GetComponent<MortarShell>().SetVelocity(initialVelocity);
        }
    }

    private Vector3 CalculateInitialVelocity(Vector3 direction, float launchPower)
    {
        float gravity = Mathf.Abs(Physics.gravity.y);

        float range = direction.magnitude;
        direction.Normalize();
        direction *= launchPower;

        float flightTime = range / launchPower;
        float yVel = .5f * gravity * flightTime;

        return new Vector3(direction.x, yVel, direction.z);
    }

    //private bool GetInterceptPosition(MortarShell shell, out Vector3 direction)
    //{
    //    // Velocity = Direction * Speed
    //    Vector3 targetVel = _target.Direction * _target.Speed;

    //    // relative pos = b - a
    //    Vector3 relativePosition = _target.transform.position - _spawnPoint.position;

    //    // a = (targetVel * targetVel) - (mySpeed * mySpeed)
    //    float a = Vector3.Dot(targetVel, targetVel) - (shell.Speed * shell.Speed);

    //    // b = 2 * (targetVel * relativePosition)
    //    float b = 2 * Vector3.Dot(targetVel, relativePosition);

    //    // c = (relativePos * relativePos)
    //    float c = Vector3.Dot(relativePosition, relativePosition);

    //    // calculate the determinant;
    //    float d = (b * b) - (4 * a * c);

    //    if (d > 0 && a != 0)
    //    {
    //        float det = Mathf.Sqrt(d);
    //        float t1 = (-b + det) / (2 * a);
    //        float t2 = (-b - det) / (2 * a);

    //        float t = 0;
    //        if (t1 > 0 || t2 > 0)
    //        {
    //            if (t1 > t2)
    //            {
    //                t = t1;
    //                direction = (_target.transform.position + (targetVel * t)) - _spawnPoint.position;
    //                return true;
    //            }
    //            else
    //            {
    //                t = t2;
    //                direction = (_target.transform.position + (targetVel * t)) - _spawnPoint.position;
    //                return true;
    //            }
    //        }
    //    }

    //    direction = relativePosition;
    //    return false;
    //}

}

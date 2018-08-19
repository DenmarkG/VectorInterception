using UnityEngine;

public class MortarDemo : MonoBehaviour 
{
    [SerializeField] private Camera _mainCamera = null;
    [SerializeField] private MortarTurret _turret = null;
    [SerializeField] private GameObject _projectilePrefab = null;

    private bool _isMouseDown = false;
    private Vector3 _mouseStartPosition = new Vector3();
    private Vector3 _mouseEndPosition = new Vector3();
    
    private Projectile _activeTarget = null;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isMouseDown = true;

            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                _mouseStartPosition = hit.point;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _isMouseDown = false;

            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                _mouseEndPosition = hit.point;
            }

            Vector3 direction = _mouseEndPosition - _mouseStartPosition;
            if (direction.sqrMagnitude < 1)
            {
                direction.Normalize();
                direction *= 5f;
            }

            GameObject obj = GameObject.Instantiate(_projectilePrefab, _mouseStartPosition + Vector3.up, Quaternion.LookRotation(direction));

            _activeTarget = obj.GetComponent<Projectile>();
            _activeTarget.SetDirection(direction);
            _activeTarget.SetSpeed(direction.magnitude);

            _turret.SetTarget(_activeTarget);
        }

        if (!_isMouseDown && Input.GetKeyUp(KeyCode.Space))
        {
            _turret.FireProjectile();
        }
    }
}

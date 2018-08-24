using UnityEngine;

public class MortarDemo : MonoBehaviour 
{
    [SerializeField] private Camera _mainCamera = null;
    [SerializeField] private MortarTurret _turret = null;
    [SerializeField] private GameObject _projectilePrefab = null;

    private Vector3 _mouseStartPosition = new Vector3();
    
    private Projectile _activeTarget = null;

    private void Update()
    {
        Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000f, ~GameConstants.ProjectileLayer))
        {
            _mouseStartPosition = hit.point;
        }

        if (Input.GetMouseButtonDown(0))
        {
            GameObject obj = GameObject.Instantiate(_projectilePrefab, _mouseStartPosition + Vector3.up, Quaternion.identity);
            _activeTarget = obj.GetComponent<Projectile>();
            _activeTarget.SetDirection(Vector3.zero);
            _activeTarget.SetSpeed(0f);

            _turret.SetTarget(_activeTarget);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _activeTarget = null;
            _turret.FireProjectile();
        }

        if (_activeTarget != null)
        {
            _activeTarget.Xform.position = _mouseStartPosition;
        }
    }
}

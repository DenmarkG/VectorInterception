using UnityEngine;
using System.Collections;

public class MainLogic : MonoBehaviour 
{
    [SerializeField] private GameObject m_bulletPrefab = null;
    [SerializeField] private GameObject m_cannonObj = null;
    private Cannon m_cannon = null;
    private Camera cam;

    void Awake()
    {
        cam = this.GetComponent<Camera>();
        if (m_cannonObj != null)
        {
            m_cannon = m_cannonObj.GetComponent<Cannon>();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Projectile bullet = FireProjectile(Input.mousePosition);
            if (m_cannon != null)
            {
                m_cannon.FireProjectile(bullet);
            }
        }
    }

    Projectile FireProjectile(Vector3 position)
    {
        position.z = cam.nearClipPlane;
        Vector3 worldPos = cam.ScreenToWorldPoint(position);
        GameObject bulletObj = (GameObject)GameObject.Instantiate(m_bulletPrefab, worldPos, m_bulletPrefab.transform.rotation);
        Projectile bullet = bulletObj.GetComponent<Projectile>();
        bullet.SetDirection(worldPos - this.transform.position.normalized);
        return bullet;
    }
}

using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour 
{
    [SerializeField] private Transform m_bulletSpawn = null;
    [SerializeField] private GameObject m_bulletPrefab = null;

    public void FireProjectile(Projectile target)
    {
        Projectile myProjectile = m_bulletPrefab.GetComponent<Projectile>();
        Vector3 relPos = target.transform.position - m_bulletSpawn.position;
        float a = Vector3.Dot(target.Speed * target.Direction, target.Speed * target.Direction) - (myProjectile.Speed * myProjectile.Speed);

        float b = 2 * Vector3.Dot(target.Speed * target.Direction, relPos);
        float c = Vector3.Dot(relPos, relPos);

        // calculate the determinant;
        float d = (b * b) - (4 * a * c);

        if (d > 0 && a != 0)
        {
            float det = Mathf.Sqrt(d);
            float t1 = (-b + det) / (2 * a);
            float t2 = (-b - det) / (2 * a);
            float t;
            if (t1 > t2 && t2 > 0)
            {
                if (myProjectile.Speed > target.Speed)
                {
                    t = t1;
                }
                else
                {
                    t = t2;
                }
                
            }
            else
            {
                //t = t1;
                if (myProjectile.Speed < target.Speed)
                {
                    t = t1;
                }
                else
                {
                    t = t2;
                }
            }

            GameObject myProjectileObj = (GameObject) GameObject.Instantiate(m_bulletPrefab, m_bulletSpawn.position, m_bulletSpawn.rotation);
            myProjectile = myProjectileObj.GetComponent<Projectile>();
            myProjectile.SetDirection((target.transform.position + (target.Direction * target.Speed * t)) - m_bulletSpawn.transform.position);
        }
    }
}

using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour 
{
    [SerializeField] private float m_speed = 2.5f;
    [SerializeField] private float m_lifeSpan = 2f;

	private Transform m_transform = null;
    public Vector3 Direction { get; set; }
    public float Speed { get { return m_speed; } }

    // Use this for initialization
	void Awake() 
	{
        Direction = Vector3.zero;
		m_transform = this.transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
        RaycastHit hitinfo;
        float step = m_speed * Time.deltaTime;
        if (!Physics.Raycast(m_transform.position, Direction, out hitinfo, step))
        {
            m_transform.position += Direction * step;
        }
        else
        {
            if (hitinfo.collider.gameObject.GetComponent<Bullet>() != null)
            {
                Destroy(hitinfo.collider.gameObject);
            }
            Destroy(this.gameObject);
        }

        

        m_lifeSpan -= Time.deltaTime;
        if (m_lifeSpan <= 0)
        {
            Destroy(this.gameObject);
        }
	}

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("hit");
        Destroy(this.gameObject);
    }
}

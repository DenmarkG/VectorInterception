// The proper way to orbit an object using quaternions
using UnityEngine;
using System.Collections;

public class OrbitObject : MonoBehaviour
{
	[SerializeField] private float m_rotSpeed = 3f;
	[SerializeField] private float m_pivotDistance = 5f;
	[SerializeField] private Transform m_pivot = null;
	
	private Quaternion m_destRotation = Quaternion.identity;
	private float m_rotX = 0f;
	private float m_rotY = 0f;
	
	private void Update()
	{
		float horz = Input.GetAxis("Horizontal");
		float vert = Input.GetAxis("Vertical");
		
		m_rotX += vert * Time.deltaTime * m_rotSpeed;
		m_rotY += horz * Time.deltaTime * m_rotSpeed;
		
		Quaternion yRot = Quaternion.Euler(0f, m_rotY, 0f);
		m_destRotation = yRot * Quaternion.Euler(m_rotX, 0f, 0f); // NOT COMMUTATIVE!!!!
		
		this.transform.rotation = m_destRotation;
		
		// Adjust the position
		this.transform.position = m_pivot.transform.position + this.transform.rotation * Vector3.forward * -m_pivotDistance;
	}
}
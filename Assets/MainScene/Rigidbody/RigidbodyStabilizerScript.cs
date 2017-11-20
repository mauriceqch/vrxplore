using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyStabilizerScript : MonoBehaviour {
	private Rigidbody m_RigidBody;
	private CapsuleCollider m_Capsule;
	private bool m_PreviouslyGrounded, m_IsGrounded;
	private Vector3 m_GroundContactNormal;
	public float groundCheckDistance = 0.1f;
	public float shellOffset = 0f;

	private void Start()
	{
		m_RigidBody = GetComponent<Rigidbody>();
		m_Capsule = GetComponent<CapsuleCollider>();
	}
	
	private void FixedUpdate()
	{
		GroundCheck();

		if (m_IsGrounded)
		{
			m_RigidBody.drag = 5f;
		}
		else
		{
			m_RigidBody.drag = 0f;
		}
	}

	/// sphere cast down just beyond the bottom of the capsule to see if the capsule is colliding round the bottom
	private void GroundCheck()
	{
		m_PreviouslyGrounded = m_IsGrounded;
		RaycastHit hitInfo;
		if (Physics.SphereCast(transform.position, m_Capsule.radius * (1.0f - shellOffset), Vector3.down, out hitInfo,
			((m_Capsule.height/2f) - m_Capsule.radius) + groundCheckDistance, Physics.AllLayers, QueryTriggerInteraction.Ignore))
		{
			m_IsGrounded = true;
			m_GroundContactNormal = hitInfo.normal;
		}
		else
		{
			m_IsGrounded = false;
			m_GroundContactNormal = Vector3.up;
		}
	}
}

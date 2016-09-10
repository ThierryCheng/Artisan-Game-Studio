using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {

	private GameObject m_DirectionalLight;
	private GameObject m_Violemon;
	void Start () {
		m_DirectionalLight = GameObject.Find ("Directional Light");
		if(m_DirectionalLight == null)
		{
			return;
		}
		m_Violemon = GameObject.Find ("Violemon");
		if(m_DirectionalLight == null)
		{
			return;
		}
		//m_DirectionalLight.transform.LookAt (m_Violemon.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		m_DirectionalLight.transform.Rotate(0, 20 * Time.deltaTime, 0);
	}
}

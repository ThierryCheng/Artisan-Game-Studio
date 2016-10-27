using UnityEngine;
using System.Collections;

namespace AGS.World
{
	public class OutLine : MonoBehaviour {
		private Renderer renderer;
		private Material m_NormalMat;
		protected Material  m_OutLineMat;
		protected bool m_UseOutLine = false;
		// Use this for initialization

		void OnMouseEnter()
		{
			if (m_UseOutLine == true && m_OutLineMat != null && renderer != null) {
				renderer.material = m_OutLineMat;
			}
		}

		protected void Start()
		{
			renderer = gameObject.GetComponentInChildren<Renderer> ();
			if (renderer != null) {
				m_NormalMat = renderer.material;
			}
		}

		void OnMouseDown ()
		{

		}

		void OnMouseExit()
		{
			if (m_UseOutLine == true) {
				renderer.material = m_NormalMat;
			}
		}
	}
}
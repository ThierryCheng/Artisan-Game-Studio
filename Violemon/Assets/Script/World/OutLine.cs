using UnityEngine;
using System.Collections;

namespace AGS.World
{
	public class OutLine : MonoBehaviour {
		private Renderer renderer;
		protected bool m_UseOutLine = false;
		private static Shader m_OutLineShader;
		private Shader m_OriShader;
		protected Color m_OutLineColor;

		void OnMouseEnter()
		{
			if (m_UseOutLine == true && m_OutLineShader != null && renderer != null) {
				renderer.material.shader = m_OutLineShader;
				renderer.material.SetColor("_OutlineColor", m_OutLineColor);
				renderer.material.SetColor("_Color", Color.white);
			}
		}

		protected void Start()
		{
			if (m_OutLineShader == null) {
				m_OutLineShader = (Shader)Resources.Load ("Shaders/OutLine");
			}

			renderer = gameObject.GetComponentInChildren<Renderer> ();
			m_OutLineColor = Color.red;
			if (renderer != null) {
				m_OriShader = renderer.material.shader;
			}
		}

		void OnMouseDown ()
		{

		}

		void OnMouseExit()
		{
			if (m_UseOutLine == true) {
				renderer.material.shader = m_OriShader;
			}
		}
	}
}
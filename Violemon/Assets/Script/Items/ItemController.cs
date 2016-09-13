using UnityEngine;
using System.Collections;

namespace AGS.Items
{
	public class ItemController : MonoBehaviour {

		private GameObject m_Violemon;

		// Use this for initialization
		void Start () {
			m_Violemon = GameObject.Find ("Violemon");
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		void OnMouseOver()
		{
			Debug.Log ("over");
		}
	}
}
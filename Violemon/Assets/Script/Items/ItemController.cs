using UnityEngine;
using System.Collections;
using AGS.Characters;
using AGS.World;

namespace AGS.Items
{
	public class ItemController : OutLine {

		private GameObject m_Violemon;
		private Item m_ItemInfo;
		public Item ItemInfo
		{
			set
			{
				m_ItemInfo = value;
			}
			get
			{
				return m_ItemInfo;
			}
		}

		// Use this for initialization
		void Start () {
			base.Start ();
			this.m_UseOutLine = true;
			this.m_OutLineColor = Color.white;
			m_Violemon = GameObject.Find ("Violemon");
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		void OnMouseOver()
		{
			//Debug.Log ("over");
		}

		void OnMouseDown ()
		{
			Player player = m_Violemon.GetComponent<Player> ();
			player.Pick (this.gameObject);
		}
	}
}
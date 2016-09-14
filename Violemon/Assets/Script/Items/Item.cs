using UnityEngine;
using System.Collections;
using AGS.Language;

namespace AGS.Items
{
	public class Item {
		
		public string m_ItemID;

		public string m_ItemName
		{
			get
			{
				return AGSLanguage.Instance().GetText(m_ItemID + "_Name");
			}
		}

		public string m_ItemDesc
		{
			get
			{
				return AGSLanguage.Instance().GetText(m_ItemID + "_Desc");
			}
		}

		public int    m_ItemCount = 1;

		public Item Clone()
		{
			return null;
		}
	}
}
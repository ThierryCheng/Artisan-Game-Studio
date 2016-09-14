using UnityEngine;
using System.Collections;

namespace AGS.Language
{
	public class AGSLanguage {
		private static AGSLanguage m_Instance;
		public static AGSLanguage Instance()
		{
			if (m_Instance == null) {
				m_Instance = new AGSLanguage();
			}
			return m_Instance;
		}
		
		private AGSLanguage(){
		}
		
		public void Init()
		{

		}

		public string GetText(string id)
		{
			string text = null;
			if (id.Equals ("Item_Apple_Name")) {
				text = "Apple";
			}
			return text;
		}
	}
}
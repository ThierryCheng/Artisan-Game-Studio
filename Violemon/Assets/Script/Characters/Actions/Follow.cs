using UnityEngine;
using System.Collections;
using AGS.Items;

namespace AGS.Characters
{
	public class Follow : AGSAction {
		
		private BaseCharacter m_BaseCharacter;
		private GameObject    m_ActionTarget;
		
		public GameObject TargetObj()
		{
			return m_ActionTarget;
		}
		
		public Follow(BaseCharacter m_BaseCharacter, GameObject m_ActionTarget)
		{
			this.m_BaseCharacter = m_BaseCharacter;
			this.m_ActionTarget  = m_ActionTarget;
		}
		
		
		public void StartAction ()
		{

		}
		
		// Update is called once per frame
		public void StopAction ()
		{
			
		}
		
		public bool CanStartAction ()
		{
			if(Vector3.Distance (m_BaseCharacter.Position(), m_ActionTarget.transform.position) <= 5f)
			{
				return true;
			} else {
				return false;
			}
		}
		
		public void ActionCallBack (string name)
		{
			
		}
		
		public void BeforeChangeTarget()
		{
		}
	}
}
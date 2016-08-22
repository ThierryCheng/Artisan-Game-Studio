using UnityEngine;
using System.Collections;
namespace AGS.Config
{
    public class LayerManager{
		private int groundLayerIndex = -1;
		private int humanLayerIndex = -1;


		private static LayerManager m_Instance;
		public static LayerManager Instance()
		{
			if(m_Instance == null)
			{
				m_Instance = new LayerManager();
			}
			return m_Instance;
		}

		public int GetGroundLayerIndex()
		{
			if(groundLayerIndex == -1)
			{
				groundLayerIndex = LayerMask.GetMask ("GroundCollider");
			}
			return groundLayerIndex;
		}

		public int GetHumanLayerIndex()
		{
			if(humanLayerIndex == -1)
			{
				humanLayerIndex = LayerMask.GetMask ("Human");
			}
			return humanLayerIndex;
		}
    }
}
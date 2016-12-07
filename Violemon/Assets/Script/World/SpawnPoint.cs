using UnityEngine;
using System.Collections;

namespace AGS.World
{
	public class SpawnPoint : MonoBehaviour {

		void OnDrawGizmos()
		{
			Gizmos.DrawSphere (transform.position, 1f);
		}
	}
}
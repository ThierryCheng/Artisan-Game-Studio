using UnityEngine;
using System.Collections;

namespace AGS.Spawn
{
	public class SpawnPoint : MonoBehaviour {

		void OnDrawGizmos()
		{
			Gizmos.DrawSphere (transform.position, 1f);
		}
	}
}
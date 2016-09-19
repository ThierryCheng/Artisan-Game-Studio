using UnityEngine;
using System.Collections;

namespace AGS.Items
{
	public class FlyItem : MonoBehaviour {
		
		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			Vector3 moveAmount = transform.TransformDirection (Vector3.forward) * 12 * Time.deltaTime;
			transform.position = transform.position + moveAmount;
		}
	}
}
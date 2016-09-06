using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace AGS.UI{
	
	public class HealthBar : MonoBehaviour {

		public GameObject player;
		public float barShowTime = 3;
		public float timer;
		private Canvas bar;
		private Slider barSlider;
        private Transform father;
        // Use this for initialization
        void Start ()
        {
			timer = 0;
			bar = transform.GetComponent<Canvas> ();
            //father = transform.GetComponentInParent<Transform>();
			barSlider = bar.GetComponentInChildren<Slider> ();
		}

		// Update is called once per frame
		void Update () {
            //debug.log("fater x = " + father.position.x);
            //debug.log("bar x = " + bar.transform.position.x);
            //debug.log("fater y = " + father.position.y);
            //debug.log("bar y = " + bar.transform.position.y);
            //debug.log("fater z = " + father.position.z);
            //debug.log("bar z = " + bar.transform.position.z);
            transform.LookAt (player.transform);
			transform.rotation = Camera.main.transform.rotation;
			if(barSlider.IsActive()){
				timer += Time.deltaTime;
				if(timer>=barShowTime){
					barSlider.enabled = false;
					bar.enabled = false;
					timer = 0;
				}
			}
			//transform.LookAt(Camera.main.transform);
		}
	}
}
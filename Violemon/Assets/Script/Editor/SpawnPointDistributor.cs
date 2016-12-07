using UnityEngine;
using UnityEditor;
using System.Collections;
using AGS.World;

namespace AGS.Editors
{
	public class SpawnPointDistributor : EditorWindow {
		RaycastHit _hitInfo;  
		SceneView.OnSceneFunc _delegate;  
		static SpawnPointDistributor _windowInstance;  
		GameObject t;
		GameObject quad;
		string myString = "Hello World";
		//string[] radioNames;// = new string[]{"Apple", "Violemon", "HumanKnight"};
		bool[] radioStates;// = new bool[]{false, false, false};
		int radioInt = 0;
		float myFloat = 1.23f;
		int multipleAmount;
		float scaleElement;
		[MenuItem("AGS/Distributer #`")]  
		static void Init()  
		{  
			if(_windowInstance == null)  
			{  
				_windowInstance = EditorWindow.GetWindow(typeof(SpawnPointDistributor)) as SpawnPointDistributor;  
				_windowInstance._delegate = new SceneView.OnSceneFunc(OnSceneFunc);  
				SceneView.onSceneGUIDelegate += _windowInstance._delegate;  


			}  
		}  

		void LoadResources()
		{
			//Debug.Log ("Load Resources");
			//t = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Resources/Prefabs/Items/Apple", typeof(GameObject));
			//t = (GameObject)Resources.Load ("Prefabs/Items/Apple");
			GameObject oriQuad = (GameObject)Resources.Load ("Prefabs/World/Quad");
			quad = (GameObject)EditorUtility.InstantiatePrefab(oriQuad);
			quad.transform.position = new Vector3 (0, -10, 0);
			//quad.GetComponent<Collider> ().enabled = false;
			Debug.Log ("Generated quad");
			SpawnObjs.Instance ().LoadObjs ();
			radioStates = new bool[SpawnObjs.Instance().m_ObjsMap.Length];
			multipleAmount = 10;
			scaleElement = 0.2f;
		}

		void OnEnable()  
		{  
			LoadResources ();
		}  

		void OnDisable()  
		{  
		}  

		void OnDestroy()  
		{  
			//Debug.Log("OnDestroy");  
			if (_delegate != null)  
			{  
				Debug.Log("OnDestroyed"); 
				SceneView.onSceneGUIDelegate -= _delegate;  
			}  

			if (quad != null) {
				Debug.Log ("Destroied quad");
				GameObject.DestroyImmediate (quad);
			}
			GameObject[] objs = GameObject.FindGameObjectsWithTag ("Quad");
			foreach (GameObject obj in objs) {
				GameObject.DestroyImmediate (obj);
			}
		}  



		void OnGUI()  
		{  
			//Debug.Log ("GUI");
			GUILayout.Label ("Base Settings", EditorStyles.boldLabel);
			multipleAmount = EditorGUILayout.IntSlider (multipleAmount, 1, 10);
			scaleElement = EditorGUILayout.Slider (scaleElement, 0f, 0.5f);
			for (int i = 0; i < SpawnObjs.Instance().m_ObjsMap.Length; i++) {
				if (radioStates [i] != EditorGUILayout.Toggle (SpawnObjs.Instance().m_ObjsMap [i][0], radioStates [i])) {
					radioStates [i] = !radioStates [i];//改变选择状态
					t = SpawnObjs.Instance().m_GameObjs[i];
					if (radioInt != i)
						radioStates [radioInt] = false;
					radioInt = i;
					//obj = CloneTransfrom [i];
				} 
			}
			//myString = EditorGUILayout.TextField ("Text Field", myString);

			//apple = EditorGUILayout.Toggle ("Apple", apple);
			//violemon = EditorGUILayout.Toggle ("Violemon", violemon);
			//myFloat = EditorGUILayout.Slider ("Slider", myFloat, -3, 3);
			//EditorGUILayout.EndToggleGroup ();
		}  

		static public void OnSceneFunc(SceneView sceneView)  
		{  
			_windowInstance.CustomSceneGUI(sceneView);  
		}  

		void CustomSceneGUI(SceneView sceneView)  
		{  
			Camera cameara = sceneView.camera;  
			Event e = Event.current;
			Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);  


			//if (e.isKey && e.character == 'a')
			if (Physics.Raycast(ray, out _hitInfo, 100000, -1))  
			{  
				//Debug.DrawRay(ray.origin, ray.direction, Color.yellow);  
				Vector3 origin = _hitInfo.point;  
				origin.y += 100;
				RaycastHit[] hits = Physics.RaycastAll (origin, Vector3.down);
				foreach(RaycastHit hit in hits)
				{
					if (IsLandTag (hit.collider.gameObject.tag)) {
						Handles.color = Color.yellow;  
						Handles.DrawLine (_hitInfo.point, origin);  

						float arrowSize = 1;  
						Vector3 pos = _hitInfo.point;  
						Quaternion quat;  
						Handles.color = Color.green;  
						quat = Quaternion.LookRotation (Vector3.up, Vector3.up);  
						Handles.ArrowCap (0, pos, quat, arrowSize);  
						Handles.color = Color.red;  
						quat = Quaternion.LookRotation (Vector3.right, Vector3.up);  
						Handles.ArrowCap (0, pos, quat, arrowSize);  
						Handles.color = Color.blue;  
						quat = Quaternion.LookRotation (Vector3.forward, Vector3.up);  
						Handles.ArrowCap (0, pos, quat, arrowSize);  
						//Handles.DrawLine(pos + new Vector3(0, 3, 0), pos);  
						if (e.isKey && e.character == 'z') {
							Debug.Log (e.button + "  :  " + e.clickCount);
							//GameObject obj = (GameObject)Instantiate(t);
							//obj.transform.position = _hitInfo.point;
							//Object prefab = EditorUtility.GetPrefabParent(t);
							//Debug.Log ("is prefab: " + prefab);
							//if(prefab)
							//{
							//	GameObject obj;
							//		obj = (GameObject)EditorUtility.InstantiatePrefab(prefab);
							//		obj.transform.position = _hitInfo.point;
							//	}
							SpawnOne (_hitInfo.point, 0f, 0f, 1f);
							//Debug.Log (Event.current.button + "  :  " + Event.current.delta.x + "  :  " + Event.current.delta.y);
						} else if (e.isKey && e.character == 'a') {
							SpawnOne (_hitInfo.point, 0f, 0f, 1f);
							//RandomSpawn (_hitInfo.point, 0f);
						} else if (e.isKey && e.character == 'b') {
							MultipleSpawn (_hitInfo.point, 0f);
						} else if (e.isKey && e.character == 'c') {
							RandomSpawn (_hitInfo.point, 0.5f);
						}
						break;
					}
				}
				/*if (Physics.Raycast(origin, Vector3.down, out _hitInfo))  
				{  
					if (IsLandTag(_hitInfo.collider.gameObject.tag)) 
					{
						Handles.color = Color.yellow;  
						Handles.DrawLine (_hitInfo.point, origin);  

						float arrowSize = 1;  
						Vector3 pos = _hitInfo.point;  
						Quaternion quat;  
						Handles.color = Color.green;  
						quat = Quaternion.LookRotation (Vector3.up, Vector3.up);  
						Handles.ArrowCap (0, pos, quat, arrowSize);  
						Handles.color = Color.red;  
						quat = Quaternion.LookRotation (Vector3.right, Vector3.up);  
						Handles.ArrowCap (0, pos, quat, arrowSize);  
						Handles.color = Color.blue;  
						quat = Quaternion.LookRotation (Vector3.forward, Vector3.up);  
						Handles.ArrowCap (0, pos, quat, arrowSize);  
						//Handles.DrawLine(pos + new Vector3(0, 3, 0), pos);  
						if (e.isKey && e.character == 'z') {
							Debug.Log (e.button + "  :  " + e.clickCount);
							//GameObject obj = (GameObject)Instantiate(t);
							//obj.transform.position = _hitInfo.point;
							//Object prefab = EditorUtility.GetPrefabParent(t);
							//Debug.Log ("is prefab: " + prefab);
							//if(prefab)
							//{
							//	GameObject obj;
							//		obj = (GameObject)EditorUtility.InstantiatePrefab(prefab);
							//		obj.transform.position = _hitInfo.point;
							//	}
							SpawnOne (_hitInfo.point, 0f, 0f, 1f);
							//Debug.Log (Event.current.button + "  :  " + Event.current.delta.x + "  :  " + Event.current.delta.y);
						} else if (e.isKey && e.character == 'a') {
							SpawnOne (_hitInfo.point, 0f, 0f, 1f);
							//RandomSpawn (_hitInfo.point, 0f);
						} else if (e.isKey && e.character == 'b') {
							MultipleSpawn (_hitInfo.point, 0f);
						} else if (e.isKey && e.character == 'c') {
							RandomSpawn (_hitInfo.point, 0.5f);
						}


					}
				}*/  
			}

			if (e.isKey && e.character == 'd') {
				if(Physics.Raycast(ray, out _hitInfo, 100000, -1))
				{
					//Debug.Log ("11111111");
					if (_hitInfo.collider.gameObject.tag == "Quad") {
						//Debug.Log ("2222222");
						//quad.GetComponent<Collider> ().enabled = true;
						RandomSpawnLand (_hitInfo.point, 0f);
						//quad.GetComponent<Collider> ().enabled = false;
					}
				}
			}
			//if (e.isMouse && e.button != 0) {

			//Debug.Log (Event.current.button + "  :  " + Event.current.delta.x + "  :  " + Event.current.delta.y);
			SceneView.RepaintAll();  
		}

		private bool IsLandTag(string tagName)
		{
			if (tagName.Equals ("Forestland") || tagName.Equals ("Frozenland") || tagName.Equals ("Grassland") || tagName.Equals ("Swampland") || tagName.Equals ("Wasteland")) {
				return true;
			} else {
				return false;
			}
		}

		private bool SpawnOne(Vector3 position, float rotation, float height, float scale)
		{

			GameObject obj;
			CapsuleCollider capsule = t.GetComponent<CapsuleCollider> ();
			Collider collider = t.GetComponent<Collider> ();
			float redius = 0.5f;
			if (capsule != null) {
				redius = capsule.radius;
			} else if (collider != null) {
				redius = 0.5f;
			} 
			Vector3 v = new Vector3 (position.x , position.y + 50, position.z);

			if (Physics.SphereCast (v, redius, Vector3.down,out _hitInfo,100f)) {
				float dot = Vector3.Dot (_hitInfo.normal, Vector3.up);
				if (IsLandTag(_hitInfo.collider.gameObject.tag) && dot >= 0.9f) {
					string folderName = t.name + "_folder";
					GameObject folder = GameObject.Find (folderName);
					if (folder == null) {
						folder = new GameObject ();
						folder.name = folderName;
					}

					Undo.IncrementCurrentGroup();
					obj = (GameObject)EditorUtility.InstantiatePrefab(t);
					obj.transform.position = new Vector3(_hitInfo.point.x, _hitInfo.point.y + height, _hitInfo.point.z);
					obj.transform.Rotate (0, rotation, 0);
					if (scale != 1f) {
						//Debug.Log ("change scale " + scale);
						obj.transform.localScale = new Vector3(scale, scale, scale);
					}
					Undo.RegisterCreatedObjectUndo(obj, "Create " + obj.name);
					obj.transform.parent = folder.transform;
					return true;
				}
			}
			return false;
		}

		/*private bool SpawnOne(Vector3 position, float rotation, float height, float scale)
		{
			
			GameObject obj;
			CapsuleCollider capsule = t.GetComponent<CapsuleCollider> ();
			Collider collider = t.GetComponent<Collider> ();
			float redius = 0.5f;
			if (capsule != null) {
				redius = capsule.radius;
			} else if (collider != null) {
				redius = 0.5f;
			} 
			Vector3 v = new Vector3 (position.x , position.y + 50, position.z);
			RaycastHit[] hits = Physics.SphereCastAll (v, redius, Vector3.down, 100f);
			foreach(RaycastHit hit in hits)
			{
				if (hit.collider.transform.tag.Equals ("Quad")) {
					continue;
				} else if (!IsLandTag (hit.collider.transform.tag)) {
					return false;
				} else if (IsLandTag (hit.collider.transform.tag)) {
					float dot = Vector3.Dot (hit.normal, Vector3.up);
					if (dot >= 0.9f) {
						Undo.IncrementCurrentGroup();
						obj = (GameObject)EditorUtility.InstantiatePrefab(t);
						obj.transform.position = new Vector3(hit.point.x, hit.point.y + height, hit.point.z);
						obj.transform.Rotate (0, rotation, 0);
						if (scale != 1f) {
							Debug.Log ("change scale " + scale);
							obj.transform.localScale = new Vector3(scale, scale, scale);
						}
						Undo.RegisterCreatedObjectUndo(obj, "Create " + obj.name);
						return true;
					}
				}
					
			}
			return false;
		}*/



		private bool RandomSpawn(Vector3 position, float height)
		{
			float random = Random.value;
			//float scale = Random.value * scaleElement * 2 + (1f - scaleElement);
			float scale = Random.value * scaleElement * 2 + 1f;
			float rotation = Mathf.Lerp (0f, 360f, random);
			return SpawnOne (position, rotation, height, scale);
		}

		private bool RandomSpawnLand(Vector3 position, float height)
		{
			float random = Random.value;
			float rotation = Mathf.Lerp (0f, 360f, random);
			float scale = Random.value * 2f + 1f;
			return SpawnOneLand (position, rotation, height, scale);
		}

		private bool SpawnOneLand(Vector3 position, float rotation, float height, float scale)
		{
			GameObject obj;
			string folderName = t.name + "_folder";
			GameObject folder = GameObject.Find (folderName);
			if (folder == null) {
				folder = new GameObject ();
				folder.name = folderName;
			}
			Undo.IncrementCurrentGroup();
			obj = (GameObject)EditorUtility.InstantiatePrefab(t);
			obj.transform.position = new Vector3(_hitInfo.point.x, _hitInfo.point.y + height + 5f, _hitInfo.point.z);
			obj.transform.Rotate (0, rotation, 0);
			if (scale != 1f) {
				//Debug.Log ("!!!!!!!change scale " + scale);
				obj.transform.localScale = new Vector3(scale, 1f, scale);
				//Debug.Log ("!!!!!!!changed scale " + obj.transform.localScale);
			}
		    Undo.RegisterCreatedObjectUndo(obj, "Create " + obj.name);
			obj.transform.parent = folder.transform;
			return true;
	    }

		private void MultipleSpawn(Vector3 position, float height)
		{
			float rx, rz, dis;
			Vector3 angle;
			bool re;
			for (int i = 0; i < multipleAmount; i++) {
				for (int j = 0; j < 3; j++) {
					rx = Random.value * 2f - 1f;
					rz = Random.value * 2f - 1f;
					dis = Random.value * 10f;
					angle = new Vector3 (rx, 0f ,rz).normalized;
					re = RandomSpawn (position + angle * dis, height);
					if (re) {
						break;
					}
				}
			}
		}
	}
}
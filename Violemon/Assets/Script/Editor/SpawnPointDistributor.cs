using UnityEngine;
using UnityEditor;
using System.Collections;
using AGS.Spawn;

namespace AGS.Editors
{
	public class SpawnPointDistributor : EditorWindow {
		RaycastHit _hitInfo;  
		SceneView.OnSceneFunc _delegate;  
		static SpawnPointDistributor _windowInstance;  
		GameObject t;
		string myString = "Hello World";
		//string[] radioNames;// = new string[]{"Apple", "Violemon", "HumanKnight"};
		bool[] radioStates;// = new bool[]{false, false, false};
		int radioInt = 0;
		float myFloat = 1.23f;

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

			radioStates = new bool[SpawnObjs.Instance().m_ObjsMap.Length];
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
		}  



		void OnGUI()  
		{  
			//Debug.Log ("GUI");
			GUILayout.Label ("Base Settings", EditorStyles.boldLabel);

			for (int i = 0; i < SpawnObjs.Instance().m_ObjsMap.Length; i++) {
				if (radioStates [i] != EditorGUILayout.Toggle (SpawnObjs.Instance().m_ObjsMap [i][1], radioStates [i])) {
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
			if (Physics.Raycast(ray, out _hitInfo, 10000, -1))  
			{  
				//Debug.DrawRay(ray.origin, ray.direction, Color.yellow);  
				Vector3 origin = _hitInfo.point;  
				origin.y += 100;  
				if (Physics.Raycast(origin, Vector3.down, out _hitInfo))  
				{  
					Handles.color = Color.yellow;  
					Handles.DrawLine(_hitInfo.point, origin);  

					float arrowSize = 1;  
					Vector3 pos = _hitInfo.point;  
					Quaternion quat;  
					Handles.color = Color.green;  
					quat = Quaternion.LookRotation(Vector3.up, Vector3.up);  
					Handles.ArrowCap(0, pos, quat, arrowSize);  
					Handles.color = Color.red;  
					quat = Quaternion.LookRotation(Vector3.right, Vector3.up);  
					Handles.ArrowCap(0, pos, quat, arrowSize);  
					Handles.color = Color.blue;  
					quat = Quaternion.LookRotation(Vector3.forward, Vector3.up);  
					Handles.ArrowCap(0, pos, quat, arrowSize);  
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
						SpawnOne (_hitInfo.point, 0f, 0f);
						//Debug.Log (Event.current.button + "  :  " + Event.current.delta.x + "  :  " + Event.current.delta.y);
					} else if (e.isKey && e.character == 'a') {
						RandomSpawn (_hitInfo.point, 0f);
					} else if (e.isKey && e.character == 'b') {
						MultipleSpawn (_hitInfo.point, 0f);
					} else if (e.isKey && e.character == 'c') {
						RandomSpawn (_hitInfo.point, 0.5f);
					}
				}  
			}  
			//if (e.isMouse && e.button != 0) {

			//Debug.Log (Event.current.button + "  :  " + Event.current.delta.x + "  :  " + Event.current.delta.y);
			SceneView.RepaintAll();  
		}

		private bool SpawnOne(Vector3 position, float rotation, float height)
		{
			
			GameObject obj;
			CapsuleCollider capsule = t.GetComponent<CapsuleCollider> ();
			Collider collider = t.GetComponent<Collider> ();
			float redius;
			if (capsule != null) {
				redius = capsule.radius;
			} else if (collider != null) {
				redius = 0.5f;
			} else {
				throw new UnityException ("Selected prefabe has no collider!");
			}
			Vector3 v = new Vector3 (position.x , position.y + 50, position.z);

			if (Physics.SphereCast (v, redius, Vector3.down,out _hitInfo,100f)) {
				float dot = Vector3.Dot (_hitInfo.normal, Vector3.up);
				if (_hitInfo.collider.gameObject.tag.Equals ("Land") && dot >= 0.9f) {
					Undo.IncrementCurrentGroup();
					obj = (GameObject)EditorUtility.InstantiatePrefab(t);
					obj.transform.position = new Vector3(_hitInfo.point.x, _hitInfo.point.y + height, _hitInfo.point.z);
					obj.transform.Rotate (0, rotation, 0);
					Undo.RegisterCreatedObjectUndo(obj, "Create " + obj.name);
					return true;
				}
			}
			return false;
		}

		private bool RandomSpawn(Vector3 position, float height)
		{
			float random = Random.value;
			float rotation = Mathf.Lerp (0f, 360f, random);
			return SpawnOne (position, rotation, height);
		}

		private void MultipleSpawn(Vector3 position, float height)
		{
			float rx, rz, dis;
			Vector3 angle;
			bool re;
			for (int i = 0; i < 10; i++) {
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
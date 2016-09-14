using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AGS.Items
{
	public class ItemManager{
		private static Dictionary<string,ItemTemp>  m_ItemTemplates = new Dictionary<string,ItemTemp> ();
		private static ItemManager m_Instance;
		public static ItemManager Instance()
		{
			if (m_Instance == null) {
				m_Instance = new ItemManager();
			}
			return m_Instance;
		}

		private ItemManager(){
		}

		public void Init()
		{
			ItemTemp temp;
			temp = new ItemTemp ();
			Consumable consumableItem;
			consumableItem = new Consumable ();
			consumableItem.m_ItemID = ItemIDs.Item_Apple;
			consumableItem.m_FeededPoint = 3f;
			temp.m_GameObject = (GameObject)Resources.Load ("Prefabs/Items/Apple");
			temp.m_Item = consumableItem;
			AddItem (temp.m_Item.m_ItemID, temp);
			//Debug.Log ("44444" + consumableItemTemp.m_GameObject);
		}

		private void AddItem(string key, ItemTemp item)
		{
			if (m_ItemTemplates.ContainsKey (key)) {
				throw new UnityException("Duplicated id: " + key);
			}

			m_ItemTemplates.Add (key, item);
		}

		public ItemTemp GetItemTemplate(string key)
		{
			return m_ItemTemplates [key];
		}

		public GameObject GenerateItem(string key, Vector3 position)
		{
			if (!m_ItemTemplates.ContainsKey (key)) {
				return null;
			}
			//Debug.Log ("123123" + m_ItemTemplates [key].m_GameObject);
			GameObject obj = (MonoBehaviour.Instantiate (m_ItemTemplates [key].m_GameObject, position + Vector3.up * 3, Quaternion.identity) as GameObject);
			Rigidbody rigidBody = obj.GetComponent<Rigidbody>();
			rigidBody.velocity = Vector3.up * 2;
			ItemController controller = obj.GetComponent<ItemController>();
			controller.ItemInfo = m_ItemTemplates [key].m_Item.Clone ();
			return obj;
		}
	}
}
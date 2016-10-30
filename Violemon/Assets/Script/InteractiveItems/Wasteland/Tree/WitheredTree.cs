using UnityEngine;
using System.Collections;
using AGS.World;

public class WitheredTree : OutLine {

	// Use this for initialization
	void Start () {
		base.Start ();
		this.m_UseOutLine = true;
		this.m_OutLineColor = Color.white;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

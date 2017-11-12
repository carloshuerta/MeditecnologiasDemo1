using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        
	}

    public GameObject btn1;
    public GameObject btn2;


    // Update is called once per frame
    void Update () {
		if (StaticSceneStates.Autenticated)
        {
            TextMesh txtMesh = (TextMesh)GetComponent("TextMesh");
            txtMesh.color = Color.green;
            btn2.SetActive(true);
        }
	}
}

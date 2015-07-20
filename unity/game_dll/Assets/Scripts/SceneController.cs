using UnityEngine;
using SimpleJSON;
using System.Collections;
using System;
using System.Runtime.InteropServices;


public class SceneController : MonoBehaviour {

	// Use this for initialization
	public GameObject jeep;
	public GameObject fltire, frtire, bltire, brtire;
	[DllImport("game_dll")]
	static extern IntPtr API_Update_Frame ();
	[DllImport("game_dll")]
	static extern void API_Init ();
	[DllImport("game_dll")]
	static extern void API_Free_Game ();

	private Hashtable name_obj;
	string[] obj_list = {"body", "fltire", "frtire", "bltire", "brtire"};
	void Start () {
		API_Init ();
		name_obj = new Hashtable ();
		name_obj.Add ("body", jeep);
		name_obj.Add ("fltire", fltire);
		name_obj.Add ("frtire", frtire);
		name_obj.Add ("bltire", bltire);
		name_obj.Add ("brtire", brtire);
	}

	/*void setPos(GameObject obj, var jv)
	{
		float x = jv ["pos"] [0].AsFloat, y = jv ["pos"] [1].AsFloat, z = jv ["pos"] [2].AsFloat;
		jeep.transform.position = new Vector3 (x, y, z);
		float rx = jv ["ori"] [0].AsFloat, ry = jv ["ori"] [1].AsFloat, rz = jv ["ori"] [2].AsFloat;
		jeep.transform.rotation = Quaternion.Euler (rx * 180 / 3.14F, ry * 180 / 3.14F , rz * 180 / 3.14F);
	}*/
	// Update is called once per frame
	void Update () {
		string s = Marshal.PtrToStringAnsi (API_Update_Frame ());
		var j = JSONNode.Parse(s);
		foreach (string name in obj_list) {
			// setPos(name_obj[name], j[name]);
			GameObject obj = (GameObject) name_obj[name];
			var jv = j[name];
			float x = jv ["pos"] [0].AsFloat, y = jv ["pos"] [1].AsFloat, z = jv ["pos"] [2].AsFloat;
			obj.transform.position = new Vector3 (x, y, z);
			float rw = jv ["ori"] [0].AsFloat, rx = jv ["ori"] [1].AsFloat, ry = jv ["ori"] [2].AsFloat, rz = jv ["ori"] [3].AsFloat;
			obj.transform.rotation = new Quaternion(rx, ry, rz, rw);

		}		
		jeep.transform.Translate (new Vector3 (0, -2, 0));
	}

	void OnDestroy(){
		API_Free_Game ();
	}
}

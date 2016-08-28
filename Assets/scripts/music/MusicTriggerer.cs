using UnityEngine;
using System.Collections;

public class MusicTriggerer : MonoBehaviour {

	[SerializeField] MusicManager m_musicManager;
	bool m_launched = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D _other){
		
		if (!m_launched && _other.gameObject.layer == LayerMask.NameToLayer ("Note")) {
			m_launched = true;
			m_musicManager.StartMusic ();
			gameObject.SetActive (false);
		}
	}
}

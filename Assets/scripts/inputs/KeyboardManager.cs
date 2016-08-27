using UnityEngine;
using System.Collections;

/// <summary>
/// This class is a prototype, not to be used on device
/// </summary>
public class BattleInputKeyboard : MonoBehaviour {

    [SerializeField] string m_inputAttack = "right";
    [SerializeField] string m_inputDefend = "left";

    // Use this for initialization
    void Start () {
		if (Application.platform == RuntimePlatform.Android) {
			enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	bool CheckInput( string _input, int _index){
		if (Input.GetKeyDown (_input)) {
            
			return true;
		} else if( Input.GetKeyUp(_input) )
        {
			return true;
		}
		return false;
	}
}

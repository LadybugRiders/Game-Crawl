using UnityEngine;
using System.Collections;

/// <summary>
/// This class is a prototype, not to be used on device
/// </summary>
public class KeyboardManager : MonoBehaviour {

	[SerializeField] string m_inputRight = "right";
	[SerializeField] string m_inputLeft = "left";

	// Use this for initialization
    void Start () {
		if (Application.platform == RuntimePlatform.Android) {
			enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		CheckInput (m_inputRight);
		CheckInput (m_inputLeft);
	}

	bool CheckInput( string _input){
		if (Input.GetKeyDown (_input)) {
			var player = GameObject.Find("Player").GetComponent<PlayerDebug>();
			MoveCommand command = new MoveCommand ();
			MoveCommandParameter param;
			if (_input == m_inputRight) {
				param = new MoveCommandParameter (MoveCommand.Direction.RIGHT, player);
			} else {
				param = new MoveCommandParameter (MoveCommand.Direction.LEFT, player);
			}
			CommandManager.instance.AddCommand (command,param);
			return true;
		} else if( Input.GetKeyUp(_input) )
        {
			return true;
		}
		return false;
	}
}

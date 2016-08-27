using UnityEngine;
using System.Collections;

public class CommandManager : MonoBehaviour {

	private static CommandManager s_instance;

	void Awake(){
		s_instance = this;
	}

	public void AddCommand(Command _command, CommandParameter _parameter){
		_command.Execute (_parameter);
	}

	public static CommandManager instance{
		get{
			if (s_instance == null) {
				GameObject go = new GameObject ("CommandManager");
				s_instance = go.AddComponent<CommandManager> ();
			}
			return s_instance;
		}
	}
}

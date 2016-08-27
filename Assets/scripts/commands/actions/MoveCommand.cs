using UnityEngine;
using System.Collections;

public class MoveCommand : Command {

	public enum Direction{ RIGHT, LEFT };

	public override void Execute (CommandParameter _parameter)
	{
		base.Execute (_parameter);
		MoveCommandParameter param = (MoveCommandParameter)_parameter;
		switch (param.Direction) {
			case Direction.LEFT: 
				param.Player.MoveLeft ();
			break;
			case Direction.RIGHT: 
				param.Player.MoveRight ();
				break;
		}
	}
}

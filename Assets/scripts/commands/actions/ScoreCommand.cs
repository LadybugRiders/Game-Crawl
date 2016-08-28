using UnityEngine;
using System.Collections;

public class ScoreCommand : Command {

	public override void Execute (CommandParameter _parameter)
	{
		base.Execute (_parameter);

		ScoreCommandParameter param = (ScoreCommandParameter)_parameter;
	}
}

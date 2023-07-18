using TMPro;
using UnityEngine;

public class DebugLogNode : ScratchNode
{
	[SerializeField] private TMP_InputField Input;

	protected override void OnStartNodeBehaviourExecution() => Debug.Log(Input.text);
}

using TMPro;
using UnityEngine;

public class SetPositionAndRotationNode : ScratchNode
{
	[SerializeField, Space] private TMP_InputField XPosition_Input;
	[SerializeField] private TMP_InputField YPosition_Input;
	[SerializeField] private TMP_InputField ZPosition_Input;

	[SerializeField, Space] private TMP_InputField XRotation_Input;
	[SerializeField] private TMP_InputField YRotation_Input;
	[SerializeField] private TMP_InputField ZRotation_Input;

	protected override void OnStartNodeBehaviourExecution()
	{
		Vector3 movementVector = new(float.Parse(XPosition_Input.text), float.Parse(YPosition_Input.text), float.Parse(ZPosition_Input.text));
		Vector3 rotationEulerVector = new(float.Parse(XRotation_Input.text), float.Parse(YRotation_Input.text), float.Parse(ZRotation_Input.text));
		ScratchManager.Instance.Target.transform.SetPositionAndRotation(movementVector, Quaternion.Euler(rotationEulerVector));
	}
}

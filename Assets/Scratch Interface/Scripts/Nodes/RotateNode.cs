using TMPro;
using UnityEngine;

public class RotateNode : ScratchNode
{
	[SerializeField] private TMP_InputField X_Input;
	[SerializeField] private TMP_InputField Y_Input;
	[SerializeField] private TMP_InputField Z_Input;

	protected override void OnStartNodeBehaviourExecution()
	{
		Vector3 rotationEulerVector = new(float.Parse(X_Input.text), float.Parse(Y_Input.text), float.Parse(Z_Input.text));
		ScratchManager.Instance.Target.transform.eulerAngles += rotationEulerVector;
	}
}

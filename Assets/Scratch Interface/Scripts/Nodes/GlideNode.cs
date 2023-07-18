using TMPro;
using UnityEngine;

public class GlideNode : ScratchCoroutineNode
{
	[SerializeField, Space] private TMP_InputField X_Input;
	[SerializeField] private TMP_InputField Y_Input;
	[SerializeField] private TMP_InputField Z_Input;
	[SerializeField] private TMP_InputField Duration_Input;

	protected override Coroutine NodeBehaviourCoroutine()
	{
		float glideDuration = float.Parse(Duration_Input.text);
		Vector3 startGlidePosition = ScratchManager.Instance.Target.transform.position;
		Vector3 endGlidePosition = new(float.Parse(X_Input.text), float.Parse(Y_Input.text), float.Parse(Z_Input.text));

		return this.Tween01(glideDuration, t =>
	   {
		   ScratchManager.Instance.Target.transform.position = Vector3.Lerp(startGlidePosition, endGlidePosition, t);
		   if (ProgressFillImage)
		   {
			   ProgressFillImage.fillAmount = t;
		   }
	   });
	}
}

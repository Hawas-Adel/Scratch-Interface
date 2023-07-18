using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class ScratchCoroutineNode : ScratchNode
{
	[SerializeField, Space] protected GameObject ActiveToggleObject;
	[SerializeField] protected Image ProgressFillImage;

	private void Awake()
	{
		if (ActiveToggleObject)
		{
			ActiveToggleObject.SetActive(false);
		}

		if (ProgressFillImage)
		{
			ProgressFillImage.fillAmount = 0;
		}
	}

	protected override void OnStartNodeBehaviourExecution() { }

	public override void ExecuteNodeBehaviour() => StartCoroutine(ExecuteNodeBehaviourCOR());
	private IEnumerator ExecuteNodeBehaviourCOR()
	{
		if (ActiveToggleObject)
		{
			ActiveToggleObject.SetActive(true);
		}

		OnStartNodeBehaviourExecution();
		yield return NodeBehaviourCoroutine();

		if (ActiveToggleObject)
		{
			ActiveToggleObject.SetActive(false);
		}

		if (ProgressFillImage)
		{
			ProgressFillImage.fillAmount = 0;
		}

		if (!nextNode)
		{
			yield break;
		}

		nextNode.ExecuteNodeBehaviour();
	}

	protected abstract Coroutine NodeBehaviourCoroutine();
}

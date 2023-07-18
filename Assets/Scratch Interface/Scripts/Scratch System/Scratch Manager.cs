using UnityEngine;
using UnityEngine.UI;

public class ScratchManager : SingletonMonoBehavior<ScratchManager>
{
	[SerializeField] private GameObject Target;

	[field: SerializeField, Space] public RectTransform ScratchCanvas { get; private set; }

	[SerializeField, Space] private Button StartButton;
	[SerializeField] private Button ResetButton;

	public event System.Action OnStartExecution;

	protected override void Awake()
	{
		base.Awake();
		StartButton.onClick.AddListener(StartExecution);
		ResetButton.onClick.AddListener(ResetTarget);
	}

	public void StartExecution() => OnStartExecution?.Invoke();
	private void ResetTarget()
	{
		Target.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
		Target.transform.localScale = Vector3.one;
	}

	public bool CanvasContainsPoint(Vector3 point) => ScratchCanvas.rect.Contains(ScratchCanvas.InverseTransformPoint(point));
}

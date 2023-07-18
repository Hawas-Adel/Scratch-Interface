using UnityEngine;

public class ScratchNodesPreviewList : UIElement<ScratchNode[]>
{
	[SerializeField] private Transform ListUIParent;
	[SerializeField] private ScratchNode[] NodePrefabs;

	private void Awake() => Bind(NodePrefabs);

	protected override void _Bind(ScratchNode[] value)
	{
		foreach (var node in NodePrefabs)
		{
			Instantiate(node, ListUIParent);
		}
	}

	protected override void _UnBind(ScratchNode[] value) => ListUIParent.DestroyAllChildren();
}

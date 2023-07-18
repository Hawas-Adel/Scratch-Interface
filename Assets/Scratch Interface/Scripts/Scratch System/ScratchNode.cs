using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class ScratchNode : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	#region Node Is In Canvas Handling
	private static List<ScratchNode> nodesInCanvas = new();
	public bool IsInCanvas => nodesInCanvas.Contains(this);

	public void DropInCanvas()
	{
		nodesInCanvas.Add(this);
		transform.SetParent(ScratchManager.Instance.ScratchCanvas, true);
		TrySnapToAnyNearbyNode();
		OnAddedToCanvas();
	}
	public void RemoveFromCanvas()
	{
		nodesInCanvas.Remove(this);
		DisconnectFomPreviousNode();
		OnRemovedFromCanvas();
	}
	protected virtual void OnAddedToCanvas() { }
	protected virtual void OnRemovedFromCanvas() { }
	#endregion

	#region Node Snapping to other Nodes
	private const float _SnappingMinDistance = 30f;

	[SerializeField] private Transform UpperConnectionNode;
	[SerializeField] private Transform LowerConnectionNode;

	private void TrySnapToAnyNearbyNode()
	{
		if (!UpperConnectionNode)
		{
			return;
		}

		foreach (ScratchNode node in nodesInCanvas)
		{
			if (CanSnapToNode(node))
			{
				SnapPositionToNode(node);
				ConnectToPreviousNode(node);
				return;
			}
		}
	}

	private void TrySnapPositionToAnyNearbyNode()
	{
		if (!UpperConnectionNode)
		{
			return;
		}

		foreach (ScratchNode node in nodesInCanvas)
		{
			if (CanSnapToNode(node))
			{
				SnapPositionToNode(node);
				return;
			}
		}
	}

	private bool CanSnapToNode(ScratchNode node)
	{
		if (node == this)
		{
			return false;
		}

		if (!node.LowerConnectionNode)
		{
			return false;
		}

		if (node.nextNode)
		{
			return false;
		}

		if (Vector3.Distance(node.LowerConnectionNode.position, UpperConnectionNode.position) > _SnappingMinDistance)
		{
			return false;
		}

		return true;
	}

	private void SnapPositionToNode(ScratchNode node) => transform.position = node.transform.position + node.LowerConnectionNode.localPosition - UpperConnectionNode.localPosition;

	private void ConnectToPreviousNode(ScratchNode node) => node.nextNode = this;

	private void DisconnectFomPreviousNode()
	{
		foreach (ScratchNode node in nodesInCanvas)
		{
			if (node.nextNode == this)
			{
				node.nextNode = null;
				return;
			}
		}
	}
	#endregion

	#region Node Execution Logic
	[System.NonSerialized] public ScratchNode nextNode;

	public virtual void ExecuteNodeBehaviour()
	{
		OnStartNodeBehaviourExecution();
		if (!nextNode)
		{
			return;
		}

		nextNode.ExecuteNodeBehaviour();
	}

	protected abstract void OnStartNodeBehaviourExecution();
	#endregion

	#region Drag / Drop
	private ScratchNode draggedNode = null;
	void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
	{
		if (IsInCanvas)
		{
			draggedNode = this;
			draggedNode.RemoveFromCanvas();
		}
		else
		{
			draggedNode = Instantiate(this, ScratchManager.Instance.transform);
		}
	}

	void IDragHandler.OnDrag(PointerEventData eventData)
	{
		draggedNode.transform.position = eventData.position;
		draggedNode.TrySnapPositionToAnyNearbyNode();
		DragNextNode(draggedNode);
	}

	private void DragNextNode(ScratchNode node)
	{
		if (!node.nextNode)
		{
			return;
		}

		node.nextNode.SnapPositionToNode(node);
		DragNextNode(node.nextNode);
	}

	void IEndDragHandler.OnEndDrag(PointerEventData eventData)
	{
		if (!ScratchManager.Instance.CanvasContainsPoint(eventData.position))
		{
			Destroy(draggedNode.gameObject);
			return;
		}

		draggedNode.DropInCanvas();
		draggedNode = null;
	}
	#endregion
}
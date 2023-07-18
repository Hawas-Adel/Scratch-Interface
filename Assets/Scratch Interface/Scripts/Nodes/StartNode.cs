public class StartNode : ScratchNode
{
	protected override void OnStartNodeBehaviourExecution() { }

	protected override void OnAddedToCanvas() => ScratchManager.Instance.OnStartExecution += ExecuteNodeBehaviour;
	protected override void OnRemovedFromCanvas() => ScratchManager.Instance.OnStartExecution -= ExecuteNodeBehaviour;
}

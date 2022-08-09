using UnityEngine;

namespace Hai.FestraGenerator.Scripts.Editor.Internal.FestraAac
{
    public abstract class FestraAacAnimatorNode
    {
        protected internal abstract Vector3 GetPosition();
        protected internal abstract void SetPosition(Vector3 position);
    }

    public abstract class FestraAacAnimatorNode<TNode> : FestraAacAnimatorNode where TNode : FestraAacAnimatorNode<TNode>
    {
        protected readonly FestraAacFlStateMachine ParentMachine;
        protected readonly IFestraAacDefaultsProvider DefaultsProvider;

        protected FestraAacAnimatorNode(FestraAacFlStateMachine parentMachine, IFestraAacDefaultsProvider defaultsProvider)
        {
            ParentMachine = parentMachine;
            DefaultsProvider = defaultsProvider;
        }

        public TNode LeftOf(TNode otherNode) => MoveNextTo(otherNode, -1, 0);
        public TNode RightOf(TNode otherNode) => MoveNextTo(otherNode, 1, 0);
        public TNode Over(TNode otherNode) => MoveNextTo(otherNode, 0, -1);
        public TNode Under(TNode otherNode) => MoveNextTo(otherNode, 0, 1);

        public TNode LeftOf() => MoveNextTo(null, -1, 0);
        public TNode RightOf() => MoveNextTo(null, 1, 0);
        public TNode Over() => MoveNextTo(null, 0, -1);
        public TNode Under() => MoveNextTo(null, 0, 1);

        public TNode Shift(TNode otherState, int shiftX, int shiftY) => MoveNextTo(otherState, shiftX, shiftY);
        public TNode At(int shiftX, int shiftY)
        {
            SetPosition(new Vector3(shiftX * DefaultsProvider.Grid().x, shiftY * DefaultsProvider.Grid().y, 0));
            return (TNode) this;
        }

        private TNode MoveNextTo(TNode otherStateOrSecondToLastWhenNull, int x, int y)
        {
            if (otherStateOrSecondToLastWhenNull == null)
            {
                var siblings = ParentMachine.GetChildNodes();
                var other = siblings[siblings.Count - 2];
                Shift(other.GetPosition(), x, y);

                return (TNode) this;
            }

            Shift(otherStateOrSecondToLastWhenNull.GetPosition(), x, y);

            return (TNode) this;
        }

        public TNode Shift(Vector3 otherPosition, int shiftX, int shiftY)
        {
            SetPosition(otherPosition + new Vector3(shiftX * DefaultsProvider.Grid().x, shiftY * DefaultsProvider.Grid().y, 0));
            return (TNode) this;
        }
    }
}

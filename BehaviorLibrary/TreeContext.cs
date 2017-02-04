using BehaviorLibrary.Components;
using System.Collections.Generic;

namespace BehaviorLibrary
{
    public class TreeContext
    {
        private List<BehaviorComponent> calling;
        private List<BehaviorComponent> called;

        public TreeContext()
        {
            this.calling = new List<BehaviorComponent>();
            this.called = new List<BehaviorComponent>();
        }

        public TreeContext(TreeContext oldContext) : this()
        {
            this.OldContext = oldContext;
        }

        public TreeContext OldContext { get; private set; }

        public void AddToCalling(BehaviorComponent behavior)
        {
            this.calling.Add(behavior);
        }

        public void AddToCalled(BehaviorComponent behavior)
        {
            this.called.Add(behavior);
        }

        public bool HasBeenCalled(BehaviorComponent behavior)
        {
            return this.called.Contains(behavior);
        }

        public bool IsCalling(BehaviorComponent behavior)
        {
            return this.calling.Contains(behavior);
        }
    }
}

using BehaviorLibrary.Components;
using BehaviorLibrary.Components.Actions;
using System.Collections.Generic;

namespace BehaviorLibrary
{
    public class TreeContext
    {
        private List<BehaviorComponent> calling;
        private List<BehaviorComponent> called;
        private int callCount;
        private List<OnNotCalled> onNotCalledBehaviors;
        private bool isInvokingFinalizer;

        public TreeContext()
        {
            this.calling = new List<BehaviorComponent>();
            this.called = new List<BehaviorComponent>();
            this.onNotCalledBehaviors = new List<OnNotCalled>();
        }

        public BehaviorComponent[] Calling {  get { return this.calling.ToArray(); } }
        public BehaviorComponent[] Called { get { return this.called.ToArray(); } }

        public TreeContext(TreeContext oldContext) : this()
        {
            this.OldContext = oldContext;
        }

        public CallHierarchy CallingHierarchy
        {
            get
            {
                return new CallHierarchy(this.calling);
            }
        }

        public TreeContext OldContext { get; private set; }

        private void AddToCalling(BehaviorComponent behavior)
        {
            this.calling.Add(behavior);
        }

        private void AddToCalled(BehaviorComponent behavior)
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

        public void OnAboutToCall(BehaviorComponent caller)
        {
            this.AddToCalling(caller);
            this.callCount++;
        }

        public void RegisterOnNotCalledBehavior(OnNotCalled onNotCalledBehavior)
        {
            this.onNotCalledBehaviors.Add(onNotCalledBehavior);
        }

        public void OnCalled(BehaviorComponent called, BehaviorReturnCode result)
        {
            this.AddToCalled(called);

            // Check if we just called the root node, so are done with the tree.
            if (!this.isInvokingFinalizer && this.calling.Count > 0 && this.calling[0] == called)
            {
                this.ProcessFinalizers(this.calling[0]);
            }
        }

        private void ProcessFinalizers(BehaviorComponent rootNode)
        {
            if (this.OldContext == null) return;
            foreach (var onNotCalledBehavior in this.OldContext.onNotCalledBehaviors)
            {
                if (!this.HasBeenCalled(onNotCalledBehavior))
                {
                    this.isInvokingFinalizer = true;
                    onNotCalledBehavior.InvokeFinalizer(this);
                    this.isInvokingFinalizer = false;
                }
            }
        }
    }
}
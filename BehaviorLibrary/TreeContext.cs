using BehaviorLibrary.Actions;
using BehaviorLibrary.Components;
using System.Collections.Generic;

namespace BehaviorLibrary
{
    public class TreeContext
    {
        private List<BehaviorComponent> calling;
        private List<BehaviorComponent> called;
        private int callCount;
        private List<OnNotCalled> onNotCalledBehaviors;

        public TreeContext()
        {
            this.calling = new List<BehaviorComponent>();
            this.called = new List<BehaviorComponent>();
            this.onNotCalledBehaviors = new List<OnNotCalled>();
        }

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

            // TODO: If we don't match the call tree, we don't know before whether or not something is going to be called.
            // So all we can do is check after everything has been called and then fire those that weren't called.
            foreach (var onNotCalledBehavior in this.OldContext.onNotCalledBehaviors)
            {
                if (onNotCalledBehavior.CanInvokeFinalizer(this, caller))
                {
                    onNotCalledBehavior.InvokeFinalizer(this);
                }
            }
        }

        public void RegisterOnNotCalledBehavior(OnNotCalled onNotCalledBehavior)
        {
            this.onNotCalledBehaviors.Add(onNotCalledBehavior);
        }

        public void OnCalled(BehaviorComponent called, BehaviorReturnCode result)
        {
            this.AddToCalled(called);
        }
    }
}
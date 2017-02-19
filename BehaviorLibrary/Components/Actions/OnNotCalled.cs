namespace BehaviorLibrary.Components.Actions
{
    /// <summary>
    /// Provides a way to specifiy behavior for when a behavior is no longer called, due to a different subtree being executed.
    /// </summary>
    public class OnNotCalled : BehaviorComponent
    {
        private BehaviorComponent finalizer;
        private CallHierarchy lastCallHierarchy;
        private bool handledThisTick;

        public OnNotCalled(BehaviorComponent finalizer)
        {
            this.finalizer = finalizer;
        }

        public override BehaviorReturnCode OnBehave(TreeContext context)
        {
            this.lastCallHierarchy = context.CallingHierarchy;
            this.handledThisTick = false;
            context.RegisterOnNotCalledBehavior(this);
            return BehaviorReturnCode.Success;
        }

        public bool CanInvokeFinalizer(TreeContext context, BehaviorComponent caller)
        {
            if (this.handledThisTick) return true;

            // Check if call hierarchies differ.
            var currentHierarchy = context.CallingHierarchy;
            if (context.OldContext != null)
            {
                return !currentHierarchy.Matches(context.OldContext.CallingHierarchy);
            }

            return false;
        }

        public BehaviorReturnCode InvokeFinalizer(TreeContext context)
        {
            this.handledThisTick = true;
            return this.finalizer.Behave(context);
        }
    }
}
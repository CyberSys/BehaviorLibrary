namespace BehaviorLibrary.Components
{
    public abstract class  BehaviorComponent
    {
        protected BehaviorReturnCode ReturnCode;

        public BehaviorComponent() { }

        public abstract BehaviorReturnCode OnBehave(TreeContext context);

        public BehaviorReturnCode Behave(TreeContext context)
        {
            context.OnAboutToCall(this);
            this.ReturnCode = this.OnBehave(context);
            context.OnCalled(this, this.ReturnCode);
            return this.ReturnCode;
        }
    }
}
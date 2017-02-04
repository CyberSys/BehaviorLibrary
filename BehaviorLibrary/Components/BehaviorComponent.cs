namespace BehaviorLibrary.Components
{
    public abstract class  BehaviorComponent
    {
        protected BehaviorReturnCode ReturnCode;

        public BehaviorComponent() { }

        public abstract BehaviorReturnCode OnBehave(TreeContext context);

        public BehaviorReturnCode Behave(TreeContext context)
        {
            context.AddToCalling(this);
            var result = this.OnBehave(context);
            this.ReturnCode = result;
            context.AddToCalled(this);
            return result;
        }
    }
}
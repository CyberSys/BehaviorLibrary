using BehaviorLibrary.Components.Conditionals;

namespace BehaviorLibrary.Components.Composites
{
    public class BinarySelector : BehaviorComponent
    {
        private BehaviorComponent condition;
        private BehaviorComponent ifTrue;
        private BehaviorComponent ifFalse;

        public BinarySelector(BehaviorComponent condition, BehaviorComponent ifTrue, BehaviorComponent ifFalse)
        {
            this.condition = condition;
            this.ifTrue = ifTrue;
            this.ifFalse = ifFalse;
        }

        public override BehaviorReturnCode OnBehave(TreeContext context)
        {
            var result = this.condition.Behave(context);
            if (result == BehaviorReturnCode.Success)
            {
                return this.ifTrue.Behave(context);
            }
            else if (result == BehaviorReturnCode.Failure)
            {
                return this.ifFalse.Behave(context);
            }

            return BehaviorReturnCode.Running;
        }
    }
}
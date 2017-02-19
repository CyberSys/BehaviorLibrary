namespace BehaviorLibrary.Components.Decorators
{
    public class Failer : BehaviorComponent
    {
        private BehaviorComponent behavior;

        /// <summary>
        /// Returns a failure even when the decorated component succeeded or is running.
        /// </summary>
        /// <param name="behavior">behavior to run</param>
        public Failer(BehaviorComponent behavior)
        {
            this.behavior = behavior;
        }

        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        public override BehaviorReturnCode OnBehave(TreeContext context)
        {
            this.ReturnCode = behavior.Behave(context);
            if (this.ReturnCode == BehaviorReturnCode.Success || this.ReturnCode == BehaviorReturnCode.Running)
            {
                this.ReturnCode = BehaviorReturnCode.Failure;
            }

            return this.ReturnCode;
        }
    }
}

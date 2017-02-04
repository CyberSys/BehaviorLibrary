using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BehaviorLibrary.Components.Decorators
{
    public class Awaiter : BehaviorComponent
    {
        private Func<bool> condition;
        private BehaviorComponent behavior;

        /// <summary>
        /// executes the behavior after a condition is true.
        /// </summary>
        /// <param name="conditon">the condition</param>
        /// <param name="behavior">behavior to run</param>
        public Awaiter(Func<bool> conditon, BehaviorComponent behavior)
        {
            this.condition = conditon;
            this.behavior = behavior;
        }

        /// <summary>
        /// performs the given behavior
        /// </summary>
        /// <returns>the behaviors return code</returns>
        public override BehaviorReturnCode OnBehave(TreeContext context)
        {
            try
            {
                if (this.condition.Invoke())
                {
                    return this.behavior.Behave(context);
                }
                else
                {
                    return BehaviorReturnCode.Running;
                }
            }
            catch (Exception e)
            {
#if DEBUG
                Console.Error.WriteLine(e.ToString());
#endif
                return BehaviorReturnCode.Failure;
            }
        }
    }
}

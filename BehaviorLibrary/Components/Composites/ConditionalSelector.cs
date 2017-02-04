using System;

namespace BehaviorLibrary.Components.Composites
{
    public class ConditionalSelector : BehaviorComponent
    {
        protected BehaviorComponent[] conditions;
        private BehaviorComponent action;

        public ConditionalSelector(BehaviorComponent condition, BehaviorComponent action)
        {
            this.conditions = new BehaviorComponent[] { condition };
            this.action = action;
        }

        public ConditionalSelector(BehaviorComponent[] conditions, BehaviorComponent action)
        {
            this.conditions = conditions;
            this.action = action;
        }

        public override BehaviorReturnCode OnBehave(TreeContext context)
        {
            for (int i = 0; i < this.conditions.Length; i++)
            {
                try
                {
                    switch (this.conditions[i].Behave(context))
                    {
                        case BehaviorReturnCode.Failure:
                            return BehaviorReturnCode.Failure;
                        case BehaviorReturnCode.Running:
                            return BehaviorReturnCode.Running;
                        default:
                            continue;
                    }
                }
                catch (Exception e)
                {
#if DEBUG
                    Console.Error.WriteLine(e.ToString());
#endif
                    continue;
                }
            }

            return this.action.Behave(context);
        }
    }
}
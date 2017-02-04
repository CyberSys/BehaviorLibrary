using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BehaviorLibrary.Components.Actions
{
    public class BehaviorAction : BehaviorComponent
    {

        private Func<BehaviorReturnCode> _Action;
        private Func<BehaviorComponent, TreeContext, BehaviorReturnCode> _Action2;

        public BehaviorAction() { }

        public BehaviorAction(Func<BehaviorReturnCode> action)
        {
            _Action = action;
        }

        public BehaviorAction(Func<BehaviorComponent, TreeContext, BehaviorReturnCode> action)
        {
            _Action2 = action;
        }

        public override BehaviorReturnCode OnBehave(TreeContext context)
        {
            try
            {
                if (this._Action != null)
                {
                    switch (_Action.Invoke())
                    {
                        case BehaviorReturnCode.Success:
                            ReturnCode = BehaviorReturnCode.Success;
                            return ReturnCode;
                        case BehaviorReturnCode.Failure:
                            ReturnCode = BehaviorReturnCode.Failure;
                            return ReturnCode;
                        case BehaviorReturnCode.Running:
                            ReturnCode = BehaviorReturnCode.Running;
                            return ReturnCode;
                        default:
                            ReturnCode = BehaviorReturnCode.Failure;
                            return ReturnCode;
                    }
                }
                else
                {
                    return _Action2.Invoke(this, context);
                }
            }
            catch (Exception e)
            {
#if DEBUG
                Console.Error.WriteLine(e.ToString());
#endif
                ReturnCode = BehaviorReturnCode.Failure;
                return ReturnCode;
            }
        }

    }
}

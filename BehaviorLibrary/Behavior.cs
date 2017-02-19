using System;
using System.Collections.Generic;
using System.Linq;
using BehaviorLibrary.Components;
using BehaviorLibrary.Components.Composites;

namespace BehaviorLibrary
{
    public enum BehaviorReturnCode
    {
        Failure,
        Success,
        Running
    }

    public delegate BehaviorReturnCode BehaviorReturn();

    /// <summary>
    /// 
    /// </summary>
    public class Behavior
    {

		private BehaviorComponent _Root;

        private BehaviorReturnCode _ReturnCode;

        public BehaviorReturnCode ReturnCode
        {
            get { return _ReturnCode; }
            set { _ReturnCode = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="root"></param>
        public Behavior(IndexSelector root)
        {
            _Root = root;
        }

		public Behavior(BehaviorComponent root){
			_Root = root;
		}

        /// <summary>
        /// perform the behavior
        /// </summary>
        public BehaviorReturnCode Behave(TreeContext context)
        {
            try
            {
                switch (_Root.Behave(context))
                {
                    case BehaviorReturnCode.Failure:
                        ReturnCode = BehaviorReturnCode.Failure;
                        break;
                    case BehaviorReturnCode.Success:
                        ReturnCode = BehaviorReturnCode.Success;
                        break;
                    case BehaviorReturnCode.Running:
                        ReturnCode = BehaviorReturnCode.Running;
                        break;
                    default:
                        ReturnCode = BehaviorReturnCode.Running;
                        break;
                }
            }
            catch (Exception e)
            {
#if DEBUG
                Console.Error.WriteLine(e.ToString());
#endif
                ReturnCode = BehaviorReturnCode.Failure;
            }

            return this.ReturnCode;
        }
    }
}

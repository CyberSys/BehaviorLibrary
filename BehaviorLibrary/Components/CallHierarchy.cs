using System.Collections.Generic;
using System.Linq;

namespace BehaviorLibrary.Components
{
    public class CallHierarchy
    {
        private IEnumerable<BehaviorComponent> callList;

        public CallHierarchy(IEnumerable<BehaviorComponent> callList)
        {
            this.callList = callList;
        }

        public bool Matches(CallHierarchy other)
        {
            var array = callList.ToArray();
            var otherArray = other.callList.ToArray();
            if (array.Length > otherArray.Length) return false; // The current list can be shorter than the old one, because we can still be processing.

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] != otherArray[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
using System;

namespace TestUssd.Core
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class StateConfigAttribute : Attribute
    {
        public string Name;
        public StateConfigAttribute(string className)
        {
            Name = className;
        }
    }
}

using System;

namespace IKriv.IsItMySource.Interfaces
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public class DebugInfoEngineAttribute : Attribute
    {
        public Type Type { get; }

        public DebugInfoEngineAttribute(Type t)
        {
            Type = t;
        }
    }
}

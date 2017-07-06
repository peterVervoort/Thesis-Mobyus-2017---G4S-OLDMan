using System;

namespace G4S.Entities.Helpers
{
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class HistoryAttribute : Attribute 
    {
        private readonly Type _historyType;

        public HistoryAttribute(Type historyType)
        {
            _historyType = historyType;
        }

        public Type HistoryType
        {
            get { return _historyType; }
        }
    }
}

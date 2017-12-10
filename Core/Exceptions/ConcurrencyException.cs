using System;

namespace Bitcoin.Core.Exceptions
{
    public class ConcurrencyException : Exception
    {
        public object ConflictedObject { get; set; }
        public ConcurrencyException(object conflictedObject)
        {
            this.ConflictedObject = conflictedObject;
        }
    }
}
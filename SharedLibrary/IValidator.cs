using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary
{
    public interface IValidator
    {
        IEnumerable<ValidationNotification> ValidationNotifications { get; }

        bool IsValid { get; }

        void Validate();
    }
}

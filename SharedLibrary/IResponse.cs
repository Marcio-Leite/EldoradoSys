using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary
{
    public interface IResponse
    {
        public IEnumerable<ValidationNotification> ValidationNotifications { get; }
        public int StatusCode { get; }
    }
}

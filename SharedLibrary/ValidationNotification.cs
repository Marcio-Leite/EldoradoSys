using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SharedLibrary
{
    public class ValidationNotification
    {
        public ValidationNotification(string field, string message)
        {
            Field = field;
            Message = message;
        }

        [DataMember(Name = "field")]
        public string Field { get; private set; }
        [DataMember(Name = "message")]
        public string Message { get; private set; }

        public bool Equals(ValidationNotification other)
        {
            var thisString = JsonConvert.SerializeObject(this);
            var otherString = JsonConvert.SerializeObject(other);
            return String.Equals(thisString, otherString);
        }
    }
}

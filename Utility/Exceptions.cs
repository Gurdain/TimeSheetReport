using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Utility
{
    public class Exceptions
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string Message { get; set; }
        public string Url { get; set; }
        public string Method { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime modifiedOn { get; set; }
        public DateTime deletedOn { get; set; }
        public bool isDeleted { get; set; }
    }
}
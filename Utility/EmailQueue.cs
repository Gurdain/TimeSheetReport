using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Utility
{
    public class EmailQueue
    {
        public int Id { get; set; }
        public string ToAddress { get; set; }
        public string FromAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public int Tries { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime modifiedOn { get; set; }
        public DateTime deletedOn { get; set; }
        public bool isDeleted { get; set; }
    }
}
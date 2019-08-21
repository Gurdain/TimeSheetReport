using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeSheetReport.Models
{
    public class Attachment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Path { get; set; }
        public int TaskId { get; set; }
        public int UserId { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime modifiedOn { get; set; }
        public DateTime deletedOn { get; set; }
        public bool isDeleted { get; set; }
    }
}
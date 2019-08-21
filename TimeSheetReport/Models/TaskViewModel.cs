using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeSheetReport.Models
{
    public class TaskViewModel
    {
        public List<Users> users { get; set; }
        public List<Status> statuses { get; set; }
        public List<Task> tasks { get; set; }
        public List<Attachment> attachments { get; set; }
        public Task task { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeSheetReport.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime SubmitBy { get; set; }
        public int TraineeId { get; set; }
        public int TrainerId { get; set; }
        public int StatusId { get; set; }
        public bool Extension { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime modifiedOn { get; set; }
        public DateTime deletedOn { get; set; }
        public bool isDeleted { get; set; }
    }
}
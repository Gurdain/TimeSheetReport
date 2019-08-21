﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimeSheetReport.Models
{
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime modifiedOn { get; set; }
        public DateTime deletedOn { get; set; }
        public bool isDeleted { get; set; }
    }
}
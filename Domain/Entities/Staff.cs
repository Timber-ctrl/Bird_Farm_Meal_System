﻿using System;
using System.Collections.Generic;

namespace Domain.Entities
{
    public partial class Staff
    {
        public Staff()
        {
            AssignStaffs = new HashSet<AssignStaff>();
            FoodReports = new HashSet<FoodReport>();
            TaskCheckLists = new HashSet<TaskCheckList>();
            TicketAssignees = new HashSet<Ticket>();
            TicketCreators = new HashSet<Ticket>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? AvatarUrl { get; set; }
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string Password { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string? DeviceToken { get; set; }
        public Guid FarmId { get; set; }
        public DateTime CreateAt { get; set; }

        public virtual Farm Farm { get; set; } = null!;
        public virtual ICollection<AssignStaff> AssignStaffs { get; set; }
        public virtual ICollection<FoodReport> FoodReports { get; set; }
        public virtual ICollection<TaskCheckList> TaskCheckLists { get; set; }
        public virtual ICollection<Ticket> TicketAssignees { get; set; }
        public virtual ICollection<Ticket> TicketCreators { get; set; }
    }
}

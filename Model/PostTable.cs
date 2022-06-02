using System;

#nullable disable

namespace Model
{
    public partial class PostTable
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateDateTime { get; set; } = DateTime.Now;
        public DateTime LastUpdate { get; set; } 
        public string Image { get; set; }
        public bool Status { get; set; }=true;

        public virtual UserTable User { get; set; }
    }
}
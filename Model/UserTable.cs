using System.Collections.Generic;

#nullable disable

namespace Model
{
    public partial class UserTable
    {
        public UserTable()
        {
            PostTables = new HashSet<PostTable>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }

        public virtual ICollection<PostTable> PostTables { get; set; }
    }
}
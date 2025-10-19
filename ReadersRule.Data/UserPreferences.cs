using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadersRule.Data
{
    public class UserPreferences
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Author { get; set; }
        public string Tags { get; set; }
    }
}

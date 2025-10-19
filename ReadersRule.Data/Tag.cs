using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadersRule.Data
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class FilledTags : Tag
    {
        public List<Book> Books { get; set; }
    }
}

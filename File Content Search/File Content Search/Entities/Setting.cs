using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Content_Search.Entities
{
    public class Setting
    {
        public Guid SettingId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}

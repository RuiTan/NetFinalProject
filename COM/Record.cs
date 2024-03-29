//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace NetFinalProject.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    [ComVisible(true)]
    [Guid("6B26A21C-0359-49B4-B692-6360B09E6CB5")]
    public partial class Record
    {
        public int id { get; set; }
        public Nullable<int> user_id { get; set; }
        public Nullable<int> movie_id { get; set; }
        public Nullable<System.DateTime> time { get; set; }
        public string content { get; set; }

        [JsonIgnore]
        public virtual Movie Movie { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
    }
}

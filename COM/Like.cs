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
    using COM;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    [ComVisible(true)]
    [Guid("733D56E9-1299-4B27-8430-CBF40807593D")]
    public partial class Like : IModel
    {
        public int user_id { get; set; }
        public Nullable<int> comment_id { get; set; }

        [JsonIgnore]
        public virtual Comment Comment { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
    }
}

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
    [Guid("C57C09DF-B16D-47A8-9CB4-5D9FB8F19D3D")]
    public partial class Feedback
    {
        public int id { get; set; }
        public int comment_id { get; set; }
        public int user_id { get; set; }
        public string content { get; set; }
        public Nullable<System.DateTime> time { get; set; }
        public Nullable<int> at_id { get; set; }
        public int reply_id { get; set; }

        [JsonIgnore]
        public virtual Comment Comment { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
    }
}
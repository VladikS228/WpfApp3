//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp3.ApplicationData
{
    using System;
    using System.Collections.Generic;
    
    public partial class postsID
    {
        public int postID { get; set; }
        public Nullable<System.DateTime> dateCreated { get; set; }
        public Nullable<int> usersID { get; set; }
        public string title { get; set; }
        public string body { get; set; }
    
        public virtual users users { get; set; }
    }
}

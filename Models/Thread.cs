//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gas_Go_v1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Thread
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Thread()
        {
            this.Post = new HashSet<Post>();
        }
    
        public int ThreadId { get; set; }
        public string Subject { get; set; }
        public System.DateTime CreatedTime { get; set; }
        public string UserID { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Post> Post { get; set; }
    }
}

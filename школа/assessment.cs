//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace школа
{
    using System;
    using System.Collections.Generic;
    
    public partial class assessment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public assessment()
        {
            this.assessmentForStudent = new HashSet<assessmentForStudent>();
        }
    
        public int id { get; set; }
        public int Mark { get; set; }
        public string subject { get; set; }
        public System.DateTime date { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<assessmentForStudent> assessmentForStudent { get; set; }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace krygki
{
    using System;
    using System.Collections.Generic;
    
    public partial class StudentTable
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StudentTable()
        {
            this.Visiting = new HashSet<Visiting>();
        }
    
        public int Id { get; set; }
        public int Id_student { get; set; }
        public int Id_timetable { get; set; }
    
        public virtual Student Student { get; set; }
        public virtual Timetable Timetable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Visiting> Visiting { get; set; }
    }
}

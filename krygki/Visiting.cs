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
    
    public partial class Visiting
    {
        public int Id { get; set; }
        public int Id_history { get; set; }
        public int Id_student { get; set; }
        public int Statusof { get; set; }
    
        public virtual History History { get; set; }
        public virtual Student Student { get; set; }
    }
}
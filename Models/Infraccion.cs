//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AgenciaTransito.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Infraccion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Infraccion()
        {
            this.FotoInfraccions = new HashSet<FotoInfraccion>();
        }
    
        public int idFotoMulta { get; set; }
        public string PlacaVehiculo { get; set; }
        public System.DateTime FechaInfraccion { get; set; }
        public string TipoInfraccion { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FotoInfraccion> FotoInfraccions { get; set; }
        public virtual Vehiculo Vehiculo { get; set; }
    }
}

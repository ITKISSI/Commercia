//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Projet_login_1
{
    using System;
    using System.Collections.Generic;
    
    public partial class commande_vente
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public commande_vente()
        {
            this.ligne_commande_vente = new HashSet<ligne_commande_vente>();
        }
    
        public short Num_C_V { get; set; }
        public Nullable<System.DateTime> Date_C { get; set; }
        public Nullable<short> Client { get; set; }
    
        public virtual client client1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ligne_commande_vente> ligne_commande_vente { get; set; }
    }
}
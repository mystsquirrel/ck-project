//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ck_project
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class lead
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public lead()
        {
            this.building_permit = new HashSet<building_permit>();
            this.installations = new HashSet<installation>();
            this.lead_log_file = new HashSet<lead_log_file>();
            this.products = new HashSet<product>();
            this.taxes_leads = new HashSet<taxes_leads>();
            this.total_cost = new HashSet<total_cost>();
        }
    
        public int lead_number { get; set; }
        public int customer_number { get; set; }
        public int class_number { get; set; }
        public int project_status_number { get; set; }
        public int project_type_number { get; set; }
        public int source_number { get; set; }
        public int address_number { get; set; }
        public int emp_number { get; set; }
        public int branch_number { get; set; }
        public int delivery_status_number { get; set; }
        public bool in_city { get; set; }
        public System.DateTime lead_date { get; set; }
        [Required(ErrorMessage = "The project name is required")]
        [MinLength(2, ErrorMessage = "The project name is too short")]
        [MaxLength(49, ErrorMessage = "The project name must be less than 49 characters")]
        public string project_name { get; set; }
        public bool tax_exempt { get; set; }
        public bool deleted { get; set; }
        public System.DateTime Last_update_date { get; set; }
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string phone_number { get; set; }
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string second_phone_number { get; set; }

        public string email { get; set; }
    
        public virtual address address { get; set; }
        public virtual branch branch { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<building_permit> building_permit { get; set; }
        public virtual customer customer { get; set; }
        public virtual delivery_status delivery_status { get; set; }
        public virtual employee employee { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<installation> installations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<lead_log_file> lead_log_file { get; set; }
        public virtual lead_source lead_source { get; set; }
        public virtual project_class project_class { get; set; }
        public virtual project_status project_status { get; set; }
        public virtual project_type project_type { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<product> products { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<taxes_leads> taxes_leads { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<total_cost> total_cost { get; set; }
    }
}

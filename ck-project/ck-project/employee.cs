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

    public partial class employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public employee()
        {
            this.leads = new HashSet<lead>();
        }

        public int emp_number { get; set; }

        [Required(ErrorMessage = "The first name is required")]
        [MinLength(2, ErrorMessage = "The first name is too short")]
        [MaxLength(19, ErrorMessage = "The first name must be less than 20 characters")]
        public string emp_firstname { get; set; }
        [MinLength(2, ErrorMessage = "The first name is too short")]
        [MaxLength(19, ErrorMessage = "The first name must be less than 20 characters")]
        public string emp_middlename { get; set; }
        [Required(ErrorMessage = "The last name is required")]
        [MinLength(2, ErrorMessage = "The last name is too short")]
        [MaxLength(19, ErrorMessage = "The last name must be less than 20 characters")]
        public string emp_lastname { get; set; }
        [Required(ErrorMessage = "The username is required")]
        [MinLength(2, ErrorMessage = "The username is too short")]
        [MaxLength(19, ErrorMessage = "The username must be less than 20 characters")]
        public string emp_username { get; set; }
        [Required(ErrorMessage = "The username is required")]
        [DataType(DataType.Password)]
        public string emp_password { get; set; }
        [Required(ErrorMessage = "Your must provide a PhoneNumber")]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]

        public string phone_number { get; set; }
        public int user_type_number { get; set; }
        public int branch_number { get; set; }
        public bool deleted { get; set; }

        public virtual branch branch { get; set; }
        public virtual users_types users_types { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<lead> leads { get; set; }
    }
}

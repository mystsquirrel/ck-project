using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ck_project.Controllers
{

    public class LeadController : Controller
    {



        //Creating the DB connecton
        ckdatabase db = new ckdatabase();



 

        // GET: Lead
        public ActionResult Index()
        {
            return View();
        }

        // GET: Leads
        //      public ActionResult ListLead()
        //{

        //         List<lead> Leads_list = db.leads.ToList();
        //        ViewBag.Leadslist = Leads_list;
        //        return View();
        //   }


        public ActionResult ListLead(string search)
        {
            return View(db.leads.Where(x => x.project_name.StartsWith(search) || search == null && x.deleted == false).ToList());
        }

        public ActionResult Details(int id)
        {
            return View(db.leads.Where(x => x.lead_number == id).ToList());
        }

        
        public ActionResult AddLead(int id)
        {
            List<SelectListItem> CustomerInfo = new List<SelectListItem>();
            CustomerInfo.AddRange(db.customers.Select(a => new SelectListItem
            {
                Text = a.customer_lastname,
                Selected = false,
                Value = a.customer_number.ToString()
            }));

            var ClassInfo = new List<SelectListItem> { new SelectListItem() };
            ClassInfo.AddRange(db.project_class.Select(b => new SelectListItem
            {
                Text = b.class_name,
                Selected = false,
                Value = b.class_number.ToString()
            }));

            var StatusInfo = new List<SelectListItem> {
                new SelectListItem()
            };
            StatusInfo.AddRange(db.project_status.Select(c => new SelectListItem
            {
                Text = c.project_status_name,
                Selected = false,
                Value = c.project_status_number.ToString()
            }));

            var ProjectTypeInfo = new List<SelectListItem> {
                new SelectListItem()
            };
            ProjectTypeInfo.AddRange(db.project_type.Select(b => new SelectListItem
            {
                Text = b.project_type_name,
                Selected = false,
                Value = b.project_type_number.ToString()
            }));

            var SourceInfo = new List<SelectListItem> {
                new SelectListItem()
            };
            SourceInfo.AddRange(db.lead_source.Select(b => new SelectListItem
            {
                Text = b.source_name,
                Selected = false,
                Value = b.source_number.ToString()
            }));

            var AddressInfo = new List<SelectListItem> {
                new SelectListItem()
            };
            AddressInfo.AddRange(db.addresses.Select(a => new SelectListItem
            {
                Text = a.address_type,
                Selected = false,
                Value = a.address_number.ToString()
            }));

            var AddressCityInfo = new List<SelectListItem> {
                new SelectListItem()
            };
     
            var EmpInfo = new List<SelectListItem> {
                new SelectListItem()
            };
            EmpInfo.AddRange(db.employees.Select(a => new SelectListItem
            {
                Text = a.emp_lastname,
                Selected = false,
                Value = a.emp_number.ToString()
            }));

            var BranchInfo = new List<SelectListItem> {
                new SelectListItem()
            };
            BranchInfo.AddRange(db.branches.Select(a => new SelectListItem
            {
                Text = a.branch_name,
                Selected = false,
                Value = a.branch_number.ToString()
            }));

            var DeliveryStatusInfo = new List<SelectListItem> {
                new SelectListItem()
            };
            DeliveryStatusInfo.AddRange(db.delivery_status.Select(a => new SelectListItem
            {
                Text = a.delivery_status_name,
                Selected = false,
                Value = a.delivery_status_number.ToString()
            }));


            //setting variable passing
            ViewBag.Customer_Info = CustomerInfo;
            ViewBag.Class_Info = ClassInfo;
            ViewBag.Status_Info = StatusInfo;
            ViewBag.ProjectType_Info = ProjectTypeInfo;
            ViewBag.Source_Info = SourceInfo;
            ViewBag.Address_Info = AddressInfo;
            ViewBag.Emp_Info = EmpInfo;
            ViewBag.Branch_Info = BranchInfo;
            ViewBag.DeliveryStatus_Info = DeliveryStatusInfo;


            return View();

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddLead(FormCollection form, int id)
        {
            //setting dropdown list for forgern key
            List<SelectListItem> CustomerInfo = new List<SelectListItem>();
            CustomerInfo.AddRange(db.customers.Select(a => new SelectListItem
            {
                Text = a.customer_lastname,
                Selected = false,
                Value = a.customer_number.ToString()
            }));

            var ClassInfo = new List<SelectListItem> ();
            ClassInfo.AddRange(db.project_class.Select(b => new SelectListItem
            {
                Text = b.class_name,
                Selected = false,
                Value = b.class_number.ToString()
            }));

            var StatusInfo = new List<SelectListItem> ();
            StatusInfo.AddRange(db.project_status.Select(c => new SelectListItem
            {
                Text = c.project_status_name,
                Selected = false,
                Value = c.project_status_number.ToString()
            }));

            var ProjectTypeInfo = new List<SelectListItem> {
                new SelectListItem()
            };
            ProjectTypeInfo.AddRange(db.project_type.Select(b => new SelectListItem
            {
                Text = b.project_type_name,
                Selected = false,
                Value = b.project_type_number.ToString()
            }));

            var SourceInfo = new List<SelectListItem> ();
            SourceInfo.AddRange(db.lead_source.Select(b => new SelectListItem
            {
                Text = b.source_name,
                Selected = false,
                Value = b.source_number.ToString()
            }));

            var AddressInfo = new List<SelectListItem> {
                new SelectListItem()
            };
            AddressInfo.AddRange(db.addresses.Select(a => new SelectListItem
            {
                Text = a.address_type,
                Selected = false,
                Value = a.address_number.ToString()
            }));

            var EmpInfo = new List<SelectListItem> {
                new SelectListItem()
            };
            EmpInfo.AddRange(db.employees.Select(a => new SelectListItem
            {
                Text = a.emp_lastname,
                Selected = false,
                Value = a.emp_number.ToString()
            }));

            var BranchInfo = new List<SelectListItem>();
            BranchInfo.AddRange(db.branches.Select(a => new SelectListItem
            {
                Text = a.branch_name,
                Selected = false,
                Value = a.branch_number.ToString()
            }));

            var DeliveryStatusInfo = new List<SelectListItem> {
                new SelectListItem()
            };
            DeliveryStatusInfo.AddRange(db.delivery_status.Select(a => new SelectListItem
            {
                Text = a.delivery_status_name,
                Selected = false,
                Value = a.delivery_status_number.ToString()
            }));


            //setting variable passing
            ViewBag.Customer_Info = CustomerInfo;
            ViewBag.Class_Info = ClassInfo;
            ViewBag.Status_Info = StatusInfo;
            ViewBag.ProjectType_Info = ProjectTypeInfo;
            ViewBag.Source_Info = SourceInfo;
            ViewBag.Address_Info = AddressInfo;
            ViewBag.Emp_Info = EmpInfo;
            ViewBag.Branch_Info = BranchInfo;
            ViewBag.DeliveryStatus_Info = DeliveryStatusInfo;

            //create instance
            lead target = new lead();
            target.customer = db.customers.Where(a => a.customer_number == id).FirstOrDefault();
            //get property
            TryUpdateModel(target, new string[] {  "class_number",  "project_type_number", "source_number", "address_number", "emp_number", "branch_number", "delivery_status_number", "in_city",  "project_name", "tax_exempt"}, form.ToValueProvider());
            target.deleted = false;
            target.project_status_number = 3;
            target.lead_date = System.DateTime.Now;
            target.Last_update_date = System.DateTime.Now;
           


            //validate
            //      if (string.IsNullOrEmpty(target.emp_firstname))
            //     ModelState.AddModelError("firstname", "firstname is required");

            //     if (ModelState.IsValid)
            //    {
            db.leads.Add(target);
            db.SaveChanges();
            //    }
            ViewBag.m = " The lead was successfully created and saved to customer "+ target.customer.customer_firstname + " " + target.customer.customer_lastname + " on " + System.DateTime.Now;
            return View(target);
        }

        // read from the DB
        public ActionResult Edit(int id)
        {

            //setting dropdown list for forgern key
            List<SelectListItem> CustomerInfo = new List<SelectListItem>();
            CustomerInfo.AddRange(db.customers.Select(a => new SelectListItem
            {
                Text = a.customer_lastname,
                Selected = false,
                Value = a.customer_number.ToString()
            }));

            var ClassInfo = new List<SelectListItem> {
                new SelectListItem()
            };
            ClassInfo.AddRange(db.project_class.Select(b => new SelectListItem
            {
                Text = b.class_name,
                Selected = false,
                Value = b.class_number.ToString()
            }));

            var StatusInfo = new List<SelectListItem> {
                new SelectListItem()
            };
            StatusInfo.AddRange(db.project_status.Select(c => new SelectListItem
            {
                Text = c.project_status_name,
                Selected = false,
                Value = c.project_status_number.ToString()
            }));

            var ProjectTypeInfo = new List<SelectListItem> {
                new SelectListItem()
            };
            ProjectTypeInfo.AddRange(db.project_type.Select(b => new SelectListItem
            {
                Text = b.project_type_name,
                Selected = false,
                Value = b.project_type_number.ToString()
            }));

            var SourceInfo = new List<SelectListItem> {
                new SelectListItem()
            };
            SourceInfo.AddRange(db.lead_source.Select(b => new SelectListItem
            {
                Text = b.source_name,
                Selected = false,
                Value = b.source_number.ToString()
            }));

            var AddressInfo = new List<SelectListItem> {
                new SelectListItem()
            };
            AddressInfo.AddRange(db.addresses.Select(a => new SelectListItem
            {
                Text = a.address_type,
                Selected = false,
                Value = a.address_number.ToString()
            }));

            var EmpInfo = new List<SelectListItem> {
                new SelectListItem()
            };
            EmpInfo.AddRange(db.employees.Select(a => new SelectListItem
            {
                Text = a.emp_lastname,
                Selected = false,
                Value = a.emp_number.ToString()
            }));

            var BranchInfo = new List<SelectListItem> {
                new SelectListItem()
            };
            BranchInfo.AddRange(db.branches.Select(a => new SelectListItem
            {
                Text = a.branch_name,
                Selected = false,
                Value = a.branch_number.ToString()
            }));

            var DeliveryStatusInfo = new List<SelectListItem> {
                new SelectListItem()
            };
            DeliveryStatusInfo.AddRange(db.delivery_status.Select(a => new SelectListItem
            {
                Text = a.delivery_status_name,
                Selected = false,
                Value = a.delivery_status_number.ToString()
            }));


            //setting variable passing
            ViewBag.Customer_Info = CustomerInfo;
            ViewBag.Class_Info = ClassInfo;
            ViewBag.Status_Info = StatusInfo;
            ViewBag.ProjectType_Info = ProjectTypeInfo;
            ViewBag.Source_Info = SourceInfo;
            ViewBag.Address_Info = AddressInfo;
            ViewBag.Emp_Info = EmpInfo;
            ViewBag.Branch_Info = BranchInfo;
            ViewBag.DeliveryStatus_Info = DeliveryStatusInfo;



            List<lead> Leads_list = db.leads.Where(d => d.lead_number == id).ToList();
            ViewBag.Customerslist = Leads_list;
            lead target = Leads_list[0];
            return View(target);
        }

        // Write to the DB that is why we use POST
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int id, FormCollection form)
        {


            //setting dropdown list for forgern key
            List<SelectListItem> CustomerInfo = new List<SelectListItem>();
            CustomerInfo.AddRange(db.customers.Select(a => new SelectListItem
            {
                Text = a.customer_lastname,
                Selected = false,
                Value = a.customer_number.ToString()
            }));

            var ClassInfo = new List<SelectListItem> {
                new SelectListItem()
            };
            ClassInfo.AddRange(db.project_class.Select(b => new SelectListItem
            {
                Text = b.class_name,
                Selected = false,
                Value = b.class_number.ToString()
            }));

            var StatusInfo = new List<SelectListItem> {
                new SelectListItem()
            };
            StatusInfo.AddRange(db.project_status.Select(c => new SelectListItem
            {
                Text = c.project_status_name,
                Selected = false,
                Value = c.project_status_number.ToString()
            }));

            var ProjectTypeInfo = new List<SelectListItem> {
                new SelectListItem()
            };
            ProjectTypeInfo.AddRange(db.project_type.Select(b => new SelectListItem
            {
                Text = b.project_type_name,
                Selected = false,
                Value = b.project_type_number.ToString()
            }));

            var SourceInfo = new List<SelectListItem> {
                new SelectListItem()
            };
            SourceInfo.AddRange(db.lead_source.Select(b => new SelectListItem
            {
                Text = b.source_name,
                Selected = false,
                Value = b.source_number.ToString()
            }));

            var AddressInfo = new List<SelectListItem> {
                new SelectListItem()
            };
            AddressInfo.AddRange(db.addresses.Select(a => new SelectListItem
            {
                Text = a.address_type,
                Selected = false,
                Value = a.address_number.ToString()
            }));

            var EmpInfo = new List<SelectListItem> {
                new SelectListItem()
            };
            EmpInfo.AddRange(db.employees.Select(a => new SelectListItem
            {
                Text = a.emp_lastname,
                Selected = false,
                Value = a.emp_number.ToString()
            }));

            var BranchInfo = new List<SelectListItem> {
                new SelectListItem()
            };
            BranchInfo.AddRange(db.branches.Select(a => new SelectListItem
            {
                Text = a.branch_name,
                Selected = false,
                Value = a.branch_number.ToString()
            }));

            var DeliveryStatusInfo = new List<SelectListItem> {
                new SelectListItem()
            };
            DeliveryStatusInfo.AddRange(db.delivery_status.Select(a => new SelectListItem
            {
                Text = a.delivery_status_name,
                Selected = false,
                Value = a.delivery_status_number.ToString()
            }));


            //setting variable passing
            ViewBag.Customer_Info = CustomerInfo;
            ViewBag.Class_Info = ClassInfo;
            ViewBag.Status_Info = StatusInfo;
            ViewBag.ProjectType_Info = ProjectTypeInfo;
            ViewBag.Source_Info = SourceInfo;
            ViewBag.Address_Info = AddressInfo;
            ViewBag.Emp_Info = EmpInfo;
            ViewBag.Branch_Info = BranchInfo;
            ViewBag.DeliveryStatus_Info = DeliveryStatusInfo;



            List<lead> Leads_list = db.leads.Where(d => d.lead_number == id).ToList();
            ViewBag.Customerslist = Leads_list;
            lead target = Leads_list[0];

            TryUpdateModel(target, new string[] { "customer_number", "class_number", "project_status_number", "project_type_number", "address_number", "emp_number", "branch_number", "delivery_status_number", "in_city", "lead_date", "closing_date", "last_status_date", "project_name", "tax_exempt", "Last_update_date" }, form.ToValueProvider());
            db.SaveChanges();
            return View(target);
        }

    }
}
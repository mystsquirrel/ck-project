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

        //     GET: Leads
        //      public ActionResult ListLead()
        //{

        //         List<lead> Leads_list = db.leads.ToList();
        //        ViewBag.Leadslist = Leads_list;
        //        return View();
        //   }



        public ActionResult ListLead(string search=null,int type=-1,string da= "2017/01/01 11:11:11")
        {
            var ClassInfo = new List<SelectListItem> { new SelectListItem() };
            ClassInfo.AddRange(db.project_status.Select(b => new SelectListItem
            {
                Text = b.project_status_name,
                Selected = false,
                Value = b.project_status_number.ToString()
            }));
            ViewBag.lead_type = ClassInfo;

            return View(db.leads.Where(x => x.project_name.StartsWith(search) && x.project_status_number==type || search == null && x.deleted == false).ToList());
        }

        //public ActionResult ListLead(string search)
        //{
        //    //return View(db.leads.Where(x => x.lead_date.Date >= search ).ToList());
        //}

        //public ActionResult ListLead2(DateTime search)
        //{
        //    return View(db.leads.Where(x => x.lead_date.Date. >= search).ToList());
        //}

        //public ActionResult ListLead(string search, string Date)
        //{
        //    DateTime dt = DateTime.Parse(Date);


        //    return View(db.leads.Where(b => b.lead_date.Equals(dt) || Date == null && b.deleted == false).ToList());

        //  //  return View(db.leads.Where(x => x.lead_date.Equals(Date) || Date == null && x.deleted == false).ToList());
        //}

        public ActionResult Details(int id)
        {
            return View(db.leads.Where(x => x.lead_number == id).ToList());
        }


        public ActionResult AddLeadXX(int id)
        {
            List<SelectListItem> CustomerInfo = new List<SelectListItem>();
            CustomerInfo.AddRange(db.customers.Select(a => new SelectListItem
            {
                Text = a.customer_lastname,
                Selected = false,
                Value = a.customer_number.ToString()
            }));

            var ClassInfo = new List<SelectListItem>();
            ClassInfo.AddRange(db.project_class.Select(b => new SelectListItem
            {
                Text = b.class_name,
                Selected = false,
                Value = b.class_number.ToString()
            }));

            var StatusInfo = new List<SelectListItem>();
            StatusInfo.AddRange(db.project_status.Select(c => new SelectListItem
            {
                Text = c.project_status_name,
                Selected = false,
                Value = c.project_status_number.ToString()
            }));

            var ProjectTypeInfo = new List<SelectListItem>();
            ProjectTypeInfo.AddRange(db.project_type.Select(b => new SelectListItem
            {
                Text = b.project_type_name,
                Selected = false,
                Value = b.project_type_number.ToString()
            }));

            var SourceInfo = new List<SelectListItem>();
            SourceInfo.AddRange(db.lead_source.Select(b => new SelectListItem
            {
                Text = b.source_name,
                Selected = false,
                Value = b.source_number.ToString()
            }));

            var AddressInfo = new List<SelectListItem> ();
            AddressInfo.AddRange(db.addresses.Select(a => new SelectListItem
            {
                Text = a.address_type,
                Selected = false,
                Value = a.address_number.ToString()
            }));

            var AddressCityInfo = new List<SelectListItem>();

            var EmpInfo = new List<SelectListItem> ();
            EmpInfo.AddRange(db.employees.Select(a => new SelectListItem
            {
                Text = a.emp_lastname,
                Selected = false,
                Value = a.emp_number.ToString()
            }));

            var BranchInfo = new List<SelectListItem> ();
            BranchInfo.AddRange(db.branches.Select(a => new SelectListItem
            {
                Text = a.branch_name,
                Selected = false,
                Value = a.branch_number.ToString()
            }));

            var DeliveryStatusInfo = new List<SelectListItem>();
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

            var Sstate = new List<SelectListItem> {
                new SelectListItem{ Text="WV",Selected=false,Value="WV"},
                 new SelectListItem{ Text="IN",Selected=false,Value="IN"},
                 new SelectListItem{ Text="NY",Selected=false,Value="NY"},
                 new SelectListItem{ Text="KY",Selected=false,Value="KY"}

            };
            ViewBag.Sstate = Sstate;
            return View();
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AddLeadXX(FormCollection form, int id)
        {//setting dropdown list for forgern key
            List<SelectListItem> CustomerInfo = new List<SelectListItem>();
            CustomerInfo.AddRange(db.customers.Select(a => new SelectListItem
            {
                Text = a.customer_lastname,
                Selected = false,
                Value = a.customer_number.ToString()
            }));

            var ClassInfo = new List<SelectListItem>();
            ClassInfo.AddRange(db.project_class.Select(XC => new SelectListItem
            {
                Text = XC.class_name,
                Selected = false,
                Value = XC.class_number.ToString()
            }));

            var StatusInfo = new List<SelectListItem>();
            StatusInfo.AddRange(db.project_status.Select(c => new SelectListItem
            {
                Text = c.project_status_name,
                Selected = false,
                Value = c.project_status_number.ToString()
            }));

            var ProjectTypeInfo = new List<SelectListItem>();
            ProjectTypeInfo.AddRange(db.project_type.Select(VB => new SelectListItem
            {
                Text = VB.project_type_name,
                Selected = false,
                Value = VB.project_type_number.ToString()
            }));

            var SourceInfo = new List<SelectListItem>();
            SourceInfo.AddRange(db.lead_source.Select(SD => new SelectListItem
            {
                Text = SD.source_name,
                Selected = false,
                Value = SD.source_number.ToString()
            }));

            var AddressInfo = new List<SelectListItem>();
            AddressInfo.AddRange(db.addresses.Select(a => new SelectListItem
            {
                Text = a.address_type,
                Selected = false,
                Value = a.address_number.ToString()
            }));

            var EmpInfo = new List<SelectListItem> ();
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

            var DeliveryStatusInfo = new List<SelectListItem>();
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
           

            var Sstate = new List<SelectListItem> {
                new SelectListItem{ Text="WV",Selected=false,Value="WV"},
                 new SelectListItem{ Text="IN",Selected=false,Value="IN"},
                 new SelectListItem{ Text="NY",Selected=false,Value="NY"},
                 new SelectListItem{ Text="KY",Selected=false,Value="KY"}

            };
            ViewBag.Sstate = Sstate;

            address b = new address
            {
                address1 = form["Item2.address1"],
                //     address_type = form["Item2.address_type"],
                address_type = "JobAddress",
                city = form["Item2.city"],
                //    state = form["Item2.state"],
                state = form["state"],
             
                //   state = "Not fixed yet",
                county = form["Item2.county"],
                zipcode = form["Item2.zipcode"],
                deleted = false
            };
               db.addresses.Add(b);
               db.SaveChanges();



            lead target = new lead();
            {
                target.customer =db.customers.Where(aa=> aa.customer_number==id).FirstOrDefault();
                //   target.class_number = Int32.Parse(form["class_number"]);
                int a = Int32.Parse(form["class_number"]);
                target.project_class = db.project_class.Where(KL => KL.class_number == a).FirstOrDefault();
                int c = Int32.Parse(form["project_type_number"]);
                target.project_type = db.project_type.Where(GH => GH.project_type_number == c).FirstOrDefault();
                // target.project_type_number = Int32.Parse(form["project_type_number"]);
                int dd = Int32.Parse(form["source_number"]);
               target.lead_source = db.lead_source.Where(TY =>TY.source_number ==dd ).FirstOrDefault();
                int ee= Int32.Parse(form["source_number"]);
                target.lead_source =db.lead_source.Where(qq=> qq.source_number==ee).FirstOrDefault();
                int ff = Int32.Parse(form["emp_number"]);
                target.employee =db.employees.Where(ww=> ww.emp_number==ff).FirstOrDefault();
                int gg = Int32.Parse(form["branch_number"]);
                target.branch =db.branches.Where(ss=> ss.branch_number==gg).FirstOrDefault();
                int hh = Int32.Parse(form["delivery_status_number"]);
                target.delivery_status =db.delivery_status.Where(xx=> xx.delivery_status_number==hh).FirstOrDefault() ;
                target.in_city = true;//Convert.ToBoolean(form["Item1.in_city"]);
                target.project_name = form["Item1.project_name"];
                target.tax_exempt = true;//Convert.ToBoolean(form["Item1.tax_exempt"]);
                target.deleted = false;
                target.address_number = b.address_number;
                target.project_status_number = 3;
                target.lead_date = System.DateTime.Now;
                target.Last_update_date = System.DateTime.Now;

                
            };

            //create instance
            //lead target = new lead();
            //target.customer = db.customers.Where(a => a.customer_number == id).FirstOrDefault();
            ////get property
            //TryUpdateModel(target, new string[] { "class_number", "project_type_number", "source_number", "emp_number", "branch_number", "delivery_status_number", "in_city", "project_name", "tax_exempt" }, form.ToValueProvider());
            //target.deleted = false;
            //target.address_number = b.address_number;
            //target.project_status_number = 3;
            //target.lead_date = System.DateTime.Now;
            //target.Last_update_date = System.DateTime.Now;



            //validate
            //      if (string.IsNullOrEmpty(target.emp_firstname))
            //     ModelState.AddModelError("firstname", "firstname is required");

            //     if (ModelState.IsValid)
            //    {
            //linking 2 table
            target.address_number = b.address_number;
            db.leads.Add(target);
            db.SaveChanges();
            //    }
            ViewBag.m = " The lead was successfully created and saved to customer " + target.customer.customer_firstname + " " + target.customer.customer_lastname + " on " + System.DateTime.Now;
            return View();      



      

            ////create instance
            //lead target = new lead();
            //target.customer = db.customers.Where(a => a.customer_number == id).FirstOrDefault();
            ////get property
            //TryUpdateModel(target, new string[] { "class_number", "project_type_number", "source_number", "emp_number", "branch_number", "delivery_status_number", "in_city", "project_name", "tax_exempt" }, form.ToValueProvider());
            //target.deleted = false;
            ////  target.address_number = 1;
            //target.project_status_number = 3;
            //target.address_number = b.address_number;
            //target.lead_date = System.DateTime.Now;
            //target.Last_update_date = System.DateTime.Now;




            ////write change to db,address first
            //if (ModelState.IsValid)
            //{
            //    db.addresses.Add(b);
            //    db.SaveChanges();
            //    //linking 2 table

            //    db.leads.Add(target);
            //    db.SaveChanges();
            //}

            //ViewBag.m = " The lead was successfully created and saved to customer " + target.customer.customer_firstname + " " + target.customer.customer_lastname + " on " + System.DateTime.Now;
            //return View(target);
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

            var ClassInfo = new List<SelectListItem>();
            ClassInfo.AddRange(db.project_class.Select(b => new SelectListItem
            {
                Text = b.class_name,
                Selected = false,
                Value = b.class_number.ToString()
            }));

            var StatusInfo = new List<SelectListItem>();
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

            var SourceInfo = new List<SelectListItem>();
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
            TryUpdateModel(target, new string[] { "class_number", "project_type_number", "source_number", "emp_number", "branch_number", "delivery_status_number", "in_city", "project_name", "tax_exempt" }, form.ToValueProvider());
            target.deleted = false;
            target.address_number = 1;
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
            ViewBag.m = " The lead was successfully created and saved to customer " + target.customer.customer_firstname + " " + target.customer.customer_lastname + " on " + System.DateTime.Now;
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

            var ClassInfo = new List<SelectListItem>();
            ClassInfo.AddRange(db.project_class.Select(b => new SelectListItem
            {
                Text = b.class_name,
                Selected = false,
                Value = b.class_number.ToString()
            }));

            var StatusInfo = new List<SelectListItem>();
            StatusInfo.AddRange(db.project_status.Select(c => new SelectListItem
            {
                Text = c.project_status_name,
                Selected = false,
                Value = c.project_status_number.ToString()
            }));

            var ProjectTypeInfo = new List<SelectListItem> ();
            ProjectTypeInfo.AddRange(db.project_type.Select(b => new SelectListItem
            {
                Text = b.project_type_name,
                Selected = false,
                Value = b.project_type_number.ToString()
            }));

            var SourceInfo = new List<SelectListItem>();
            SourceInfo.AddRange(db.lead_source.Select(b => new SelectListItem
            {
                Text = b.source_name,
                Selected = false,
                Value = b.source_number.ToString()
            }));

            var AddressInfo = new List<SelectListItem>();
            AddressInfo.AddRange(db.addresses.Select(a => new SelectListItem
            {
                Text = a.address_type,
                Selected = false,
                Value = a.address_number.ToString()
            }));

            var EmpInfo = new List<SelectListItem>();
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

            var DeliveryStatusInfo = new List<SelectListItem> ();
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

            var ClassInfo = new List<SelectListItem>();
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

            var ProjectTypeInfo = new List<SelectListItem> ();
            ProjectTypeInfo.AddRange(db.project_type.Select(b => new SelectListItem
            {
                Text = b.project_type_name,
                Selected = false,
                Value = b.project_type_number.ToString()
            }));

            var SourceInfo = new List<SelectListItem>();
            SourceInfo.AddRange(db.lead_source.Select(b => new SelectListItem
            {
                Text = b.source_name,
                Selected = false,
                Value = b.source_number.ToString()
            }));

            var AddressInfo = new List<SelectListItem>();
            AddressInfo.AddRange(db.addresses.Select(a => new SelectListItem
            {
                Text = a.address_type,
                Selected = false,
                Value = a.address_number.ToString()
            }));

            var EmpInfo = new List<SelectListItem> ();
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

            var DeliveryStatusInfo = new List<SelectListItem> ();
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

            TryUpdateModel(target, new string[] {  "class_number", "project_status_number", "project_type_number",  "emp_number", "branch_number", "delivery_status_number", "in_city", "source_number", "project_name", "tax_exempt"}, form.ToValueProvider());
            target.Last_update_date = System.DateTime.Now;
            db.SaveChanges();
            return View(target);
        }

    }
}
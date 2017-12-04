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


        public ActionResult ListLead(FormCollection fo=null,string msg=null)
        {
            string search = fo["search"];
            int type = string.IsNullOrEmpty(fo["type"])?3:int.Parse(fo["type"]);
            DateTime start = string.IsNullOrEmpty(fo["start"])?DateTime.MinValue:DateTime.Parse(fo["start"]);
            DateTime end = string.IsNullOrEmpty(fo["end"])?DateTime.MaxValue : DateTime.Parse(fo["end"]);
            try
            {
                ViewBag.m = msg;
                var ClassInfo = new List<SelectListItem>();
                ClassInfo.AddRange(db.project_status.Where(CCVV => CCVV.project_status_name != "closed").Select(b => new SelectListItem
                
                {
                    Text = b.project_status_name,
                    Selected = false,
                    Value = b.project_status_number.ToString()
                }));
                ViewBag.lead_type = ClassInfo;

                return View(db.leads.Where(x => (x.project_name.Contains(search) || search == null) && x.project_status_number == type && (x.project_status_number != 6 && x.deleted == false)&&(x.lead_date>=start && x.lead_date<=end)).ToList());
            }
            catch (Exception e)
            {
                ViewBag.m = "Something went wrong ... "+e.Message;
                return View();
            }
        }


        public ActionResult Details(int id)
        {try
            {
                return View(db.leads.Where(x => x.lead_number == id).ToList());
            }
            catch (Exception e)
            {
                ViewBag.m = e.Message;
                return View();
            }
        }
        

        public ActionResult Add(int id, String msg = null)
        {
            ViewBag.m = msg;
            try
            {
                List<SelectListItem> CustomerInfo = new List<SelectListItem>();
                CustomerInfo.AddRange(db.customers.Where(x => x.deleted != true).Select(a => new SelectListItem
                {
                    Text = a.customer_lastname,
                    Selected = false,
                    Value = a.customer_number.ToString()
                }));

                var ClassInfo = new List<SelectListItem>();
                ClassInfo.AddRange(db.project_class.Where(x => x.deleted != true).Select(b => new SelectListItem
                {
                    Text = b.class_name,
                    Selected = false,
                    Value = b.class_number.ToString()
                }));

                var StatusInfo = new List<SelectListItem>();
                StatusInfo.AddRange(db.project_status.Where(x => x.deleted != true).Select(c => new SelectListItem
                {
                    Text = c.project_status_name,
                    Selected = false,
                    Value = c.project_status_number.ToString()
                }));

                var ProjectTypeInfo = new List<SelectListItem>();
                ProjectTypeInfo.AddRange(db.project_type.Where(x => x.deleted != true).Select(b => new SelectListItem
                {
                    Text = b.project_type_name,
                    Selected = false,
                    Value = b.project_type_number.ToString()
                }));

                var SourceInfo = new List<SelectListItem>();
                SourceInfo.AddRange(db.lead_source.Where(x => x.deleted != true).Select(b => new SelectListItem
                {
                    Text = b.source_name,
                    Selected = false,
                    Value = b.source_number.ToString()
                }));

                var AddressInfo = new List<SelectListItem>();
                AddressInfo.AddRange(db.addresses.Where(x => x.deleted != true).Select(a => new SelectListItem
                {
                    Text = a.address_type,
                    Selected = false,
                    Value = a.address_number.ToString()
                }));

                var AddressCityInfo = new List<SelectListItem>();

                var EmpInfo = new List<SelectListItem>();
                EmpInfo.AddRange(db.employees.Where(x => x.deleted != true).Select(a => new SelectListItem
                {
                    Text = a.emp_firstname + " " + a.emp_lastname,
                    Selected = false,
                    Value = a.emp_number.ToString()
                }));

                var BranchInfo = new List<SelectListItem>();
                BranchInfo.AddRange(db.branches.Where(x => x.deleted != true).Select(a => new SelectListItem
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
               new SelectListItem() {Text="Alabama", Value="Alabama"},
        new SelectListItem() { Text="Alaska", Value="Alaska"},
        new SelectListItem() { Text="Arizona", Value="Arizona"},
        new SelectListItem() { Text="Arkansas", Value="Arkansas"},
        new SelectListItem() { Text="California", Value="California"},
        new SelectListItem() { Text="Colorado", Value="Colorado"},
        new SelectListItem() { Text="Connecticut", Value="Connecticut"},
        new SelectListItem() { Text="District of Columbia", Value="District of Columbia"},
        new SelectListItem() { Text="Delaware", Value="Delaware"},
        new SelectListItem() { Text="Florida", Value="Florida"},
        new SelectListItem() { Text="Georgia", Value="Georgia"},
        new SelectListItem() { Text="Hawaii", Value="Hawaii"},
        new SelectListItem() { Text="Idaho", Value="Idaho"},
        new SelectListItem() { Text="Illinois", Value="Illinois"},
        new SelectListItem() { Text="Indiana", Value="Indiana"},
        new SelectListItem() { Text="Iowa", Value="Iowa"},
        new SelectListItem() { Text="Kansas", Value="Kansas"},
        new SelectListItem() { Text="Kentucky", Value="Kentucky"},
        new SelectListItem() { Text="Louisiana", Value="Louisiana"},
        new SelectListItem() { Text="Maine", Value="Maine"},
        new SelectListItem() { Text="Maryland", Value="Maryland"},
        new SelectListItem() { Text="Massachusetts", Value="Massachusetts"},
        new SelectListItem() { Text="Michigan", Value="Michigan"},
        new SelectListItem() { Text="Minnesota", Value="Minnesota"},
        new SelectListItem() { Text="Mississippi", Value="Mississippi"},
        new SelectListItem() { Text="Missouri", Value="Missouri"},
        new SelectListItem() { Text="Montana", Value="Montana"},
        new SelectListItem() { Text="Nebraska", Value="Nebraska"},
        new SelectListItem() { Text="Nevada", Value="Nevada"},
        new SelectListItem() { Text="New Hampshire", Value="New Hampshire"},
        new SelectListItem() { Text="New Jersey", Value="New Jersey"},
        new SelectListItem() { Text="New Mexico", Value="New Mexico"},
        new SelectListItem() { Text="New York", Value="New York"},
        new SelectListItem() { Text="North Carolina", Value="North Carolina"},
        new SelectListItem() { Text="North Dakota", Value="North Dakota"},
        new SelectListItem() { Text="Ohio", Value="Ohio"},
        new SelectListItem() { Text="Oklahoma", Value="Oklahoma"},
        new SelectListItem() { Text="Oregon", Value="Oregon"},
        new SelectListItem() { Text="Pennsylvania", Value="Pennsylvania"},
        new SelectListItem() { Text="Rhode Island", Value="Rhode Island"},
        new SelectListItem() { Text="South Carolina", Value="South Carolina"},
        new SelectListItem() { Text="South Dakota", Value="South Dakota"},
        new SelectListItem() { Text="Tennessee", Value="Tennessee"},
        new SelectListItem() { Text="Texas", Value="Texas"},
        new SelectListItem() { Text="Utah", Value="Utah"},
        new SelectListItem() { Text="Vermont", Value="Vermont"},
        new SelectListItem() { Text="Virginia", Value="Virginia"},
        new SelectListItem() { Text="Washington", Value="Washington"},
        new SelectListItem() { Text="West Virginia", Value="West Virginia"},
        new SelectListItem() { Text="Wisconsin", Value="Wisconsin"},
        new SelectListItem() { Text="Wyoming", Value="Wyoming"}

            };
                ViewBag.Sstate = Sstate;
                return View();
            }
            catch
            {
                ViewBag.m = " Something went wrong ... please try again";
                return View();
            }
        }
        
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Add(FormCollection form, int id, String msg = null)
        {
            ViewBag.m = msg;
            try
            {
                List<SelectListItem> CustomerInfo = new List<SelectListItem>();
                CustomerInfo.AddRange(db.customers.Where(x => x.deleted != true).Select(a => new SelectListItem
                {
                    Text = a.customer_lastname,
                    Selected = false,
                    Value = a.customer_number.ToString()
                }));

                var ClassInfo = new List<SelectListItem>();
                ClassInfo.AddRange(db.project_class.Where(x => x.deleted != true).Select(LLL => new SelectListItem
                {
                    Text = LLL.class_name,
                    Selected = false,
                    Value = LLL.class_number.ToString()
                }));

                var StatusInfo = new List<SelectListItem>();
                StatusInfo.AddRange(db.project_status.Where(x => x.deleted != true).Select(c => new SelectListItem
                {
                    Text = c.project_status_name,
                    Selected = false,
                    Value = c.project_status_number.ToString()
                }));

                var ProjectTypeInfo = new List<SelectListItem>();
                ProjectTypeInfo.AddRange(db.project_type.Where(x => x.deleted != true).Select(VVVb => new SelectListItem
                {
                    Text = VVVb.project_type_name,
                    Selected = false,
                    Value = VVVb.project_type_number.ToString()
                }));

                var SourceInfo = new List<SelectListItem>();
                SourceInfo.AddRange(db.lead_source.Where(x => x.deleted != true).Select(ffb => new SelectListItem
                {
                    Text = ffb.source_name,
                    Selected = false,
                    Value = ffb.source_number.ToString()
                }));

                var AddressInfo = new List<SelectListItem>();
                AddressInfo.AddRange(db.addresses.Where(x => x.deleted != true).Select(a => new SelectListItem
                {
                    Text = a.address_type,
                    Selected = false,
                    Value = a.address_number.ToString()
                }));

                var AddressCityInfo = new List<SelectListItem>();

                var EmpInfo = new List<SelectListItem>();
                EmpInfo.AddRange(db.employees.Where(x => x.deleted != true).Select(a => new SelectListItem
                {
                    Text = a.emp_firstname + " " + a.emp_lastname,
                    Selected = false,
                    Value = a.emp_number.ToString()
                }));

                var BranchInfo = new List<SelectListItem>();
                BranchInfo.AddRange(db.branches.Where(x => x.deleted != true).Select(a => new SelectListItem
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
               new SelectListItem() {Text="Alabama", Value="Alabama"},
        new SelectListItem() { Text="Alaska", Value="Alaska"},
        new SelectListItem() { Text="Arizona", Value="Arizona"},
        new SelectListItem() { Text="Arkansas", Value="Arkansas"},
        new SelectListItem() { Text="California", Value="California"},
        new SelectListItem() { Text="Colorado", Value="Colorado"},
        new SelectListItem() { Text="Connecticut", Value="Connecticut"},
        new SelectListItem() { Text="District of Columbia", Value="District of Columbia"},
        new SelectListItem() { Text="Delaware", Value="Delaware"},
        new SelectListItem() { Text="Florida", Value="Florida"},
        new SelectListItem() { Text="Georgia", Value="Georgia"},
        new SelectListItem() { Text="Hawaii", Value="Hawaii"},
        new SelectListItem() { Text="Idaho", Value="Idaho"},
        new SelectListItem() { Text="Illinois", Value="Illinois"},
        new SelectListItem() { Text="Indiana", Value="Indiana"},
        new SelectListItem() { Text="Iowa", Value="Iowa"},
        new SelectListItem() { Text="Kansas", Value="Kansas"},
        new SelectListItem() { Text="Kentucky", Value="Kentucky"},
        new SelectListItem() { Text="Louisiana", Value="Louisiana"},
        new SelectListItem() { Text="Maine", Value="Maine"},
        new SelectListItem() { Text="Maryland", Value="Maryland"},
        new SelectListItem() { Text="Massachusetts", Value="Massachusetts"},
        new SelectListItem() { Text="Michigan", Value="Michigan"},
        new SelectListItem() { Text="Minnesota", Value="Minnesota"},
        new SelectListItem() { Text="Mississippi", Value="Mississippi"},
        new SelectListItem() { Text="Missouri", Value="Missouri"},
        new SelectListItem() { Text="Montana", Value="Montana"},
        new SelectListItem() { Text="Nebraska", Value="Nebraska"},
        new SelectListItem() { Text="Nevada", Value="Nevada"},
        new SelectListItem() { Text="New Hampshire", Value="New Hampshire"},
        new SelectListItem() { Text="New Jersey", Value="New Jersey"},
        new SelectListItem() { Text="New Mexico", Value="New Mexico"},
        new SelectListItem() { Text="New York", Value="New York"},
        new SelectListItem() { Text="North Carolina", Value="North Carolina"},
        new SelectListItem() { Text="North Dakota", Value="North Dakota"},
        new SelectListItem() { Text="Ohio", Value="Ohio"},
        new SelectListItem() { Text="Oklahoma", Value="Oklahoma"},
        new SelectListItem() { Text="Oregon", Value="Oregon"},
        new SelectListItem() { Text="Pennsylvania", Value="Pennsylvania"},
        new SelectListItem() { Text="Rhode Island", Value="Rhode Island"},
        new SelectListItem() { Text="South Carolina", Value="South Carolina"},
        new SelectListItem() { Text="South Dakota", Value="South Dakota"},
        new SelectListItem() { Text="Tennessee", Value="Tennessee"},
        new SelectListItem() { Text="Texas", Value="Texas"},
        new SelectListItem() { Text="Utah", Value="Utah"},
        new SelectListItem() { Text="Vermont", Value="Vermont"},
        new SelectListItem() { Text="Virginia", Value="Virginia"},
        new SelectListItem() { Text="Washington", Value="Washington"},
        new SelectListItem() { Text="West Virginia", Value="West Virginia"},
        new SelectListItem() { Text="Wisconsin", Value="Wisconsin"},
        new SelectListItem() { Text="Wyoming", Value="Wyoming"}

            };
                ViewBag.Sstate = Sstate;

                address b = new address
                {
                    address1 = form["Item2.address1"],
                    address_type = "JobAddress",
                    city = form["Item2.city"],
                    state = form["state"],
                    county = form["Item2.county"],
                    zipcode = form["Item2.zipcode"],
                    deleted = false
                };
                db.addresses.Add(b);
                db.SaveChanges();



                lead target = new lead();
                {
                    target.customer = db.customers.Where(aa => aa.customer_number == id).FirstOrDefault();
                    int a = Int32.Parse(form["class_number"]);
                    target.project_class = db.project_class.Where(KL => KL.class_number == a).FirstOrDefault();
                    int c = Int32.Parse(form["project_type_number"]);
                    target.project_type = db.project_type.Where(GH => GH.project_type_number == c).FirstOrDefault();
                    int dd = Int32.Parse(form["source_number"]);
                    target.lead_source = db.lead_source.Where(TY => TY.source_number == dd).FirstOrDefault();
                    int ee = Int32.Parse(form["source_number"]);
                    target.lead_source = db.lead_source.Where(qq => qq.source_number == ee).FirstOrDefault();
                    int ff = Int32.Parse(form["emp_number"]);
                    target.employee = db.employees.Where(ww => ww.emp_number == ff).FirstOrDefault();
                    int gg = Int32.Parse(form["branch_number"]);
                    target.branch = db.branches.Where(ss => ss.branch_number == gg).FirstOrDefault();
                    int hh = Int32.Parse(form["delivery_status_number"]);
                    target.delivery_status = db.delivery_status.Where(xx => xx.delivery_status_number == hh).FirstOrDefault();


                    target.in_city = Convert.ToBoolean(form["Item1.in_city"].Split(',')[0]);
                    string aaa = form["Item1.in_city"];
                    target.tax_exempt = Convert.ToBoolean(form["Item1.tax_exempt"].Split(',')[0]);
                    target.project_name = form["Item1.project_name"];
                    target.phone_number = form["Item1.phone_number"];
                    target.second_phone_number = form["Item1.second_phone_number"];
                    target.email = form["Item1.email"];

                    target.deleted = false;
                    target.address_number = b.address_number;
                    target.project_status_number = 3;
                    target.lead_date = System.DateTime.Now;
                    target.Last_update_date = System.DateTime.Now;


                };


                //linking 2 table
                target.address_number = b.address_number;
                db.leads.Add(target);
                db.SaveChanges();
                //    }

                ViewBag.m = " The lead was successfully created to " + target.customer.customer_firstname + " " + target.customer.customer_lastname + " on " + System.DateTime.Now;

                return View();
            }
            catch (Exception e)
            {
                ViewBag.m = "The lead was not created ..." + e.Message;
                return View();
            }
        }
      
        

        // read from the DB
        public ActionResult Edit(int id)
        {
            try
            {
                List<SelectListItem> CustomerInfo = new List<SelectListItem>();
                CustomerInfo.AddRange(db.customers.Where(x => x.deleted != true).Select(a => new SelectListItem
                {
                    Text = a.customer_lastname,
                    Selected = false,
                    Value = a.customer_number.ToString()
                }));

                var ClassInfo = new List<SelectListItem>();
                ClassInfo.AddRange(db.project_class.Where(x => x.deleted != true).Select(b => new SelectListItem
                {
                    Text = b.class_name,
                    Selected = false,
                    Value = b.class_number.ToString()
                }));

                var StatusInfo = new List<SelectListItem>();
                StatusInfo.AddRange(db.project_status.Where(x => x.deleted != true).Select(c => new SelectListItem
                {
                    Text = c.project_status_name,
                    Selected = false,
                    Value = c.project_status_number.ToString()
                }));

                var ProjectTypeInfo = new List<SelectListItem>();
                ProjectTypeInfo.AddRange(db.project_type.Where(x => x.deleted != true).Select(b => new SelectListItem
                {
                    Text = b.project_type_name,
                    Selected = false,
                    Value = b.project_type_number.ToString()
                }));

                var SourceInfo = new List<SelectListItem>();
                SourceInfo.AddRange(db.lead_source.Where(x => x.deleted != true).Select(b => new SelectListItem
                {
                    Text = b.source_name,
                    Selected = false,
                    Value = b.source_number.ToString()
                }));

                var AddressInfo = new List<SelectListItem>();
                AddressInfo.AddRange(db.addresses.Where(x => x.deleted != true).Select(a => new SelectListItem
                {
                    Text = a.address_type,
                    Selected = false,
                    Value = a.address_number.ToString()
                }));

                var AddressCityInfo = new List<SelectListItem>();

                var EmpInfo = new List<SelectListItem>();
                EmpInfo.AddRange(db.employees.Where(x => x.deleted != true).Select(a => new SelectListItem
                {
                    Text = a.emp_firstname + " " + a.emp_lastname,
                    Selected = false,
                    Value = a.emp_number.ToString()
                }));

                var BranchInfo = new List<SelectListItem>();
                BranchInfo.AddRange(db.branches.Where(x => x.deleted != true).Select(a => new SelectListItem
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



                List<lead> Leads_list = db.leads.Where(d => d.lead_number == id).ToList();
                ViewBag.Customerslist = Leads_list;
                lead target = Leads_list[0];
                return View(target);
            }
            catch (Exception e)
            {
                ViewBag.m = "Something went wrong ..." + e.Message;
                return View();
            }
        }

        // Write to the DB that is why we use POST
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int id, FormCollection form)
        {

            try
            {
                List<SelectListItem> CustomerInfo = new List<SelectListItem>();
                CustomerInfo.AddRange(db.customers.Where(x => x.deleted != true).Select(a => new SelectListItem
                {
                    Text = a.customer_lastname,
                    Selected = false,
                    Value = a.customer_number.ToString()
                }));

                var ClassInfo = new List<SelectListItem>();
                ClassInfo.AddRange(db.project_class.Where(x => x.deleted != true).Select(b => new SelectListItem
                {
                    Text = b.class_name,
                    Selected = false,
                    Value = b.class_number.ToString()
                }));

                var StatusInfo = new List<SelectListItem>();
                StatusInfo.AddRange(db.project_status.Where(x => x.deleted != true).Select(c => new SelectListItem
                {
                    Text = c.project_status_name,
                    Selected = false,
                    Value = c.project_status_number.ToString()
                }));

                var ProjectTypeInfo = new List<SelectListItem>();
                ProjectTypeInfo.AddRange(db.project_type.Where(x => x.deleted != true).Select(b => new SelectListItem
                {
                    Text = b.project_type_name,
                    Selected = false,
                    Value = b.project_type_number.ToString()
                }));

                var SourceInfo = new List<SelectListItem>();
                SourceInfo.AddRange(db.lead_source.Where(x => x.deleted != true).Select(b => new SelectListItem
                {
                    Text = b.source_name,
                    Selected = false,
                    Value = b.source_number.ToString()
                }));

                var AddressInfo = new List<SelectListItem>();
                AddressInfo.AddRange(db.addresses.Where(x => x.deleted != true).Select(a => new SelectListItem
                {
                    Text = a.address_type,
                    Selected = false,
                    Value = a.address_number.ToString()
                }));

                var AddressCityInfo = new List<SelectListItem>();

                var EmpInfo = new List<SelectListItem>();
                EmpInfo.AddRange(db.employees.Where(x => x.deleted != true).Select(a => new SelectListItem
                {
                    Text = a.emp_firstname + " " + a.emp_lastname,
                    Selected = false,
                    Value = a.emp_number.ToString()
                }));

                var BranchInfo = new List<SelectListItem>();
                BranchInfo.AddRange(db.branches.Where(x => x.deleted != true).Select(a => new SelectListItem
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



                List<lead> Leads_list = db.leads.Where(d => d.lead_number == id).ToList();
                ViewBag.Customerslist = Leads_list;
                lead target = Leads_list[0];

                TryUpdateModel(target, new string[] { "class_number", "project_status_number", "project_type_number", "emp_number", "branch_number", "delivery_status_number", "in_city", "source_number", "project_name", "tax_exempt", "phone_number", "second_phone_number", "email" }, form.ToValueProvider());
                target.Last_update_date = System.DateTime.Now;
                db.SaveChanges(id);


                ViewBag.m = " The lead was successfully updated " + " on " + System.DateTime.Now;


                return View(target);
            }

            catch (Exception e)
            {
                ViewBag.m = "The lead was not updated ..." + e.Message;
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                List<lead> Lead_list = db.leads.Where(d => d.lead_number == id).ToList();
                ViewBag.Customerslist = Lead_list;
                lead target = Lead_list[0];
                target.deleted = true;
                db.SaveChanges(id,"delete");
            ViewBag.m = "The lead was successfully deleted.";
            return RedirectToAction("ListLead", new { search = "", msg = ViewBag.m });
        }
            catch (Exception e)
            {
                ViewBag.m = "The lead was not deleted ... " + e.Message;
                return RedirectToAction("ListLead", new { search = "", msg = ViewBag.m });
            }
        }

    }
}
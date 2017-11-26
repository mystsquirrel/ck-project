using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace ck_project.Controllers
{
    public class EmpController : Controller
    {
        // GET: Emp
        public ActionResult Index()
        {
            return View();
        }


        //Creating the DB connecton
        ckdatabase db = new ckdatabase();



        // GET: Employees
    //    public ActionResult ListEmp()
      //  {
        //    List<employee> Employees_list = db.employees.ToList();
          //  ViewBag.Employeeslist = Employees_list;
            //return View();
        //}


        public ActionResult ListEmp(string search)
        {
            return View(db.employees.Where(x => x.emp_lastname.StartsWith(search) || search == null && x.deleted == false).ToList());
        }


        // read from the DB
        public ActionResult Edit(int id)
        {

            //setting dropdown list for forgern key
            var utype = new List<SelectListItem> ();
            utype.AddRange(db.users_types.Select(a => new SelectListItem
            {
                Text = a.user_type_name,
                Selected = false,
                Value = a.user_type_number.ToString()
            }));

            var branchtypes = new List<SelectListItem> ();
            branchtypes.AddRange(db.branches.Select(b => new SelectListItem
            {
                Text = b.branch_name,
                Selected = false,
                Value = b.branch_number.ToString()
            }));
            //setting variable passing
            ViewBag.utype = utype;
            ViewBag.branch = branchtypes;
            ViewBag.id = id;


            List<employee> Employees_list = db.employees.Where(d => d.emp_number == id).ToList();
            ViewBag.Customerslist = Employees_list;
            employee target = Employees_list[0];
            target.emp_password = EncryptionHelper.Decrypt(target.emp_password);
              return View(target);           
        }


        // Write to the DB that is why we use POST
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Edit(int id, FormCollection fo)
        {


            //setting dropdown list for forgern key
            var utype = new List<SelectListItem> ();
            utype.AddRange(db.users_types.Select(a => new SelectListItem
            {
                Text = a.user_type_name,
                Selected = false,
                Value = a.user_type_number.ToString()
            }));

            var branchtypes = new List<SelectListItem>();
            branchtypes.AddRange(db.branches.Select(b => new SelectListItem
            {
                Text = b.branch_name,
                Selected = false,
                Value = b.branch_number.ToString()
            }));
            //setting variable passing
            ViewBag.utype = utype;
            ViewBag.branch = branchtypes;


            List<employee> Employees_list = db.employees.Where(d => d.emp_number == id).ToList();
            ViewBag.Employeeslist = Employees_list;
            employee target = Employees_list[0];
            TryUpdateModel(target, new string[] { "emp_firstname", "emp_middlename", "emp_lastname", "emp_username","emo_password", "user_type_number", "branch_number", "phone_number" }, fo.ToValueProvider());
            target.emp_password = EncryptionHelper.Encrypt(target.emp_password);
            db.SaveChanges();
            return View(target);
        }
        

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Add(FormCollection form)
        {
            //setting dropdown list for forgern key
            var utype = new List<SelectListItem> ();
            utype.AddRange(db.users_types.Select(a => new SelectListItem
            {
                Text = a.user_type_name,
                Selected = false,
                Value = a.user_type_number.ToString()
            }));

            var branchtypes = new List<SelectListItem> ();
            branchtypes.AddRange(db.branches.Select(b => new SelectListItem
            {
                Text = b.branch_name,
                Selected = false,
                Value = b.branch_number.ToString()
            }));
            //setting variable passing
            ViewBag.utype = utype;
            ViewBag.branch = branchtypes;
            //create instance
            employee target = new employee();
            //get property
            TryUpdateModel(target, new string[] { "emp_firstname", "emp_middlename", "emp_password","emp_lastname", "emp_username", "user_type_number", "branch_number", "emp_number", "phone_number" }, form.ToValueProvider());

            //validate
            double tempPhoneNumber = 0;
            ViewBag.Error = "";

            if (string.IsNullOrEmpty(target.emp_firstname))
            {
                ViewBag.Error = "firstname is required";
            }
            else if (target.emp_firstname.Length > 35)
            {
                ViewBag.Error = "firstname is 35";
            }
            else if (string.IsNullOrEmpty(target.phone_number))
            {
                ViewBag.Error = "phone number is required";
            }
            // ValidatePhoneNumber
            else if (!Regex.Match(target.phone_number, @"^(\+[0-9]{9})$").Success)
            {
                ViewBag.Error = "phone nymber must be numbers";
            }
            else
            {
                target.emp_password = EncryptionHelper.Encrypt(target.emp_password);
                db.employees.Add(target);
                db.SaveChanges();
            }

            return View(target);
        }

        public ActionResult Add()
        {
            var utype = new List<SelectListItem> ();
            utype.AddRange(db.users_types.Select(a => new SelectListItem
            {
                Text = a.user_type_name,
                Selected = false,
                Value = a.user_type_number.ToString()
            }));

            var branchtypes = new List<SelectListItem> ();
            branchtypes.AddRange(db.branches.Select(b => new SelectListItem
            {
                Text = b.branch_name,
                Selected = false,
                Value = b.branch_number.ToString()
            }));

            ViewBag.utype = utype;
            ViewBag.branch = branchtypes;
            return View();
        }


    }
}
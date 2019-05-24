using RentMovie.Models;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using RentMovie.ViewModel;
using System.Data.Entity.Validation;

namespace RentMovie.Controllers
{
    public class CustomersController : Controller
    {
        public ApplicationDbContext _context { get; set; }

        //Constructor 
        public CustomersController()
        {  
            _context = new ApplicationDbContext(); 
        }

        //Index Action Result 
        public ActionResult Index()
        {
            return View();
        }

        //Edit Action Result
        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            var viewModel = new CustomerFormViewModel
            {
                Customers = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("NewCustomerForm", viewModel);
        }

        //New Action Result
        public ActionResult New()
        {
            var memvershipTypes = _context.MembershipTypes.ToList(); 
            var viewModel = new CustomerFormViewModel
            {
                Customers = new Customer(), //When we initiazed the "Customer", properties will be initialized to default value
                                            // Hense, "customerId (int)" will be initialized to Zero(0) ... 
                MembershipTypes = memvershipTypes
            };

            return View("NewCustomerForm", viewModel);
        }

        //Save Action Result
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CustomerFormViewModel customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customers = customer.Customers,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("NewCustomerForm", viewModel);
            }

            if (customer.Customers.Id == 0)
            {
                _context.Customers.Add(customer.Customers);
                _context.SaveChanges(); 
            }
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Customers.Id);

                customerInDb.Name = customer.Customers.Name;
                customerInDb.Birthdate = customer.Customers.Birthdate;
                customerInDb.IsSubscribedToNewsLetter = customer.Customers.IsSubscribedToNewsLetter;
                customerInDb.MembershipTypeId = customer.Customers.MembershipTypeId;

                try
                {

                    _context.SaveChanges();
                }
                catch (DbEntityValidationException ee)
                {
                    foreach (var error in ee.EntityValidationErrors)
                    {
                        foreach (var thisError in error.ValidationErrors)
                        {
                            return Content(string.Format("{0}: {1}", thisError.PropertyName, thisError.ErrorMessage));
                        }
                    }
                }
            }
           
            return RedirectToAction("Index", "Customers");
        }
    }
}
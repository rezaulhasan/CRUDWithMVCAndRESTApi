using AutoMapper;
using RentMovie.Dtos;
using RentMovie.Models;
using System;
using System.Data.Entity; 
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace RentMovie.Controllers.Api
{

    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context { get; set; }

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        //Get/Api/Customers
        public IHttpActionResult GetCustomers(string query = null)
        {
            var customersQuery = _context.Customers
                             .Include(c => c.MembershipType);

            if (!string.IsNullOrWhiteSpace(query))
                customersQuery = customersQuery.Where(c=>c.Name.Contains(query));

            var customerDtos = customersQuery
                             .ToList()
                             .Select(Mapper.Map<Customer, CustomerDto>);
            return Ok(customerDtos); 
        }

        //Get/Api/Customers/1
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound(); 

            return Ok (Mapper.Map<Customer, CustomerDto>(customer));
        }

        //Post/api/customers 
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto) //Source "CustomerDto", Target "Customer"
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;

            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }

        //PUT/api/customers/1
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var customerInDb = _context.Customers.Single(c => c.Id == id);

            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(customerDto, customerInDb);
            _context.SaveChanges();
        }

        //DELETE/api/customers/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.Single(c => c.Id == id);

            if (customerInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();
        }
    }
}

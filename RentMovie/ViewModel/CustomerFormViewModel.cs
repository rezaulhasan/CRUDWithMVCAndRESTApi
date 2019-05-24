using RentMovie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RentMovie.ViewModel
{
    public class CustomerFormViewModel
    {
        public Customer Customers { get; set; }
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
    }
}
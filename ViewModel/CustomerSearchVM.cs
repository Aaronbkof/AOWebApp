using AOWebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AOWebApp.ViewModel
{
    public class CustomerSearchVM
    {
        [Required(ErrorMessage ="You must provide a Customer Name")]
        public string SearchText { get; set; }
        public string Suburb { get; set; }

        public SelectList SuburbSelectList { get; set; }

        public IEnumerable<Customer>? CustomerList { get; set; }


    }
}

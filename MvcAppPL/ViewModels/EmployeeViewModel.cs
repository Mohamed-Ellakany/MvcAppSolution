using MvcAppDAL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace MvcAppPL.ViewModels
{
    public class EmployeeViewModel
    {

        public int Id { get; set; }


        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50, ErrorMessage = "Max length is 50 chars")]
        [MinLength(3, ErrorMessage = "Min length is 50 chars")]
        public string Name { get; set; }

        [Range(22, 40, ErrorMessage = "Age Must Be in range from 22 to 40")]
        public int? Age { get; set; }


        [RegularExpression("^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}", ErrorMessage = "addess must be like 123-street-city-country")]
        public string Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        public bool IsActive { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public DateTime HireDate { get; set; }

        public IFormFile Image { get; set; }

        public string ImageName { get; set; }


        [ForeignKey("department")]
        public int? DepartmentId { get; set; }

        [InverseProperty("Employees")]
        public Department department { get; set; }

    }
}

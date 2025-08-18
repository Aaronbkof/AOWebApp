using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AOWebApp.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    [Display(Name = "First Name")]
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string MainPhoneNumber { get; set; } = null!;
    public string? SecondaryPhoneNumber { get; set; }
    public int AddressId { get; set; }
    public virtual Address Address { get; set; } = null!;
    public virtual ICollection<CustomerOrder> CustomerOrders { get; set; } = new List<CustomerOrder>();
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    // Non-mapped properties
    [NotMapped]
    [Display(Name = "Customer Name")]
    public string FullName
    {
        get
        {
            return $"{FirstName} {LastName}";
        }
    }

    [NotMapped]
    [Display(Name = "Contact Number")]
    public string ContactNumber
    {
        get
        {
            if (!string.IsNullOrEmpty(MainPhoneNumber) && !string.IsNullOrEmpty(SecondaryPhoneNumber))
            {
                return $"{MainPhoneNumber}<br/>{SecondaryPhoneNumber}";
            }
            else if (!string.IsNullOrEmpty(MainPhoneNumber))
            {
                return MainPhoneNumber;
            }
            else if (!string.IsNullOrEmpty(SecondaryPhoneNumber))
            {
                return SecondaryPhoneNumber;
            }
            else
            {
                return "No phone number";
            }
        }
    }

    [NotMapped]
    [Display(Name = "Address")]
    public string FullAddress
    {
        get
        {
            if (Address != null)
            {
                // Format: AddressLine on first line, then Suburb, Postcode [Region] on second line
                var addressLine1 = !string.IsNullOrEmpty(Address.AddressLine) ? Address.AddressLine : "";

                var addressLine2Parts = new List<string>();

                if (!string.IsNullOrEmpty(Address.Suburb))
                    addressLine2Parts.Add(Address.Suburb);

                if (!string.IsNullOrEmpty(Address.Postcode))
                    addressLine2Parts.Add(Address.Postcode);

                if (!string.IsNullOrEmpty(Address.Region))
                    addressLine2Parts.Add($"[{Address.Region}]");

                var addressLine2 = string.Join(", ", addressLine2Parts);

                // Combine with HTML line break
                if (!string.IsNullOrEmpty(addressLine1) && !string.IsNullOrEmpty(addressLine2))
                {
                    return $"{addressLine1}<br/>{addressLine2}";
                }
                else if (!string.IsNullOrEmpty(addressLine1))
                {
                    return addressLine1;
                }
                else if (!string.IsNullOrEmpty(addressLine2))
                {
                    return addressLine2;
                }
            }
            return "No address";
        }
    }
}
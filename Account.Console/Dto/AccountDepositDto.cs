using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Console.Dto
{
  // Use Case Rules
  public class AccountDepositDto
  {
    [Required(ErrorMessage = "CustomerId boş geçilemez")]
    public string CustomerId { get; set; }
    public string AccountNumber { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; }

    public string Channel { get; set; } // Online/Bank/ATM



  }
}

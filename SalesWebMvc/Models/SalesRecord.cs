using SalesWebMvc.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace SalesWebMvc.Models
{
    public class SalesRecord
    {
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Display(Name = "Data da venda")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "{0} obrigatória")]
        public DateTime Date { get; set; }

        [Display(Name="Valor total")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Required(ErrorMessage = "{0} obrigatório")]
        public double Amount { get; set; }

        [Display(Name = "Status da venda")]
        [Required(ErrorMessage = "{0} obrigatório")]
        public SaleStatus Status { get; set; }

        [Display(Name = "Vendedor")]
        [Required(ErrorMessage = "{0} obrigatório")]
        public Seller Seller { get; set; }

        public SalesRecord()
        {

        }

        public SalesRecord(int id, DateTime date, double amount, SaleStatus status, Seller seller)
        {
            Id = id;
            Date = date;
            Amount = amount;
            Status = status;
            Seller = seller;
        }
    }
}

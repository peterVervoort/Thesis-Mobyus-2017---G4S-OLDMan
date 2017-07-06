using G4S.Entities.Pocos;
using System;
using System.ComponentModel.DataAnnotations;

namespace G4S.Models
{
    public class OrderItemHistoryPostModel : PostModelBase<OrderItemHistory>
    {
        [Required]
        public int OrderItemId { get; set; }
        public int? StateChangeId { get; set; }
    }
}
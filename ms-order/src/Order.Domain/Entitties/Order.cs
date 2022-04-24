using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Order.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }

    public class OrderItem
    {
        [Key]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public decimal Value { get; set; }
        public int KitchenIdentifier { get; set; }
    }
}

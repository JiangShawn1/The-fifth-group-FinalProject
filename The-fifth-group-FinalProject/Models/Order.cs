﻿using System;
using System.Collections.Generic;

namespace The_fifth_group_FinalProject.Models
{
    public partial class Order
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public string OrderNumber { get; set; } = null!;
        public DateTime? CreateAt { get; set; }
        public int OrderType { get; set; }
        public int OrderStatus { get; set; }
        public int TradeStatus { get; set; }
        public int? UseCoupon { get; set; }
        public int Amount { get; set; }
        public string? ShippingMethod { get; set; }
        public string? OrderAddress { get; set; }
        public string OrderContent { get; set; } = null!;

        public virtual Member Member { get; set; } = null!;
        public virtual Coupon? UseCouponNavigation { get; set; }
        public List<OrderItem> OrderItem { get; set; }
    }
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public int Amount { get; set; }
        public int SubTotal { get; set; }
    }
    public class PCartItem : OrderItem
    {
        public PCartItem() { }
        public PCartItem(OrderItem order)
        {
            this.OrderId = order.OrderId;
            this.ProductName = order.ProductName;
            this.Amount = order.Amount;
            this.SubTotal = order.SubTotal;
        }
        public Product Product { get; set; } //商品內容
        public string imageSrc { get; set; } //商品圖片
    }
}

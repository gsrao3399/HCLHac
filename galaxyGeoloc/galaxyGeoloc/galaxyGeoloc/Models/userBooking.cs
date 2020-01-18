//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace galaxyGeoloc.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class userBooking
    {
        public int transactionId { get; set; }
        public Nullable<int> userId { get; set; }
        public Nullable<int> hotelId { get; set; }
        public Nullable<int> numberOfRooms { get; set; }
        public Nullable<int> roomType { get; set; }
        public Nullable<decimal> totalPrice { get; set; }
        public Nullable<System.DateTime> createdDateTime { get; set; }
        public Nullable<int> modifiedBy { get; set; }
        public Nullable<System.DateTime> modifiedDate { get; set; }
    
        public virtual hotel hotel { get; set; }
        public virtual user user { get; set; }
    }
}

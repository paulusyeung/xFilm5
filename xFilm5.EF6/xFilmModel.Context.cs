﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace xFilm5.EF6
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class xFilmEntities : DbContext
    {
        public xFilmEntities()
            : base("name=xFilmEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Acct_INDetails> Acct_INDetails { get; set; }
        public virtual DbSet<Acct_INMaster> Acct_INMaster { get; set; }
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Client_AddressBook> Client_AddressBook { get; set; }
        public virtual DbSet<Client_User> Client_User { get; set; }
        public virtual DbSet<ClientPricing> ClientPricing { get; set; }
        public virtual DbSet<Order_Details> Order_Details { get; set; }
        public virtual DbSet<Order_Internal> Order_Internal { get; set; }
        public virtual DbSet<Order_Journal> Order_Journal { get; set; }
        public virtual DbSet<OrderComment> OrderComment { get; set; }
        public virtual DbSet<OrderHeader> OrderHeader { get; set; }
        public virtual DbSet<OrderPkPrintQueueVps> OrderPkPrintQueueVps { get; set; }
        public virtual DbSet<PosTx> PosTx { get; set; }
        public virtual DbSet<PrintQueue> PrintQueue { get; set; }
        public virtual DbSet<PrintQueue_LifeCycle> PrintQueue_LifeCycle { get; set; }
        public virtual DbSet<PrintQueue_VPS> PrintQueue_VPS { get; set; }
        public virtual DbSet<ReceiptDetail> ReceiptDetail { get; set; }
        public virtual DbSet<ReceiptHeader> ReceiptHeader { get; set; }
        public virtual DbSet<Staff> Staff { get; set; }
        public virtual DbSet<T_BillingCode_Dept> T_BillingCode_Dept { get; set; }
        public virtual DbSet<T_BillingCode_Item> T_BillingCode_Item { get; set; }
        public virtual DbSet<T_DeliveryMethod> T_DeliveryMethod { get; set; }
        public virtual DbSet<T_FriendlyService> T_FriendlyService { get; set; }
        public virtual DbSet<T_JobTitle> T_JobTitle { get; set; }
        public virtual DbSet<T_LSR> T_LSR { get; set; }
        public virtual DbSet<T_Media> T_Media { get; set; }
        public virtual DbSet<T_PaperSize> T_PaperSize { get; set; }
        public virtual DbSet<T_PaymentType> T_PaymentType { get; set; }
        public virtual DbSet<T_Platform> T_Platform { get; set; }
        public virtual DbSet<T_Priority> T_Priority { get; set; }
        public virtual DbSet<T_ProofingMaterial> T_ProofingMaterial { get; set; }
        public virtual DbSet<T_Service> T_Service { get; set; }
        public virtual DbSet<T_Software> T_Software { get; set; }
        public virtual DbSet<T_SoftwareVersion> T_SoftwareVersion { get; set; }
        public virtual DbSet<T_Status> T_Status { get; set; }
        public virtual DbSet<T_Workflow> T_Workflow { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserPreference> UserPreference { get; set; }
        public virtual DbSet<X_Counter> X_Counter { get; set; }
        public virtual DbSet<Z_WebColor> Z_WebColor { get; set; }
        public virtual DbSet<vwReceiptDetailsList_Ex> vwReceiptDetailsList_Ex { get; set; }
        public virtual DbSet<vwOrderPkPrintQueueVpsList> vwOrderPkPrintQueueVpsList { get; set; }
        public virtual DbSet<vwOrderPkPrintQueueVpsList_Blueprint> vwOrderPkPrintQueueVpsList_Blueprint { get; set; }
        public virtual DbSet<vwOrderPkPrintQueueVpsList_Film> vwOrderPkPrintQueueVpsList_Film { get; set; }
        public virtual DbSet<vwOrderPkPrintQueueVpsList_Plate> vwOrderPkPrintQueueVpsList_Plate { get; set; }
        public virtual DbSet<vwOrderList> vwOrderList { get; set; }
        public virtual DbSet<vwInvoiceList_All> vwInvoiceList_All { get; set; }
        public virtual DbSet<vwPrintQueueVpsList_Ordered> vwPrintQueueVpsList_Ordered { get; set; }
        public virtual DbSet<vwInv5DetailsList> vwInv5DetailsList { get; set; }
        public virtual DbSet<UserAuth> UserAuth { get; set; }
        public virtual DbSet<UserNotification> UserNotification { get; set; }
        public virtual DbSet<vwClientList> vwClientList { get; set; }
        public virtual DbSet<vwUserNotificationList> vwUserNotificationList { get; set; }
    }
}

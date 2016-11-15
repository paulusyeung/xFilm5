namespace xFilm5.Api.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class xFilm5ApiEdm : DbContext
    {
        public xFilm5ApiEdm()
            : base("name=xFilm5ApiEdm")
        {
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
        public virtual DbSet<OrderPkPrintQ> OrderPkPrintQ { get; set; }
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
        public virtual DbSet<UserAuth> UserAuth { get; set; }
        public virtual DbSet<UserNotification> UserNotification { get; set; }
        public virtual DbSet<UserPreference> UserPreference { get; set; }
        public virtual DbSet<X_Counter> X_Counter { get; set; }
        public virtual DbSet<Z_WebColor> Z_WebColor { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Acct_INDetails>()
                .Property(e => e.UnitAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Acct_INDetails>()
                .Property(e => e.Discount)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Acct_INDetails>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Acct_INMaster>()
                .Property(e => e.InvoiceAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Acct_INMaster>()
                .Property(e => e.PaidAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Acct_INMaster>()
                .HasMany(e => e.Acct_INDetails)
                .WithRequired(e => e.Acct_INMaster)
                .HasForeignKey(e => e.INMasterID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Acct_INMaster>()
                .HasMany(e => e.ReceiptHeader)
                .WithOptional(e => e.Acct_INMaster)
                .HasForeignKey(e => e.INMasterId);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.Acct_INMaster)
                .WithRequired(e => e.Client)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.Client_AddressBook)
                .WithRequired(e => e.Client)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.Client_User)
                .WithRequired(e => e.Client)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.ClientPricing)
                .WithRequired(e => e.Client)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.OrderHeader)
                .WithRequired(e => e.Client)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.PrintQueue)
                .WithRequired(e => e.Client)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Client>()
                .HasMany(e => e.ReceiptHeader)
                .WithRequired(e => e.Client)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Client_User>()
                .HasMany(e => e.OrderHeader)
                .WithOptional(e => e.Client_User)
                .HasForeignKey(e => e.UserID);

            modelBuilder.Entity<Client_User>()
                .HasMany(e => e.ReceiptHeader)
                .WithOptional(e => e.Client_User)
                .HasForeignKey(e => e.ClientUserId);

            modelBuilder.Entity<ClientPricing>()
                .Property(e => e.UnitPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Order_Details>()
                .Property(e => e.ArtworkAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Order_Internal>()
                .Property(e => e.ArtworkFeeAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<OrderHeader>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<OrderHeader>()
                .HasMany(e => e.Acct_INMaster)
                .WithOptional(e => e.OrderHeader)
                .HasForeignKey(e => e.OrderID);

            modelBuilder.Entity<OrderHeader>()
                .HasMany(e => e.Order_Details)
                .WithRequired(e => e.OrderHeader)
                .HasForeignKey(e => e.OrderID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderHeader>()
                .HasMany(e => e.Order_Internal)
                .WithRequired(e => e.OrderHeader)
                .HasForeignKey(e => e.OrderID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderHeader>()
                .HasMany(e => e.Order_Journal)
                .WithRequired(e => e.OrderHeader)
                .HasForeignKey(e => e.OrderID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderHeader>()
                .HasMany(e => e.OrderComment)
                .WithOptional(e => e.OrderHeader)
                .HasForeignKey(e => e.OrderID);

            modelBuilder.Entity<OrderHeader>()
                .HasMany(e => e.OrderPkPrintQ)
                .WithRequired(e => e.OrderHeader)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrderHeader>()
                .HasMany(e => e.PrintQueue)
                .WithOptional(e => e.OrderHeader)
                .HasForeignKey(e => e.OrderID);

            modelBuilder.Entity<OrderPkPrintQ>()
                .Property(e => e.Qty)
                .HasPrecision(18, 4);

            modelBuilder.Entity<PosTx>()
                .Property(e => e.TxAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PrintQueue>()
                .HasMany(e => e.OrderPkPrintQ)
                .WithRequired(e => e.PrintQueue)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PrintQueue>()
                .HasMany(e => e.PrintQueue_VPS)
                .WithRequired(e => e.PrintQueue)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PrintQueue>()
                .HasMany(e => e.PrintQueue_LifeCycle)
                .WithRequired(e => e.PrintQueue)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ReceiptDetail>()
                .Property(e => e.UnitAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ReceiptDetail>()
                .Property(e => e.Discount)
                .HasPrecision(5, 2);

            modelBuilder.Entity<ReceiptDetail>()
                .Property(e => e.Amount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ReceiptHeader>()
                .Property(e => e.ReceiptAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ReceiptHeader>()
                .Property(e => e.PaidAmount)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ReceiptHeader>()
                .HasMany(e => e.ReceiptDetail)
                .WithRequired(e => e.ReceiptHeader)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<T_BillingCode_Dept>()
                .HasMany(e => e.T_BillingCode_Item)
                .WithOptional(e => e.T_BillingCode_Dept)
                .HasForeignKey(e => e.DeptID);

            modelBuilder.Entity<T_BillingCode_Item>()
                .Property(e => e.UnitPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<T_BillingCode_Item>()
                .HasMany(e => e.ClientPricing)
                .WithRequired(e => e.T_BillingCode_Item)
                .HasForeignKey(e => e.ItemId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<T_DeliveryMethod>()
                .HasMany(e => e.Order_Details)
                .WithOptional(e => e.T_DeliveryMethod)
                .HasForeignKey(e => e.DeliveryMethod);

            modelBuilder.Entity<T_JobTitle>()
                .HasMany(e => e.T_Workflow)
                .WithOptional(e => e.T_JobTitle)
                .HasForeignKey(e => e.JobTitle);

            modelBuilder.Entity<T_Media>()
                .HasMany(e => e.Order_Details)
                .WithOptional(e => e.T_Media)
                .HasForeignKey(e => e.Media);

            modelBuilder.Entity<T_PaperSize>()
                .HasMany(e => e.Order_Details)
                .WithOptional(e => e.T_PaperSize)
                .HasForeignKey(e => e.SizeID);

            modelBuilder.Entity<T_PaymentType>()
                .HasMany(e => e.Acct_INMaster)
                .WithOptional(e => e.T_PaymentType)
                .HasForeignKey(e => e.PaymentType);

            modelBuilder.Entity<T_PaymentType>()
                .HasMany(e => e.ReceiptHeader)
                .WithOptional(e => e.T_PaymentType)
                .HasForeignKey(e => e.PaymentType);

            modelBuilder.Entity<T_Platform>()
                .HasMany(e => e.Order_Details)
                .WithOptional(e => e.T_Platform)
                .HasForeignKey(e => e.Platform);

            modelBuilder.Entity<T_Priority>()
                .HasMany(e => e.OrderHeader)
                .WithOptional(e => e.T_Priority)
                .HasForeignKey(e => e.Priority);

            modelBuilder.Entity<T_Service>()
                .HasMany(e => e.OrderHeader)
                .WithOptional(e => e.T_Service)
                .HasForeignKey(e => e.ServiceType);

            modelBuilder.Entity<T_Software>()
                .HasMany(e => e.T_SoftwareVersion)
                .WithRequired(e => e.T_Software)
                .HasForeignKey(e => e.SoftwareID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<T_Status>()
                .HasMany(e => e.OrderHeader)
                .WithOptional(e => e.T_Status)
                .HasForeignKey(e => e.Status);

            modelBuilder.Entity<T_Workflow>()
                .HasMany(e => e.Order_Journal)
                .WithOptional(e => e.T_Workflow)
                .HasForeignKey(e => e.Status);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserAuth)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserNotification)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserPreference)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}

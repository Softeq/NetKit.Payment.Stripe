﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Softeq.NetKit.Payments.Data.Models.Subscription;
using Softeq.NetKit.Payments.Data.Models.SubscriptionPlan;
using Softeq.NetKit.Payments.SQLRepository;
using System;

namespace Softeq.NetKit.Payments.SQLRepository.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180419125013_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Softeq.NetKit.Payments.Data.Models.BillingAddress.BillingAddress", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AddressLine1");

                    b.Property<string>("AddressLine2");

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<string>("Name");

                    b.Property<string>("State");

                    b.Property<string>("ZipCode");

                    b.HasKey("Id");

                    b.ToTable("BillingAddress");
                });

            modelBuilder.Entity("Softeq.NetKit.Payments.Data.Models.Card.CreditCard", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CardCountry");

                    b.Property<int?>("ExpirationMonth")
                        .IsRequired();

                    b.Property<int?>("ExpirationYear")
                        .IsRequired();

                    b.Property<string>("Fingerprint");

                    b.Property<string>("Last4");

                    b.Property<string>("Name");

                    b.Property<string>("SaasUserId");

                    b.Property<string>("StripeCustomerId");

                    b.Property<string>("StripeId");

                    b.Property<string>("Type");

                    b.Property<Guid?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("CreditCard");
                });

            modelBuilder.Entity("Softeq.NetKit.Payments.Data.Models.Charge.Charge", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("Amount");

                    b.Property<Guid>("CreditCardId");

                    b.Property<string>("Currency");

                    b.Property<DateTime?>("Date");

                    b.Property<string>("Description");

                    b.Property<string>("StripeId");

                    b.Property<string>("SaasUserId");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CreditCardId");

                    b.HasIndex("UserId");

                    b.ToTable("Charge");
                });

            modelBuilder.Entity("Softeq.NetKit.Payments.Data.Models.Invoice.Invoice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AmountDue");

                    b.Property<int?>("ApplicationFee");

                    b.Property<int?>("AttemptCount");

                    b.Property<bool?>("Attempted");

                    b.Property<Guid?>("BillingAddressId");

                    b.Property<bool?>("Closed");

                    b.Property<string>("Currency");

                    b.Property<string>("SaasUserId");

                    b.Property<Guid?>("CustomerId");

                    b.Property<DateTime?>("Date");

                    b.Property<string>("Description");

                    b.Property<int?>("EndingBalance");

                    b.Property<bool?>("Forgiven");

                    b.Property<DateTime?>("NextPaymentAttempt");

                    b.Property<bool?>("Paid");

                    b.Property<DateTime?>("PeriodEnd");

                    b.Property<DateTime?>("PeriodStart");

                    b.Property<string>("ReceiptNumber");

                    b.Property<int?>("StartingBalance");

                    b.Property<string>("StripeCustomerId")
                        .HasMaxLength(50);

                    b.Property<string>("StripeId")
                        .HasMaxLength(50);

                    b.Property<int?>("Subtotal");

                    b.Property<int?>("Tax");

                    b.Property<decimal?>("TaxPercent");

                    b.Property<int?>("Total");

                    b.HasKey("Id");

                    b.HasIndex("BillingAddressId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("StripeCustomerId")
                        .IsUnique()
                        .HasFilter("[StripeCustomerId] IS NOT NULL");

                    b.ToTable("Invoice");
                });

            modelBuilder.Entity("Softeq.NetKit.Payments.Data.Models.Invoice.InvoiceLineItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("Amount");

                    b.Property<string>("Currency");

                    b.Property<Guid?>("InvoiceId");

                    b.Property<Guid?>("PeriodId");

                    b.Property<Guid?>("PlanId");

                    b.Property<bool>("Proration");

                    b.Property<int?>("Quantity");

                    b.Property<string>("StripeLineItemId");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("PeriodId");

                    b.HasIndex("PlanId");

                    b.ToTable("InvoiceLineItem");
                });

            modelBuilder.Entity("Softeq.NetKit.Payments.Data.Models.Invoice.InvoicePeriod", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("End");

                    b.Property<DateTime?>("Start");

                    b.HasKey("Id");

                    b.ToTable("InvoicePeriod");
                });

            modelBuilder.Entity("Softeq.NetKit.Payments.Data.Models.Invoice.InvoicePlan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AmountInCents");

                    b.Property<DateTime?>("Created");

                    b.Property<string>("Currency");

                    b.Property<string>("Interval");

                    b.Property<int>("IntervalCount");

                    b.Property<string>("Name");

                    b.Property<string>("StripePlanId");

                    b.Property<int?>("TrialPeriodDays");

                    b.HasKey("Id");

                    b.ToTable("InvoicePlan");
                });

            modelBuilder.Entity("Softeq.NetKit.Payments.Data.Models.Subscription.Subscription", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CancelReason");

                    b.Property<DateTime?>("End");

                    b.Property<DateTime?>("Start");

                    b.Property<string>("Status");

                    b.Property<string>("StripeId")
                        .HasMaxLength(50);

                    b.Property<Guid>("SubscriptionPlanId");

                    b.Property<decimal>("TaxPercent");

                    b.Property<DateTime?>("TrialEnd");

                    b.Property<DateTime?>("TrialStart");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("SubscriptionPlanId");

                    b.HasIndex("UserId");

                    b.ToTable("Subscription");
                });

            modelBuilder.Entity("Softeq.NetKit.Payments.Data.Models.SubscriptionPlan.SubscriptionPlan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Currency");

                    b.Property<int>("Interval");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<double>("Price");

                    b.Property<int>("Status");

                    b.Property<string>("StripeId");

                    b.Property<int>("TrialPeriodInDays");

                    b.HasKey("Id");

                    b.ToTable("SubscriptionPlan");
                });

            modelBuilder.Entity("Softeq.NetKit.Payments.Data.Models.SubscriptionPlan.SubscriptionPlanProperty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Key");

                    b.Property<Guid>("SubscriptionPlanId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("SubscriptionPlanId");

                    b.ToTable("SubscriptionPlanProperty");
                });

            modelBuilder.Entity("Softeq.NetKit.Payments.Data.Models.User.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("BillingAddressId");

                    b.Property<bool>("Delinquent");

                    b.Property<string>("Email");

                    b.Property<string>("IPAddress");

                    b.Property<string>("IPAddressCountry");

                    b.Property<decimal>("LifetimeValue");

                    b.Property<string>("SaasUserId");

                    b.Property<string>("StripeCustomerId");

                    b.HasKey("Id");

                    b.HasIndex("BillingAddressId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Softeq.NetKit.Payments.Data.Models.Card.CreditCard", b =>
                {
                    b.HasOne("Softeq.NetKit.Payments.Data.Models.User.User")
                        .WithMany("CreditCards")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Softeq.NetKit.Payments.Data.Models.Charge.Charge", b =>
                {
                    b.HasOne("Softeq.NetKit.Payments.Data.Models.Card.CreditCard", "CreditCard")
                        .WithMany("Charges")
                        .HasForeignKey("CreditCardId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Softeq.NetKit.Payments.Data.Models.User.User", "User")
                        .WithMany("Charges")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Softeq.NetKit.Payments.Data.Models.Invoice.Invoice", b =>
                {
                    b.HasOne("Softeq.NetKit.Payments.Data.Models.BillingAddress.BillingAddress", "BillingAddress")
                        .WithMany()
                        .HasForeignKey("BillingAddressId");

                    b.HasOne("Softeq.NetKit.Payments.Data.Models.User.User", "Customer")
                        .WithMany("Invoices")
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("Softeq.NetKit.Payments.Data.Models.Invoice.InvoiceLineItem", b =>
                {
                    b.HasOne("Softeq.NetKit.Payments.Data.Models.Invoice.Invoice")
                        .WithMany("LineItems")
                        .HasForeignKey("InvoiceId");

                    b.HasOne("Softeq.NetKit.Payments.Data.Models.Invoice.InvoicePeriod", "Period")
                        .WithMany()
                        .HasForeignKey("PeriodId");

                    b.HasOne("Softeq.NetKit.Payments.Data.Models.Invoice.InvoicePlan", "Plan")
                        .WithMany()
                        .HasForeignKey("PlanId");
                });

            modelBuilder.Entity("Softeq.NetKit.Payments.Data.Models.Subscription.Subscription", b =>
                {
                    b.HasOne("Softeq.NetKit.Payments.Data.Models.SubscriptionPlan.SubscriptionPlan", "SubscriptionPlan")
                        .WithMany()
                        .HasForeignKey("SubscriptionPlanId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Softeq.NetKit.Payments.Data.Models.User.User", "User")
                        .WithMany("Subscriptions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Softeq.NetKit.Payments.Data.Models.SubscriptionPlan.SubscriptionPlanProperty", b =>
                {
                    b.HasOne("Softeq.NetKit.Payments.Data.Models.SubscriptionPlan.SubscriptionPlan", "SubscriptionPlan")
                        .WithMany("Properties")
                        .HasForeignKey("SubscriptionPlanId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Softeq.NetKit.Payments.Data.Models.User.User", b =>
                {
                    b.HasOne("Softeq.NetKit.Payments.Data.Models.BillingAddress.BillingAddress", "BillingAddress")
                        .WithMany()
                        .HasForeignKey("BillingAddressId");
                });
#pragma warning restore 612, 618
        }
    }
}

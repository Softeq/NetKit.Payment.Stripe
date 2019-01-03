// Developed by Softeq Development Corporation
// http://www.softeq.com

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Softeq.NetKit.Payments.SQLRepository.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BillingAddress",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AddressLine1 = table.Column<string>(nullable: true),
                    AddressLine2 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingAddress", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvoicePeriod",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    End = table.Column<DateTime>(nullable: true),
                    Start = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicePeriod", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvoicePlan",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AmountInCents = table.Column<int>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    Currency = table.Column<string>(nullable: true),
                    Interval = table.Column<string>(nullable: true),
                    IntervalCount = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    StripePlanId = table.Column<string>(nullable: true),
                    TrialPeriodDays = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoicePlan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPlan",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Currency = table.Column<string>(nullable: true),
                    Interval = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    StripeId = table.Column<string>(nullable: true),
                    TrialPeriodInDays = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BillingAddressId = table.Column<Guid>(nullable: true),
                    Delinquent = table.Column<bool>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    IPAddress = table.Column<string>(nullable: true),
                    IPAddressCountry = table.Column<string>(nullable: true),
                    LifetimeValue = table.Column<decimal>(nullable: false),
                    SaasUserId = table.Column<string>(nullable: true),
                    StripeCustomerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_BillingAddress_BillingAddressId",
                        column: x => x.BillingAddressId,
                        principalTable: "BillingAddress",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPlanProperty",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Key = table.Column<string>(nullable: true),
                    SubscriptionPlanId = table.Column<Guid>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlanProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubscriptionPlanProperty_SubscriptionPlan_SubscriptionPlanId",
                        column: x => x.SubscriptionPlanId,
                        principalTable: "SubscriptionPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CreditCard",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CardCountry = table.Column<string>(nullable: true),
                    ExpirationMonth = table.Column<int>(nullable: false),
                    ExpirationYear = table.Column<int>(nullable: false),
                    Fingerprint = table.Column<string>(nullable: true),
                    Last4 = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    SaasUserId = table.Column<string>(nullable: true),
                    StripeCustomerId = table.Column<string>(nullable: true),
                    StripeId = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CreditCard_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AmountDue = table.Column<int>(nullable: true),
                    ApplicationFee = table.Column<int>(nullable: true),
                    AttemptCount = table.Column<int>(nullable: true),
                    Attempted = table.Column<bool>(nullable: true),
                    BillingAddressId = table.Column<Guid>(nullable: true),
                    Closed = table.Column<bool>(nullable: true),
                    Currency = table.Column<string>(nullable: true),
                    CustomerId = table.Column<Guid>(nullable: true),
                    Date = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    EndingBalance = table.Column<int>(nullable: true),
                    Forgiven = table.Column<bool>(nullable: true),
                    NextPaymentAttempt = table.Column<DateTime>(nullable: true),
                    Paid = table.Column<bool>(nullable: true),
                    PeriodEnd = table.Column<DateTime>(nullable: true),
                    PeriodStart = table.Column<DateTime>(nullable: true),
                    ReceiptNumber = table.Column<string>(nullable: true),
                    SaasUserId = table.Column<string>(nullable: true),
                    StartingBalance = table.Column<int>(nullable: true),
                    StripeCustomerId = table.Column<string>(maxLength: 50, nullable: true),
                    StripeId = table.Column<string>(maxLength: 50, nullable: true),
                    Subtotal = table.Column<int>(nullable: true),
                    Tax = table.Column<int>(nullable: true),
                    TaxPercent = table.Column<decimal>(nullable: true),
                    Total = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoice_BillingAddress_BillingAddressId",
                        column: x => x.BillingAddressId,
                        principalTable: "BillingAddress",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoice_User_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subscription",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CancelReason = table.Column<string>(nullable: true),
                    End = table.Column<DateTime>(nullable: true),
                    Start = table.Column<DateTime>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    StripeId = table.Column<string>(maxLength: 50, nullable: true),
                    SubscriptionPlanId = table.Column<Guid>(nullable: false),
                    TaxPercent = table.Column<decimal>(nullable: false),
                    TrialEnd = table.Column<DateTime>(nullable: true),
                    TrialStart = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subscription_SubscriptionPlan_SubscriptionPlanId",
                        column: x => x.SubscriptionPlanId,
                        principalTable: "SubscriptionPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Subscription_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Charge",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Amount = table.Column<int>(nullable: true),
                    CreditCardId = table.Column<Guid>(nullable: false),
                    Currency = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    StripeId = table.Column<string>(nullable: true),
                    SaasUserId = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Charge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Charge_CreditCard_CreditCardId",
                        column: x => x.CreditCardId,
                        principalTable: "CreditCard",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Charge_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceLineItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Amount = table.Column<int>(nullable: true),
                    Currency = table.Column<string>(nullable: true),
                    InvoiceId = table.Column<Guid>(nullable: true),
                    PeriodId = table.Column<Guid>(nullable: true),
                    PlanId = table.Column<Guid>(nullable: true),
                    Proration = table.Column<bool>(nullable: false),
                    Quantity = table.Column<int>(nullable: true),
                    StripeLineItemId = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceLineItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InvoiceLineItem_Invoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvoiceLineItem_InvoicePeriod_PeriodId",
                        column: x => x.PeriodId,
                        principalTable: "InvoicePeriod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvoiceLineItem_InvoicePlan_PlanId",
                        column: x => x.PlanId,
                        principalTable: "InvoicePlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Charge_CreditCardId",
                table: "Charge",
                column: "CreditCardId");

            migrationBuilder.CreateIndex(
                name: "IX_Charge_UserId",
                table: "Charge",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCard_UserId",
                table: "CreditCard",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_BillingAddressId",
                table: "Invoice",
                column: "BillingAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_CustomerId",
                table: "Invoice",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_StripeCustomerId",
                table: "Invoice",
                column: "StripeCustomerId",
                unique: true,
                filter: "[StripeCustomerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLineItem_InvoiceId",
                table: "InvoiceLineItem",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLineItem_PeriodId",
                table: "InvoiceLineItem",
                column: "PeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceLineItem_PlanId",
                table: "InvoiceLineItem",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_SubscriptionPlanId",
                table: "Subscription",
                column: "SubscriptionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Subscription_UserId",
                table: "Subscription",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionPlanProperty_SubscriptionPlanId",
                table: "SubscriptionPlanProperty",
                column: "SubscriptionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_User_BillingAddressId",
                table: "User",
                column: "BillingAddressId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Charge");

            migrationBuilder.DropTable(
                name: "InvoiceLineItem");

            migrationBuilder.DropTable(
                name: "Subscription");

            migrationBuilder.DropTable(
                name: "SubscriptionPlanProperty");

            migrationBuilder.DropTable(
                name: "CreditCard");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "InvoicePeriod");

            migrationBuilder.DropTable(
                name: "InvoicePlan");

            migrationBuilder.DropTable(
                name: "SubscriptionPlan");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "BillingAddress");
        }
    }
}
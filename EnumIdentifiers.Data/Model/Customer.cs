namespace EnumIdentifiers.Data.Model
{
    public class Customer
    {
        public enum BillingType
        {
            CreditCard,
            Invoice,
            ISP
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public BillingType Billing { get; set; }
        public SubscriptionLevel.Level SubscriptionLevel { get; set; }
        public SubscriptionLevel SubscriptionLevelRelation { get; set; }
    }
}
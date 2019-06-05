using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicesApp.Website.Messages
{
    public static class ManageMessage
    {
        public const string ChangePasswordSuccess = "Your password has been changed.";
        public const string SetPasswordSuccess = "Your password has been set.";
        public const string UpdateProfileSuccess = "Your profile has been updated.";
        public const string AddServiceRelationSuccess = "Your service relation has been updated.";
        public const string NullErrorServiceProviderProfile = "You can't provide services now. At first you must add an info to your profile.";
        public const string CreateOrderSuccess = "Your order has been created.";
        public const string ConfirmOrderSuccess = "You confirmed order fulfillment.";
        public const string NullErrorCustomerProfile = "You can't create orders now. At first you must add an info to your profile.";
        public const string NoCategoryError = "You must add at least one category.";
        public const string Error = "An error has occurred.";
    }
}
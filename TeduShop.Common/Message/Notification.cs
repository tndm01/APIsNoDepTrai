using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeduShop.Common.Message
{
    public class Notification
    {
        /// <summary>
        /// Notification User
        /// </summary>
        public static string CREATE_USER = "ADD NEW USER";
        public static string UPDATE_USER = "UPDATE USER";
        public static string DELETE_USER = "DELETE USER";

        /// <summary>
        /// Notification Role
        /// </summary>
        public static string CREATE_ROLE = "ADD NEW ROLE";
        public static string UPDATE_ROLE = "UPDATE ROLE";
        public static string DELETE_ROLE = "DELETE ROLE";

        /// <summary>
        /// Notification Product
        /// </summary>
        public static string CREATE_PRODUCT = "ADD NEW PRODUCT";
        public static string UPDATE_PRODUCT = "UPDATE PRODUCT";
        public static string DELETE_PRODUCT = "DELETE PRODUCT";

        /// <summary>
        /// Notification ProductCategory
        /// </summary>
        public static string CREATE_PRODUCTCATEGORY = "ADD NEW PRODUCTCATEGORY";
        public static string UPDATE_PRODUCTCATEGORY = "UPDATE PRODUCTCATEGORY";
        public static string DELETE_PRODUCTCATEGORY = "DELETE PRODUCTCATEGORY";

        /// <summary>
        /// Notification Function
        /// </summary>
        public static string CREATE_FUNCTION = "ADD NEW FUNCTION";
        public static string UPDATE_FUNCTION = "UPDATE FUNCTION";
        public static string DELETE_FUNCTION = "DELETE FUNCTION";

        /// <summary>
        /// Notification Product Image
        /// </summary>
        public static string CREATE_PRODUCTIMG = "ADD NEW PRODUCT IMAGE";
        public static string UPDATE_PRODUCTIMG = "UPDATE PRODUCT IMAGE";
        public static string DELETE_PRODUCTIMG = "DELETE PRODUCT IMAGE";

        /// <summary>
        /// Notification Product Quantity
        /// </summary>
        public static string CREATE_PRODUCTQUANTITY = "ADD NEW PRODUCT QUANTITY";
        public static string UPDATE_PRODUCTQUANTITY = "UPDATE PRODUCT QUANTITY";
        public static string DELETE_PRODUCTQUANTITY = "DELETE PRODUCT QUANTITY";
    }
}

﻿using System;
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
        public static string CREATE_USER = "Thêm mới người dùng";
        public static string UPDATE_USER = "Chỉnh sửa người dùng";
        public static string DELETE_USER = "Xóa người dùng";

        /// <summary>
        /// Notification Role
        /// </summary>
        public static string CREATE_ROLE = "Thêm mới quyền";
        public static string UPDATE_ROLE = "Chỉnh sửa quyền";
        public static string DELETE_ROLE = "Xóa quyền";

        /// <summary>
        /// Notification Product
        /// </summary>
        public static string CREATE_PRODUCT = "Thêm mới sản phẩm";
        public static string UPDATE_PRODUCT = "Chỉnh sửa sản phẩm";
        public static string DELETE_PRODUCT = "Xóa sản phẩm";

        /// <summary>
        /// Notification ProductCategory
        /// </summary>
        public static string CREATE_PRODUCTCATEGORY = "Thêm mới danh mục";
        public static string UPDATE_PRODUCTCATEGORY = "Chỉnh sửa danh mục";
        public static string DELETE_PRODUCTCATEGORY = "Xóa danh mục";

        /// <summary>
        /// Notification Function
        /// </summary>
        public static string CREATE_FUNCTION = "Thêm mới chức năng";
        public static string UPDATE_FUNCTION = "Chỉnh sửa chức năng";
        public static string DELETE_FUNCTION = "Xóa chức năng";

        /// <summary>
        /// Notification Product Image
        /// </summary>
        public static string CREATE_PRODUCTIMG = "Thêm mới hỉnh ảnh sản phầm";
        public static string UPDATE_PRODUCTIMG = "Chỉnh sửa hình ảnh sản phẩm";
        public static string DELETE_PRODUCTIMG = "Xóa hình ảnh sản phẩm";

        /// <summary>
        /// Notification Product Quantity
        /// </summary>
        public static string CREATE_PRODUCTQUANTITY = "Thêm mới số lượng sản phẩm";
        public static string UPDATE_PRODUCTQUANTITY = "Chỉnh sửa số lượng sản phẩm";
        public static string DELETE_PRODUCTQUANTITY = "Xóa số lượng sản phẩm";

        /// <summary>
        /// Notification Color
        /// </summary>
        public static string CREATE_COLOR = "Thêm mới màu sắc";
        public static string UPDATE_COLOR = "Chỉnh sửa màu sắc";
        public static string DELETE_COLOR = "Xóa màu sắc";

        /// <summary>
        /// Notification Size
        /// </summary>
        public static string CREATE_SIZE = "Thêm mới Size";
        public static string UPDATE_SIZE = "Chỉnh sửa Size";
        public static string DELETE_SIZE = "Xóa màu Size";

        /// <summary>
        /// Notification Dvt
        /// </summary>
        public static string CREATE_DVT = "Thêm mới Dvt";
        public static string UPDATE_DVT = "Chỉnh sửa Dvt";
        public static string DELETE_DVT = "Xóa màu Dvt";

        /// <summary>
        /// Notification Supplier
        /// </summary>
        public static string CREATE_SUPPLIER = "Thêm mới nhà cung cấp";
        public static string UPDATE_SUPPLIER = "Chỉnh sửa nhà cung cấp";
        public static string DELETE_SUPPLIER = "Xóa màu nhà cung cấp";

        /// <summary>
        /// Notification Supplier
        /// </summary>
        public static string CREATE_IMPORT = "Thêm mới phiếu nhập";
        public static string UPDATE_IMPORT = "Chỉnh sửa phiếu nhập";
        public static string DELETE_IMPORT = "Xóa số phiếu nhập";
    }
}

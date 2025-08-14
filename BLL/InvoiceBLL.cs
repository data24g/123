
using Store_X.DAL;
using Store_X.DTO;
using System;
using System.Collections.Generic;
using System.Data;

namespace Store_X.BLL
{
    public class InvoiceBLL
    {
        // Sử dụng mẫu Singleton
        private static InvoiceBLL _instance;
        public static InvoiceBLL Instance => _instance ?? (_instance = new InvoiceBLL());
        private InvoiceBLL() { }

        /// <summary>
        /// Xử lý logic nghiệp vụ và gọi DAL để tạo một hóa đơn mới.
        /// </summary>
        /// <param name="invoiceHeader">Thông tin chính của hóa đơn.</param>
        /// <param name="invoiceDetailsList">Danh sách các sản phẩm trong hóa đơn.</param>
        /// <returns>Trả về true nếu tạo thành công, ngược lại trả về false.</returns>
        public bool CreateInvoice(Invoice invoiceHeader, List<InvoiceDetail> invoiceDetailsList)
        {
            // --- Logic nghiệp vụ ---
            // Hóa đơn phải có ít nhất một sản phẩm và tổng tiền không được âm.
            if (invoiceHeader == null || invoiceDetailsList == null || invoiceDetailsList.Count == 0 || invoiceHeader.TotalAmount < 0)
            {
                return false;
            }

            // Gọi xuống lớp DAL. Lớp DAL sẽ trả về ID của hóa đơn mới (>0) nếu thành công,
            // hoặc trả về 0 nếu thất bại (ví dụ do transaction rollback vì hết hàng).
            int newInvoiceId = InvoiceDAL.Instance.CreateInvoice(invoiceHeader, invoiceDetailsList);

            // Chuyển đổi kết quả từ int sang bool.
            return newInvoiceId > 0;
        }

        /// <summary>
        /// Lấy danh sách các hóa đơn gần đây (ví dụ: 20 hóa đơn cuối cùng).
        /// </summary>
        public DataTable GetRecentInvoices()
        {
            return InvoiceDAL.Instance.GetRecentInvoices();
        }

        /// <summary>
        /// Lấy danh sách hóa đơn trong một khoảng thời gian cụ thể.
        /// </summary>
        public DataTable GetInvoicesByDateRange(DateTime fromDate, DateTime toDate)
        {
            // Logic nghiệp vụ: Ngày bắt đầu không thể sau ngày kết thúc.
            if (fromDate > toDate) return null;

            return InvoiceDAL.Instance.GetInvoicesByDateRange(fromDate, toDate);
        }

        /// <summary>
        /// Lấy thông tin chính (header) của một hóa đơn bằng ID.
        /// </summary>
        public DataRow GetInvoiceHeaderById(int invoiceId)
        {
            if (invoiceId <= 0) return null;
            DataTable dt = InvoiceDAL.Instance.GetInvoiceHeaderById(invoiceId);
            return (dt != null && dt.Rows.Count > 0) ? dt.Rows[0] : null;
        }

        /// <summary>
        /// Lấy danh sách chi tiết (các sản phẩm) của một hóa đơn bằng ID.
        /// </summary>
        public DataTable GetInvoiceDetailsById(int invoiceId)
        {
            if (invoiceId <= 0) return null;
            return InvoiceDAL.Instance.GetInvoiceDetailsById(invoiceId);
        }

        /// <summary>
        /// Lấy danh sách hóa đơn của một khách hàng cụ thể.
        /// </summary>
        public DataTable GetInvoicesByCustomerId(int customerId)
        {
            if (customerId <= 0) return null;
            return InvoiceDAL.Instance.GetInvoicesByCustomerId(customerId);
        }

        /// <summary>
        /// Lấy dữ liệu thống kê (tổng số hóa đơn, tổng doanh thu) trong một ngày.
        /// </summary>
        public DataRow GetDailyStatistics(DateTime date)
        {
            DataTable dt = InvoiceDAL.Instance.GetDailyStatistics(date);
            return (dt != null && dt.Rows.Count > 0) ? dt.Rows[0] : null;
        }
    }
}
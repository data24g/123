
using Store_X.DAL;
using Store_X.DTO;
using System.Data;

namespace Store_X.BLL
{
    public class SupplierBLL
    {
        // Sử dụng mẫu Singleton
        private static SupplierBLL _instance;
        public static SupplierBLL Instance => _instance ?? (_instance = new SupplierBLL());
        private SupplierBLL() { }

        /// <summary>
        /// Lấy về một DataTable chứa thông tin của tất cả các nhà cung cấp.
        /// </summary>
        public DataTable GetAllSuppliers()
        {
            return SupplierDAL.Instance.GetAllSuppliers();
        }

        /// <summary>
        /// Xử lý logic nghiệp vụ và gọi DAL để thêm một nhà cung cấp mới, sau đó lấy về ID của nó.
        /// Chức năng này phục vụ cho việc người dùng tự gõ tên nhà cung cấp mới vào ComboBox.
        /// </summary>
        /// <param name="supplierName">Tên nhà cung cấp mới cần thêm.</param>
        /// <returns>ID của nhà cung cấp vừa được tạo. Trả về 0 nếu thất bại.</returns>
        public int AddSupplierAndGetId(string supplierName)
        {
            // --- Logic nghiệp vụ ---
            // Tên nhà cung cấp không được rỗng.
            if (string.IsNullOrWhiteSpace(supplierName))
            {
                return 0;
            }
            return SupplierDAL.Instance.AddSupplierAndGetId(supplierName.Trim());
        }

        /// <summary>
        /// Xử lý logic nghiệp vụ để thêm một nhà cung cấp mới.
        /// </summary>
        public bool AddSupplier(Supplier supplier)
        {
            // --- Logic nghiệp vụ ---
            // Tên nhà cung cấp là bắt buộc.
            if (supplier == null || string.IsNullOrWhiteSpace(supplier.SupplierName))
            {
                return false;
            }
            return SupplierDAL.Instance.AddSupplier(supplier);
        }

        /// <summary>
        /// Xử lý logic nghiệp vụ để cập nhật thông tin một nhà cung cấp.
        /// </summary>
        public bool UpdateSupplier(Supplier supplier)
        {
            // --- Logic nghiệp vụ ---
            // Phải có SupplierID và tên không được rỗng.
            if (supplier == null || supplier.SupplierID <= 0 || string.IsNullOrWhiteSpace(supplier.SupplierName))
            {
                return false;
            }
            return SupplierDAL.Instance.UpdateSupplier(supplier);
        }

        /// <summary>
        /// Xử lý logic nghiệp vụ để xóa một nhà cung cấp.
        /// </summary>
        public bool DeleteSupplier(int supplierId)
        {
            if (supplierId <= 0)
            {
                return false;
            }
            // Logic kiểm tra ràng buộc (ví dụ: NCC có đang cung cấp sản phẩm nào không)
            // được xử lý bởi CSDL. BLL chỉ cần gọi DAL.
            return SupplierDAL.Instance.DeleteSupplier(supplierId);
        }
    }
}
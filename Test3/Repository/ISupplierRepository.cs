using Test3.Models;
using System.Collections.Generic;

namespace Test3.Repository
{
    interface ISupplierRepository
    {
        IEnumerable<Supplier> GetSupplier();
        Supplier GetSupplierByID(int supId);
        void InsertSupplier(Supplier supplier);
        void DeleteSupplier(int supplierID);
        void UpdateSupplier(Supplier supplier);
        void Save();

    }
}

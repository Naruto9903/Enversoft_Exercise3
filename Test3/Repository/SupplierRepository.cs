using Test3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace Test3.Repository
{
    public class SupplierRepository : ISupplierRepository
    {
        private db_SuppliersEntities _context;
        public SupplierRepository(db_SuppliersEntities SupplierContext)
        {
            this._context = SupplierContext;
        }
        public IEnumerable<Supplier> GetSupplier()
        {
            return _context.Suppliers.ToList();
        }
        public Supplier GetSupplierByID(int id)
        {
            return _context.Suppliers.Find(id);
        }
        public void InsertSupplier(Supplier supplier)
        {
            Random random = new Random();
            supplier.Code = random.Next();
            _context.Suppliers.Add(supplier);
        }
        public void DeleteSupplier(int SupplierID)
        {
            Supplier Supplier = _context.Suppliers.Find(SupplierID);
            _context.Suppliers.Remove(Supplier);
        }
        public void UpdateSupplier(Supplier Supplier)
        {
            _context.Entry(Supplier).State = EntityState.Modified;
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
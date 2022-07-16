using System.Data;
using System.Linq;
using System.Web.Mvc;
using Test3.Models;
using Test3.Repository;

namespace Test3.Controllers
{
    public class HomeController : Controller
    {
        private ISupplierRepository isup;
        public HomeController()
        {
            this.isup = new SupplierRepository(new db_SuppliersEntities());
        }

        db_SuppliersEntities db = new db_SuppliersEntities();
        public ActionResult Index(string searching)
        {
            return View(db.Suppliers.Where(x => x.SupplierName.Contains(searching) || searching == null).ToList());
        }

        public ActionResult Suppliers()
        {
            var suplist = isup.GetSupplier().ToList();
            return View(suplist);
        }

        public ActionResult Details(int id)
        {
            Supplier supplier = isup.GetSupplierByID(id);
            return View(supplier);
        }

        // GET: Supplier/Create
        public ActionResult Create()
        {
            return View(new Supplier());
        }
        [HttpPost]
        public ActionResult Create(Supplier Supplier)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    isup.InsertSupplier(Supplier);
                    isup.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again.");
            }
            return View(Supplier);
        }

        // GET: Supplier/Edit/5
        public ActionResult Edit(int id)
        {
            Supplier supplier = isup.GetSupplierByID(id);
            return View(supplier);
        }
        [HttpPost]
        public ActionResult Edit(Supplier supplier)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    isup.UpdateSupplier(supplier);
                    isup.Save();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again.");
            }
            return View(supplier);
        }

        // GET: Supplier/Delete/5
        public ActionResult Delete(int id, bool? saveChangesError)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Unable to save changes. Try again.";
            }
            Supplier supplier = isup.GetSupplierByID(id);
            return View(supplier);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Supplier supplier = isup.GetSupplierByID(id);
                isup.DeleteSupplier(id);
                isup.Save();
            }
            catch (DataException)
            {
                return RedirectToAction("Delete",
                   new System.Web.Routing.RouteValueDictionary {
        { "id", id },
        { "saveChangesError", true } });
            }
            return RedirectToAction("Index");
        }
    }
}

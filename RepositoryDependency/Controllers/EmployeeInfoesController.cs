using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RepositoryDependency.Models;
using RepositoryDependency.Repository;

namespace RepositoryDependency.Controllers
{
    public class EmployeeInfoesController : Controller
    {
        //Property of the type IRepository <TEnt, in TPk>
        public readonly IRepository<EmployeeInfo, int> _repository;

        //The Dependency Injection of the IRepository<TEnt, in TPk>
        public EmployeeInfoesController(IRepository<EmployeeInfo, int> repo)
        {
            _repository = repo;
        }

        // GET: EmployeeInfoes
        public ActionResult Index()
        {
            return View(_repository.Get());
        }

        // GET: EmployeeInfoes/Details/5
        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeInfo employeeInfo = _repository.Get(id);
            if (employeeInfo == null)
            {
                return HttpNotFound();
            }
            return View(employeeInfo);
        }

        // GET: EmployeeInfoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmpNo,EmpName,Salary,DeptName,Designation")] EmployeeInfo employeeInfo)
        {
            if (ModelState.IsValid)
            {
                _repository.Add(employeeInfo);
                return RedirectToAction("Index");
            }

            return View(employeeInfo);
        }

        // GET: EmployeeInfoes/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeInfo employeeInfo = _repository.Get(id);
            if (employeeInfo == null)
            {
                return HttpNotFound();
            }
            return View(employeeInfo);
        }

        // POST: EmployeeInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmpNo,EmpName,Salary,DeptName,Designation")] EmployeeInfo employeeInfo)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(employeeInfo);

                return RedirectToAction("Index");
            }
            return View(employeeInfo);
        }

        // GET: EmployeeInfoes/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeInfo employeeInfo = _repository.Get(id);
            if (employeeInfo == null)
            {
                return HttpNotFound();
            }
            return View(employeeInfo);
        }

        // POST: EmployeeInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeInfo employeeInfo = _repository.Get(id);
            _repository.Remove(employeeInfo);
            return RedirectToAction("Index");
        }

       
    }
}

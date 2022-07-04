using InterviewTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterviewTest.Controllers
{
    /// <summary>
    /// Controller for the views that insert data to the database
    /// </summary>
    public class InsertController : Controller
    {
        /// <summary>
        /// Open the View to Insert a building
        /// </summary>
        /// <returns></returns>
        public ActionResult InsertBuilding()
        {
            return View();
        }

        /// <summary>
        /// Receive the new building object and if it's valid adds it to the data base 
        /// </summary>
        /// <param name="building"></param>
        /// <returns>the view</returns>
        [HttpPost]
        public ActionResult InsertBuilding([Bind(Include = "PKBuilding, Building1")]Building building)
        {
            if (ModelState.IsValid)
            {
                MaterialsEntities dataBaseEntity = new MaterialsEntities();
                dataBaseEntity.Buildings.Add(building);
                dataBaseEntity.SaveChanges();
                ViewBag.message = "Building added successfully";
            }
            return View();
        }

        /// <summary>
        /// Open the View to Insert a customer
        /// </summary>
        /// <returns>the view</returns>
        public ActionResult InsertCustomer()
        {
            MaterialsEntities dataBaseEntity = new MaterialsEntities();
            ViewBag.Buildings = dataBaseEntity.Buildings.ToList();
            return View();
        }

        /// <summary>
        /// Receive the new customer object and if it's valid adds it to the data base 
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>the view</returns>
        [HttpPost]
        public ActionResult InsertCustomer([Bind(Include = "PKCustomer, Customer1, Prefix, FKBuilding")] Customer customer)
        {
            MaterialsEntities dataBaseEntity = new MaterialsEntities();
            ViewBag.Buildings = dataBaseEntity.Buildings.ToList();

            if (ModelState.IsValid)
            {
                dataBaseEntity.Customers.Add(customer);
                dataBaseEntity.SaveChanges();
                ViewBag.message = "Customer added successfully";
            }
            return View();
        }

        /// <summary>
        /// Open the View to Insert a part number
        /// </summary>
        /// <returns></returns>
        public ActionResult InsertPartNumber()
        {
            MaterialsEntities dataBaseEntity = new MaterialsEntities();
            ViewBag.Customers = dataBaseEntity.Customers.ToList();
            return View();
        }

        /// <summary>
        /// Receive the new part number object and if it's valid adds it to the data base 
        /// </summary>
        /// <param name="partNumber"></param>
        /// <returns>view</returns>
        [HttpPost]
        public ActionResult InsertPartNumber([Bind(Include = "PKPartNumber, PartNumber1, FKCustomer, Available")] PartNumber partNumber)
        {
            MaterialsEntities dataBaseEntity = new MaterialsEntities();
            ViewBag.Customers = dataBaseEntity.Customers.ToList();
            if (ModelState.IsValid)
            {
                dataBaseEntity.PartNumbers.Add(partNumber);
                dataBaseEntity.SaveChanges();
                ViewBag.message = "Part Number added successfully";
            }
            return View();
        }
    }
}
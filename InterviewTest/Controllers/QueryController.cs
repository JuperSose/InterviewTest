using InterviewTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace InterviewTest.Controllers
{
    public class QueryController : Controller
    {
        /// <summary>
        /// Get the data to show in the view, sort it and send it to the view of to export to CSV
        /// </summary>
        /// <param name="search"></param>
        /// <param name="available"></param>
        /// <param name="notAvailable"></param>
        /// <param name="sortColumn"></param>
        /// <param name="iconClass"></param>
        /// <param name="exportCSV"></param>
        /// <returns>the view</returns>
        public ActionResult ShowData(string search = "", bool available = false, bool notAvailable = false, string sortColumn = "PartNumber", string iconClass = "fa-sort-asc", bool exportCSV = false)
        {
            MaterialsEntities dataBaseEntity = new MaterialsEntities();
            List<GetGridViewData_Result> dataList = dataBaseEntity.GetGridViewData().ToList();
            List<GetGridViewData_Result> dataSend = new List<GetGridViewData_Result>();
            ViewBag.search = search;
            ViewBag.available = available;
            ViewBag.notAvailable = notAvailable;
            ViewBag.sortColumn = sortColumn;
            ViewBag.iconClass = iconClass;

            foreach (var data in dataList)
            {
                if (data.PartNumber.ToLower().Contains(search.ToLower()) || data.Customer.ToLower().Contains(search.ToLower()))
                    if((available == true && data.Available == true) || (notAvailable == true && data.Available == false))
                        dataSend.Add(data);
            }
            if (ViewBag.sortColumn == "PartNumber")
            {
                if (ViewBag.iconClass == "fa-sort-asc")
                    dataSend = dataSend.OrderBy(temp => temp.PartNumber).ToList();
                else
                    dataSend = dataSend.OrderByDescending(temp => temp.PartNumber).ToList();
            }
            else if (ViewBag.sortColumn == "Customer")
            {
                if (ViewBag.iconClass == "fa-sort-asc")
                    dataSend = dataSend.OrderBy(temp => temp.Customer).ToList();
                else
                    dataSend = dataSend.OrderByDescending(temp => temp.Customer).ToList();
            }
            else if (ViewBag.sortColumn == "Building")
            {
                if (ViewBag.iconClass == "fa-sort-asc")
                    dataSend = dataSend.OrderBy(temp => temp.Building).ToList();
                else
                    dataSend = dataSend.OrderByDescending(temp => temp.Building).ToList();
            }
            else if (ViewBag.sortColumn == "Available")
            {
                if (ViewBag.iconClass == "fa-sort-asc")
                    dataSend = dataSend.OrderBy(temp => temp.Available).ToList();
                else
                    dataSend = dataSend.OrderByDescending(temp => temp.Available).ToList();
            }
            if (exportCSV)
                return ExportToCSV(dataSend);
            else
                return View(dataSend);
        }

        /// <summary>
        /// Get the data to export it to CSV
        /// </summary>
        /// <param name="getGridViewData_Results"></param>
        /// <returns>the CSV file</returns>
        public ActionResult ExportToCSV(List<GetGridViewData_Result> getGridViewData_Results)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("PartNumber,Customer,Building,Available");
            foreach (var d in getGridViewData_Results)
            {
                builder.AppendLine($"{d.PartNumber},{d.Customer},{d.Building},{d.Available}");
            }
            return File(Encoding.UTF8.GetBytes(builder.ToString()), "text/csv", "Data.csv");
        }
    }
}
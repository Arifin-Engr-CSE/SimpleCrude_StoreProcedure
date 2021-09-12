using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using SimpleCrude_StoreProcedure.Models;
using Dapper;
using Microsoft.AspNetCore.Hosting;
using AspNetCore.Reporting;

namespace SimpleCrude_StoreProcedure.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _wEnv;

       
        
        public HomeController(IWebHostEnvironment wEnv)
        {
            _wEnv = wEnv;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }
        public IActionResult Index()
        {
           
            return View(DapperORM.ReturnList<Student>("StudentViewALL",null));
        }

        //CreateAndEdit

        [HttpGet]
        public IActionResult CreateAndEdit(int id=0)
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAndEdit(Student student)
        {
            DynamicParameters param = new DynamicParameters();
            param.Add("@Id",student.Id);
            param.Add("@Name", student.Name);
            param.Add("@Gender", student.Gender);
            param.Add("@Address", student.Address);
            DapperORM.ExicuteWithoutReturn("StudentAddOrEdit", param);
            return RedirectToAction("Index");
        }

      

        //Report
        public IActionResult Print()
        {

            var dt = new DataTable();
            dt = GetAllStudent();
            string mimetype = "";
            int extension = 1;
            var path = $"{this._wEnv.WebRootPath}\\Reports\\Report.rdlc";
            string userName = "Arifin";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("prm_1", "RDLC Report");
            parameters.Add("CreatedByName", userName);
            LocalReport localReport = new LocalReport(path);
            localReport.AddDataSource("dsStudent",dt);

            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimetype);
            return File(result.MainStream, "application/pdf");
        }
        
        public DataTable GetAllStudent()
        {
            var dt = new DataTable();
            dt.Columns.Add("StudentId");
            dt.Columns.Add("StudentName");
            dt.Columns.Add("Gender");
            dt.Columns.Add("Address");
            List<Student> stList = DapperORM.ReturnList<Student>("StudentViewALL", null).ToList();
            DataRow row;
            foreach (Student student in stList)
            {
                row = dt.NewRow();
                row["StudentId"] = student.Id;
                row["StudentName"] = student.Name;
                row["Gender"] = student.Gender;
                row["Address"] = student.Address;

                dt.Rows.Add(row);
            }
            
            return dt;
        }
    }
}

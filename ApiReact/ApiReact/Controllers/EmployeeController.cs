using ApiReact.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiReact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _iconfiguration;
        //private readonly IWebHostEnvironment _iconfiguration;
        public EmployeeController(IConfiguration configuration)
        {
            _iconfiguration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string sql = @"select * from Employee";
            DataTable table = new DataTable();
            string sqlDataSource = _iconfiguration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection conn = new SqlConnection(sqlDataSource))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    myReader = command.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    conn.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Employee employee)
        {
            string sql = @"insert into Employee values (@EmployeeName)";
            DataTable table = new DataTable();
            string sqlDataSource = _iconfiguration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection conn = new SqlConnection(sqlDataSource))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("@EmployeeName", employee.EmployeeName);
                    command.Parameters.AddWithValue("@Department", employee.Department);
                    command.Parameters.AddWithValue("@DateOfJoining", employee.DateOfJoining);
                    command.Parameters.AddWithValue("@PhotoFileName", employee.PhotoFileName);
                    myReader = command.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    conn.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPut]
        public JsonResult Put(Employee employee)
        {
            string sql = @"update Department set EmployeeName = @EmployeeName,Department = @Department,
                            DateOfJoining = @DateOfJoining,PhotoFileName = @PhotoFileName where EmployeeID = @EmployeeID";
            DataTable table = new DataTable();
            string sqlDataSource = _iconfiguration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection conn = new SqlConnection(sqlDataSource))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("@EmployeeName", employee.EmployeeName);
                    command.Parameters.AddWithValue("@Department", employee.Department);
                    command.Parameters.AddWithValue("@DateOfJoining", employee.DateOfJoining);
                    command.Parameters.AddWithValue("@PhotoFileName", employee.PhotoFileName);
                    myReader = command.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    conn.Close();
                }
            }
            return new JsonResult(table);
        }
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string sql = @"delete from Employee  where EmployeeID = @EmployeeID";
            DataTable table = new DataTable();
            string sqlDataSource = _iconfiguration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection conn = new SqlConnection(sqlDataSource))
            {
                conn.Open();
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("@EmployeeID", id);
                    myReader = command.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    conn.Close();
                }
            }
            return new JsonResult(table);
        }

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string fileName = postedFile.FileName;
                var physicalPath = 
            }
            catch(Exception ex)
            {

            }
        }
    }
}

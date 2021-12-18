using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ApiReact.Models;

namespace ApiReact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _iconfiguration;
        public DepartmentController(IConfiguration configuration)
        {
            _iconfiguration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string sql = @"select * from Department";
            DataTable table = new DataTable();
            string sqlDataSource = _iconfiguration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using(SqlConnection conn = new SqlConnection(sqlDataSource))
            {
                conn.Open();
                using(SqlCommand command = new SqlCommand(sql, conn))
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
        public JsonResult Post(Department department)
        {
            string sql = @"insert into Department values (@DepartmentName)";
            DataTable table = new DataTable();
            string sqlDataSource = _iconfiguration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using(SqlConnection conn = new SqlConnection(sqlDataSource))
            {
                conn.Open();
                using(SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("@DepartmentName",department.DepartmentName);
                    myReader = command.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    conn.Close();
                }
            }
            return new JsonResult(table);
        }
        
        [HttpPut]
        public JsonResult Put(Department department)
        {
            string sql = @"update Department set DepartmentName = @DepartmentName where DepartmentID = @DepartmentID";
            DataTable table = new DataTable();
            string sqlDataSource = _iconfiguration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using(SqlConnection conn = new SqlConnection(sqlDataSource))
            {
                conn.Open();
                using(SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("@DepartmentID",department.DepartmentID);
                    command.Parameters.AddWithValue("@DepartmentName",department.DepartmentName);
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
            string sql = @"delete from Department  where DepartmentID = @DepartmentID";
            DataTable table = new DataTable();
            string sqlDataSource = _iconfiguration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using(SqlConnection conn = new SqlConnection(sqlDataSource))
            {
                conn.Open();
                using(SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("@DepartmentID", id);
                    myReader = command.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    conn.Close();
                }
            }
            return new JsonResult(table);
        }
    }
}

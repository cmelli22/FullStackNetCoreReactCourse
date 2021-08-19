using FullstackCourseReact.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FullstackCourseReact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select DeparmentId, DepartmentName from Department";
            DataTable dt = new DataTable();
            string conectionString = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(conectionString))
            {
                myCon.Open();
                using (SqlCommand myComand = new SqlCommand())
                {
                    myComand.CommandType = CommandType.Text;
                    myComand.CommandText = query;
                    myComand.Connection = myCon;
                    myReader = myComand.ExecuteReader();
                    dt.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(dt);
        }


        [HttpPost]
        public JsonResult Post(Department dep)
        {
            string query = @"insert into Department
                             values(@DepartmentName)";


            DataTable dt = new DataTable();
            string conectionString = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(conectionString))
            {
                myCon.Open();
                using (SqlCommand myComand = new SqlCommand())
                {
                    myComand.Parameters.AddWithValue("@DepartmentName", dep.DepartmentName);
                    myComand.CommandType = CommandType.Text;
                    myComand.CommandText = query;
                    myComand.Connection = myCon;
                    myReader = myComand.ExecuteReader();
                    dt.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Succesfully");
        }

        [HttpPut]
        public JsonResult Put(Department dep)
        {
            string query = @"update Department
                             set DepartmentName =@DepartmentName
                            where DeparmentId = @DepartmentId";
            DataTable dt = new DataTable();
            string conectionString = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(conectionString))
            {
                myCon.Open();
                using (SqlCommand myComand = new SqlCommand())
                {
                    myComand.Parameters.AddWithValue("@DepartmentId", dep.DepartmentId);
                    myComand.Parameters.AddWithValue("@DepartmentName", dep.DepartmentName);
                    myComand.CommandType = CommandType.Text;
                    myComand.CommandText = query;
                    myComand.Connection = myCon;
                    myReader = myComand.ExecuteReader();
                    dt.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("updated Succesfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"delete from Department
                            where DeparmentId = @DepartmentId";
            DataTable dt = new DataTable();
            string conectionString = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(conectionString))
            {
                myCon.Open();
                using (SqlCommand myComand = new SqlCommand())
                {
                    myComand.Parameters.AddWithValue("@DepartmentId", id);
                    myComand.CommandType = CommandType.Text;
                    myComand.CommandText = query;
                    myComand.Connection = myCon;
                    myReader = myComand.ExecuteReader();
                    dt.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("deleted Succesfully");
        }
    }
}

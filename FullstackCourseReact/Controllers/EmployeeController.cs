using FullstackCourseReact.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FullstackCourseReact.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public EmployeeController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select * from Employee";
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
        public JsonResult Post(Employee emp)
        {
            string query = @"insert into Employee
                             values(@EmployeeName,@Department,@DateOfJoining,@PhotoFileName)";


            DataTable dt = new DataTable();
            string conectionString = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(conectionString))
            {
                myCon.Open();
                using (SqlCommand myComand = new SqlCommand())
                {
                    myComand.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
                    myComand.Parameters.AddWithValue("@Department", emp.Department);
                    myComand.Parameters.AddWithValue("@DateOfJoining", emp.DateOfJoining);
                    myComand.Parameters.AddWithValue("@PhotoFileName", emp.PhotoFileName);
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
        public JsonResult Put(Employee emp)
        {
            string query = @"update Employee
                             set EmployeeName =@EmployeeName,
                                 Department =@Department,
                                 DateOfJoinng =@DateOfJoining,
                                 PhotoFileName =@PhotoFileName
                            where EmployeeId = @EmployeeId";
            DataTable dt = new DataTable();
            string conectionString = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(conectionString))
            {
                myCon.Open();
                using (SqlCommand myComand = new SqlCommand())
                {
                    myComand.Parameters.AddWithValue("@EmployeeId", emp.EmployeeId);
                    myComand.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
                    myComand.Parameters.AddWithValue("@Department", emp.Department);
                    myComand.Parameters.AddWithValue("@DateOfJoining", emp.DateOfJoining);
                    myComand.Parameters.AddWithValue("@PhotoFileName", emp.PhotoFileName);
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
            string query = @"delete from Employee
                            where EmployeeId = @EmployeeId";
            DataTable dt = new DataTable();
            string conectionString = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(conectionString))
            {
                myCon.Open();
                using (SqlCommand myComand = new SqlCommand())
                {
                    myComand.Parameters.AddWithValue("@EmployeeId", id);
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

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFiles()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _webHostEnvironment.ContentRootPath + "/Photos/" + filename;

                using (var stream = new FileStream(physicalPath,FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename + " Added");
            }
            catch (Exception)
            {

                return new JsonResult("anonymus.png");
            }
        }
    }
}

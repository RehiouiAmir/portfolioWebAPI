using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using portfolioWebAPI.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace portfolioWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public ProfileController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

    
        [HttpGet]

        public JsonResult Get()
        {
            string query = @"Select ProfileId, LastName, FirstName, Adress, ZipeCode, Birthday, Email, Phone, ImgUrl, CvUrl, Job from dbo.Profile";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PortfolioAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }


        [HttpGet("{profId}")]

        public JsonResult GetById(int profId)
        {
            string query = @"Select LastName, FirstName, Adress, ZipeCode, Birthday 
                                ,Email, Phone, ImgUrl, CvUrl, Job from dbo.Profile
                           where ProfileId = " + profId + @"";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PortfolioAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }


        [HttpPost]

        public JsonResult Post(Profile prof)
        {
            string query = @"insert into dbo.Profile 
                            values ('" + prof.FirstName + @"'
                            ,'" + prof.LastName + @"'
                            ,'" + prof.Adress + @"'
                            ,'" + prof.ZipeCode + @"'
                            ,'" + prof.Bitrhday + @"'
                            ,'" + prof.Email + @"'
                            ,'" + prof.Phone + @"'
                            ,'" + prof.ImgUrl + @"'
                            ,'" + prof.CvUrl + @"'
                            ,'" + prof.Job + @"')";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PortfolioAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Profile prof)
        {
            string query = @"
                    update dbo.Profile set 
                    LastName= '" + prof.LastName + @"'
                    ,FirstName= '" + prof.FirstName + @"'
                    ,Adress= '" + prof.Adress + @"'
                    ,ZipeCode= '" + prof.ZipeCode + @"'
                    ,Birthday= '" + prof.Bitrhday + @"'
                    ,Email= '" + prof.Email + @"'
                    ,Phone= '" + prof.Phone + @"'
                    ,ImgUrl= '" + prof.ImgUrl + @"'
                    ,CvUrl= '" + prof.CvUrl + @"'
                    ,Job= '" + prof.Job + @"'
                    where ProfileId = " + prof.ProfileId + @"";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PortfolioAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }


        [HttpDelete("{profId}")]

        public JsonResult Delete(int profId)
        {
            string query = @"delete from dbo.Profile where ProfileId=" + profId + @"";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PortfolioAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("deleted Successfully");
        }

        [Route("SaveFile")]
        [HttpPost]

        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);

            }
            catch (Exception)
            {
                return new JsonResult("anonymous.png");
            }
        }

    }
}


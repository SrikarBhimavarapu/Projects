using EmpRegisterForm.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmpRegisterForm.Controllers
{
    public class EmpDetailsController : Controller
    {
        // GET: EmpDetails
      
        public ActionResult Index()
        {
            Country();
          
            return View();
        }   
        public void Country()
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            string query = string.Format("Select * from Country");
            using (SqlConnection sc = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EmpData;Integrated Security=True;"))
            {
                sc.Open();
                using (SqlCommand cmd = new SqlCommand(query, sc))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lst.Add(new SelectListItem
                            {
                                Value = dr["CountryId"].ToString(),
                                Text = dr["Country Name"].ToString(),

                            });
                        }
                    }
                }
            }
            ViewBag.Country = lst;
        }
        [HttpGet]
        public JsonResult  States(string id)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            string query = string.Format("Select StateId,State from State where CountryId = @CountryId");
            using (SqlConnection sc = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EmpData;Integrated Security=True;"))
            {
                sc.Open();
                using (SqlCommand cmd = new SqlCommand(query, sc))
                {

                    cmd.Parameters.AddWithValue("@CountryId", id);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lst.Add(new SelectListItem
                            {
                               Value = dr["StateId"].ToString(),
                                Text = (string)dr["State"],
                            });
                        }
                    }
                }
            }
            //  string output = JsonConvert.SerializeObject(lst);
            //  return Json(output, JsonRequestBehavior.AllowGet);
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult City(string id)
        {
            List<SelectListItem> lst = new List<SelectListItem>();
            string query = string.Format("Select * from City where StateId = @StateId");
            using (SqlConnection sc = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EmpData;Integrated Security=True;"))
            {
                sc.Open();
                using (SqlCommand cmd = new SqlCommand(query, sc))
                {

                    cmd.Parameters.AddWithValue("@StateId", id);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lst.Add(new SelectListItem
                            {
                               Value = dr["CityId"].ToString(),
                               Text = dr["City"].ToString(),
                            });
                        }
                    }
                }
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
           
        }
       
        
        [HttpPost]
        public JsonResult CreateEmp(EmpDetail emp )
        {  
            string query = string.Format("Insert into EmpData values (@Id,@FirstName,@LastName,@Email,@Gender,@DateOfBirth,@Hobbies)");
            using (SqlConnection sc = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EmpData;Integrated Security=True;"))
            {
                sc.Open();
                using (SqlCommand cmd = new SqlCommand(query, sc))
                {
                    cmd.Parameters.AddWithValue("@Id", emp.Id);
                    cmd.Parameters.AddWithValue("@FirstName", emp.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", emp.LastName);
                    cmd.Parameters.AddWithValue("@Email", emp.Email);
                    cmd.Parameters.AddWithValue("@Gender", emp.Gender);
                    cmd.Parameters.AddWithValue("@DateOfBirth", emp.DateOfBirth);
                    cmd.Parameters.AddWithValue("@Hobbies", emp.hobbies);
                    cmd.ExecuteNonQuery();
                }
            }
            return Json(emp);
        }
        [HttpGet]
        public JsonResult GetDetails()
        {
            List<EmpDetail> lstemp = new List<EmpDetail>();
            string query = string.Format("Select Id,FirstName,LastName,Email,Gender,DateOfBirth,Hobbies from EmpData");
            using (SqlConnection sc = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EmpData;Integrated Security=True;"))
            {
                sc.Open();
                using (SqlCommand cmd = new SqlCommand(query, sc))
                {

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lstemp.Add(new EmpDetail
                            {
                                Id = dr["Id"].ToString(),
                                FirstName = dr["FirstName"].ToString(),
                                LastName = dr["LastName"].ToString(),
                                Email = dr["Email"].ToString(),
                                Gender = dr["Gender"].ToString(),
                                DateOfBirth = dr["DateOfBirth"].ToString(),
                                hobbies = dr["Hobbies"].ToString(),

                            }) ;
                        }
                    }
                }
            }
            string output = JsonConvert.SerializeObject(lstemp);
            return Json(output, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteEmp(EmpDetail emp)
        { 
            try
            {
                string query = string.Format("Delete from EmpData where @Id = Id");
                using (SqlConnection sc = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EmpData;Integrated Security=True;"))
                {
                    sc.Open();
                    using (SqlCommand cmd = new SqlCommand(query, sc))
                    {
                        cmd.Parameters.AddWithValue("@Id",emp.Id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Json(emp);
        }
        [HttpPost]
        public JsonResult EditEmp(EmpDetail emp)
        {

            string query = string.Format("Select FirstName,LastName,Email,Gender,DateOfBirth,Hobbies from EmpData where Id = @Id");
            using (SqlConnection sc = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EmpData;Integrated Security=True;"))
            {
                sc.Open();
                using (SqlCommand cmd = new SqlCommand(query, sc))
                {
                    cmd.Parameters.AddWithValue("@Id", emp.Id);
                   using(SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            emp.FirstName = dr["FirstName"].ToString();
                            emp.LastName = dr["LastName"].ToString();
                            emp.Email = dr["Email"].ToString();
                            emp.Gender = dr["Gender"].ToString();
                            emp.DateOfBirth = dr["DateOfBirth"].ToString();
                            emp.hobbies = dr["Hobbies"].ToString();
                        }
                    }
                }
            }
           // string Output = JsonConvert.SerializeObject(emp);
                return Json(emp,JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateEmp(EmpDetail emp)
        {
            string query = string.Format("Update EmpData set FirstName = @FirstName,LastName = @LastName,Email=@Email,Gender=@Gender,DateOfBirth=@DateOfBirth,Hobbies=@Hobbies where Id = @Id");
            using(SqlConnection sc = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EmpData;Integrated Security=True;"))
            {
                sc.Open();          
                    using (SqlCommand cmd = new SqlCommand(query, sc))
                    {
                        cmd.Parameters.AddWithValue("@Id", emp.Id);
                        cmd.Parameters.AddWithValue("@FirstName", emp.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", emp.LastName);
                        cmd.Parameters.AddWithValue("@Email", emp.Email);
                        cmd.Parameters.AddWithValue("@Gender", emp.Gender);
                        cmd.Parameters.AddWithValue("@DateOfBirth", emp.DateOfBirth);
                        cmd.Parameters.AddWithValue("@Hobbies", emp.hobbies);
                        cmd.ExecuteNonQuery();
                    }
            }
            return Json(emp);
        }
    }
}
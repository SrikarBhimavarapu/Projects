using SimpleAjaxCall.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimpleAjaxCall.Controllers
{
    public class EmployeeEntitiesController : Controller
    {
        // GET: EmployeeEntities
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult InsertData(EmployeeEntitie emp)
        {
           
            try
            {
                string query = string.Format("Insert into EmpDetails values(@EmpId,@EmpName,@EmpSalary)");
                using (SqlConnection sc = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EmpData;Integrated Security=True;"))
                {
                    using (SqlCommand cmd = new SqlCommand(query, sc))
                    {

                        sc.Open();
                        cmd.Parameters.AddWithValue("@EmpId", emp.EmpId);
                        cmd.Parameters.AddWithValue("@EmpName", emp.EmpName);
                        cmd.Parameters.AddWithValue("@EmpSalary", emp.EmpSalary);
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
        public JsonResult GetDetails()
        {
            List<EmployeeEntitie> lstemp = new List<EmployeeEntitie>();
            string query = string.Format("Select * from EmpDetails");
            using (SqlConnection sc = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EmpData;Integrated Security=True;"))
            {
                sc.Open();
                using (SqlCommand cmd = new SqlCommand(query, sc))
                {

                    using (SqlDataReader dr = cmd.ExecuteReader()) {
                        while (dr.Read())
                        {
                            lstemp.Add(new EmployeeEntitie
                            {
                                EmpId = int.Parse(dr["EmpId"].ToString()),
                               EmpName = dr["EmpName"].ToString(),
                                EmpSalary = decimal.Parse(dr["EmpSalary"].ToString()),

                            }); ;
                        }
                    }
                }
            }
            return Json(lstemp);
        }
        public JsonResult UpdateEmp(EmployeeEntitie emp)
        {
            try
            {
                string query = string.Format("Update EmpDetails Set EmpName = @EmpName,EmpSalary = @EmpSalary where EmpId = @EmpId");
                using (SqlConnection sc = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EmpData;Integrated Security=True;"))
                {
                    sc.Open();
                    using (SqlCommand cmd = new SqlCommand(query, sc))
                    {

                        cmd.Parameters.AddWithValue("@EmpId", emp.EmpId);
                        cmd.Parameters.AddWithValue("@EmpName", emp.EmpName);
                        cmd.Parameters.AddWithValue("@EmpSalary", emp.EmpSalary);
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
        public JsonResult DeleteEmp(EmployeeEntitie emp)
        {
            try
            {
                string query = string.Format("Delete from EmpDetails where EmpId = @EmpId");
                using (SqlConnection sc = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EmpData;Integrated Security=True;"))
                {
                    sc.Open();
                    using (SqlCommand cmd = new SqlCommand(query, sc))
                    {
                        cmd.Parameters.AddWithValue("@EmpId",emp.EmpId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Json(emp);
        }
    }
}
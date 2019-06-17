using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Assignment.Models
{
    public class UserAccount
    {
        [key]
        public int UserId { get; set; }

        [Required(ErrorMessage ="Please Enter FirstName")]
        public string FirstName { get; set; }

        [Required(ErrorMessage ="Please Enter LastName")]
        public string LastName { get; set; }

        [Required(ErrorMessage ="Please Enter Email-Id")]
        [EmailAddress(ErrorMessage ="Please Enter a Valid Email-Id")]
        public string EmailId { get; set; }

        [Required(ErrorMessage ="Please Enter UserName")]
        public string UserName { get; set; }

        [Required (ErrorMessage ="Enter Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password",ErrorMessage ="Password Not Matching")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }

    public class Crudoperation
    {
        string str = ConfigurationManager.ConnectionStrings["CrudConnection"].ConnectionString;
        SqlConnection con;
        SqlCommand cmd;

        public IEnumerable<UserAccount> GetUserInfoForLogin

        {
            get
            {
                con = new SqlConnection(str);
                con.Open();
                cmd = new SqlCommand();
                cmd.CommandText = "SSpGetAll";
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader dr = cmd.ExecuteReader();


                List<UserAccount> user = new List<UserAccount>();

                while (dr.Read())
                {
                    UserAccount usr = new UserAccount();
                    usr.UserName = dr["UserName"].ToString();
                    usr.Password = dr["Password"].ToString();

                    user.Add(usr);
                }

                dr.Close();
                con.Close();

                return user;
            }
        }

        public void AddUser(UserAccount user)
        {
            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand();
            cmd.CommandText = "SSpInsertAll"; 
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
            cmd.Parameters.AddWithValue("@LastName", user.LastName);
            cmd.Parameters.AddWithValue("@EmailId", user.EmailId);
            cmd.Parameters.AddWithValue("@UserName", user.UserName);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            int result= cmd.ExecuteNonQuery();

            con.Close();
        }
    }
}
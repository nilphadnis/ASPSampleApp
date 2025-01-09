using SampleApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace SampleApp.DAL
{
    public class RegistrationModule
    {
        private string ConnectionString
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["SampleDBConnectionString"].ToString();
            }
        }

        /// <summary>
        /// New Registrations
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string Register(RegistrationModel model)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("usp_Register", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@FullName", SqlDbType.VarChar, 100) { Value = model.FullName });
                    command.Parameters.Add(new SqlParameter("@Gender", SqlDbType.VarChar, 1) { Value = model.Gender });
                    command.Parameters.Add(new SqlParameter("@StateId", SqlDbType.Int) { Value = model.StateId });
                    command.Parameters.Add(new SqlParameter("@DOB", SqlDbType.Date) { Value = model.DOB });
                    command.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 100) { Value = model.Email });
                    command.Parameters.Add(new SqlParameter("@Phone", SqlDbType.VarChar, 50) { Value = model.Phone });
                    SqlParameter param_MSG = new SqlParameter("@Ret_Msg", SqlDbType.VarChar, 100);
                    param_MSG.Direction = ParameterDirection.Output;
                    command.Parameters.Add(param_MSG);
                    connection.Open();
                    command.ExecuteNonQuery();
                    string result = command.Parameters["@Ret_Msg"].Value.ToString();
                    connection.Close();
                    return result;
                }
            }
        }

        /// <summary>
        /// Check email availability
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public string CheckEmailAvailability(string email)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("usp_CheckEmailExists", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar, 100) { Value = email });
                    SqlParameter param_MSG = new SqlParameter("@Ret_Msg", SqlDbType.VarChar, 100);
                    param_MSG.Direction = ParameterDirection.Output;
                    command.Parameters.Add(param_MSG);
                    connection.Open();
                    command.ExecuteNonQuery();
                    string result = command.Parameters["@Ret_Msg"].Value.ToString();
                    connection.Close();
                    return result;
                }
            }
        }

        /// <summary>
        /// Get all registrations
        /// </summary>
        /// <returns></returns>
        public List<RegistrationModel> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("usp_GetRegistrations", connection))
                {
                    List<RegistrationModel> lstRegistrations = new List<RegistrationModel>();
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        lstRegistrations.Add(new RegistrationModel
                        {
                            Id = Convert.ToInt32(dataReader["Id"]),
                            FullName = Convert.ToString(dataReader["FullName"]),
                            DOB = Convert.ToDateTime(dataReader["DOB"]),
                            Email = Convert.ToString(dataReader["Email"]),
                            Phone = Convert.ToString(dataReader["Phone"]),
                            Gender = Convert.ToString(dataReader["Gender"]),
                            StateId = Convert.ToInt32(dataReader["StateId"]),
                            StateName = Convert.ToString(dataReader["StateName"]),
                            CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"])
                        });
                    }
                    connection.Close();
                    return lstRegistrations;
                }
            }
        }

        public List<StateModel> GetStates()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("usp_GetStates", connection))
                {
                    List<StateModel> lstStates = new List<StateModel>();
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        lstStates.Add(new StateModel
                        {
                            Id = Convert.ToInt32(dataReader["Id"]),
                            Name = Convert.ToString(dataReader["Name"]),

                        });
                    }
                    connection.Close();
                    return lstStates;
                }
            }
        }
        public RegistrationModel GetById(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("usp_GetById", connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    RegistrationModel registration = new RegistrationModel();
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {

                        registration.Id = Convert.ToInt32(dataReader["Id"]);
                        registration.FullName = Convert.ToString(dataReader["FullName"]);
                        registration.DOB = Convert.ToDateTime(dataReader["DOB"]);
                        registration.Email = Convert.ToString(dataReader["Email"]);
                        registration.Phone = Convert.ToString(dataReader["Phone"]);
                        registration.Gender = Convert.ToString(dataReader["Gender"]);
                        registration.StateId = Convert.ToInt32(dataReader["State"]);
                        registration.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"]);

                    }
                    connection.Close();
                    return registration;
                }
            }
        }
    }
}
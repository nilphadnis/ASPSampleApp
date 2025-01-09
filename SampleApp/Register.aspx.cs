using SampleApp.DAL;
using SampleApp.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web.Services;
using System.Web.UI;

namespace SampleApp
{
    public partial class Register : System.Web.UI.Page
    {
        private RegistrationModule registrationModuleObj = new RegistrationModule();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ddlStates.DataSource = registrationModuleObj.GetStates();
                ddlStates.DataTextField = "Name";
                ddlStates.DataValueField = "Id";
                ddlStates.DataBind();
            }
        }
        protected void btnRegister_Click(object sende, EventArgs e)
        {
            
            DateTime dateOfBirth;
            if (DateTime.TryParseExact(txtDOB.Text.Trim(), "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dateOfBirth))
            {

                RegistrationModel model = new RegistrationModel
                {
                    FullName = txtFullName.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    StateId = Convert.ToInt32(ddlStates.SelectedValue),
                    DOB = dateOfBirth,
                    Gender = rdbMale.Checked ? "M" : "F"
                };

                lblMessage.Text = registrationModuleObj.Register(model);
                Task.Run(async () => await SendEmail(model.Email));
            }
            else
            {
                lblMessage.Text = "Invalid DOB";
            }
            
            string script = "setTimeout(function() { $('#myModal').modal('show'); }, 500);";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", script, true);
        }
        private async Task SendEmail(string mailAddress)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient();
            mail.To.Add(mailAddress);
            mail.From = new MailAddress("no-replay@sampleapp.com");
            mail.Subject = "Sample Web Application Registration Completed";
            mail.IsBodyHtml = true;
            mail.Body = $"You have registered your email address {mailAddress} with sample web application";        
           
            
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Host = "smtp.gmail.com";
            SmtpServer.EnableSsl = true;
            SmtpServer.Port = 587;
            SmtpServer.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            SmtpServer.Credentials = new NetworkCredential("jeevandatafortune@gmail.com", "fortune2024@");           
            try
            {
               
                await SmtpServer.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
             
            }
        }

        [WebMethod]
        public static string CheckEmailAddressAvailability(string email)
        {
            return new RegistrationModule().CheckEmailAvailability(email);
        }
    }
}
  
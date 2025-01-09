using System;
using SampleApp.Models;
using SampleApp.DAL;
using System.Collections.Generic;
using System.Text;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.IO;

namespace SampleApp
{
    public partial class Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindDataTable();
        }

        protected void btnExport_Click(object sender,EventArgs e)
        {
            DownloadExcel();
        }

        private List<RegistrationModel> GetAllRegistrations()
        {
            return new RegistrationModule().GetAll();
        }
        private void BindDataTable()
        {
            List<RegistrationModel> registrations = GetAllRegistrations();
            StringBuilder htmlTable = new StringBuilder();
            htmlTable.Append("<table id='tblRegistrations' class='display'>");
            htmlTable.Append("<thead><tr><th>SNo</th><th>Full Name</th><th>Gender</th><th>State</th><th>Date Of Birth</th><th>Email</th><th>Phone No</th><th>Date Of Registration</th><th>Certificate</th></tr></thead>");
            htmlTable.Append("<tbody>");
            int sno = 1;
            foreach (var reg in registrations)
            {
                htmlTable.Append("<tr>");
                htmlTable.AppendFormat("<td>{0}</td>", sno);
                htmlTable.AppendFormat("<td>{0}</td>", reg.FullName);
                htmlTable.AppendFormat("<td>{0}</td>", reg.Gender);
                htmlTable.AppendFormat("<td>{0}</td>", reg.StateName);
                htmlTable.AppendFormat("<td>{0}</td>", reg.DOB.ToString("MM/dd/yyyy"));
                htmlTable.AppendFormat("<td>{0}</td>", reg.Email);
                htmlTable.AppendFormat("<td>{0}</td>", reg.Phone);
                htmlTable.AppendFormat("<td>{0}</td>", reg.CreatedDate?.ToString("MM/dd/yyyy"));
                htmlTable.AppendFormat("<td><a href=\"/CertificateHandler.ashx?id={0}\" class=\"download\">Download</a></td>", reg.Id);
                htmlTable.Append("</tr>");
                sno++;
            }

            htmlTable.Append("</tbody></table>");

            StringBuilder js = new StringBuilder();
            js.Append("<script>");
            js.Append("$(document).ready(function() {");
            js.AppendLine("$('#tblRegistrations').DataTable();");          
            js.Append("});");
            js.Append("</script>");

            ltrlDataTable.Text = htmlTable.ToString() + js.ToString();
        }
        private void DownloadExcel()
        {
            using (var stream = new MemoryStream())
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Registrations");
                    worksheet.Cells["A1"].Value = "SNo";
                    worksheet.Cells["B1"].Value = "FullName";
                    worksheet.Cells["C1"].Value = "Gender";
                    worksheet.Cells["D1"].Value = "state";
                    worksheet.Cells["E1"].Value = "Date of Birth";
                    worksheet.Cells["F1"].Value = "email";
                    worksheet.Cells["G1"].Value = "Phone No";
                    worksheet.Cells["H1"].Value = "date of registration";
                    worksheet.Cells["A1:H1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells["A1:H1"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Blue);
                    worksheet.Cells["A1:H1"].Style.Font.Size = 14;
                    worksheet.Cells["A1:H1"].Style.Font.Bold = true;
                    worksheet.Cells["A1:H1"].Style.Font.Color.SetColor(System.Drawing.Color.White);
                    worksheet.Cells["A1:H1"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells["A1:H1"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells["A1:H1"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells["A1:H1"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    List<RegistrationModel> registrations = GetAllRegistrations();
                    int row = 2;
                    foreach (var reg in registrations)
                    {
                        worksheet.Cells[$"A{row}"].Value = row - 1;
                        worksheet.Cells[$"B{row}"].Value = reg.FullName;
                        worksheet.Cells[$"C{row}"].Value = reg.Gender.Equals("Male") ? "M" : "F";
                        worksheet.Cells[$"D{row}"].Value = reg.StateName;
                        worksheet.Cells[$"E{row}"].Value = reg.DOB.ToString("MM/dd/yyyy");
                        worksheet.Cells[$"F{row}"].Value = reg.Email;
                        worksheet.Cells[$"G{row}"].Value = reg.Phone;
                        worksheet.Cells[$"H{row}"].Value = reg.CreatedDate?.ToString("MM/dd/yyyy");
                        row++;
                    }

                    worksheet.Cells[$"A2:H{row - 1}"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A2:H{row - 1}"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A2:H{row - 1}"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A2:H{row - 1}"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    worksheet.Cells[$"A2:H{row - 1}"].Style.Font.Size = 12;
                    worksheet.Cells[$"A1:H{row - 1}"].AutoFitColumns();
                    package.SaveAs(stream);

                }

                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment; filename=Registrations.xlsx");
                Response.BinaryWrite(stream.ToArray());
                Response.End();
            }
        }
    }
}
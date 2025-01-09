using SampleApp.DAL;
using SampleApp.Models;
using System;
using System.IO;
using System.Web;
using Aspose.Words;
using Aspose.Words.Replacing;

namespace SampleApp
{
    /// <summary>
    /// Summary description for CertificateHandler
    /// </summary>
    public class CertificateHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            if (HttpContext.Current.Request.QueryString.Get("id") != null)
            {
                License licence = new License();
                FileStream fsLicense = new FileStream(HttpContext.Current.Server.MapPath("~/Aspose.TotalProductFamily.lic"),FileMode.Open);
                licence.SetLicense(fsLicense);
                fsLicense.Close();
              

                int id = Convert.ToInt32(HttpContext.Current.Request.QueryString["id"]);
                RegistrationModel registration = new RegistrationModule().GetById(id);               
                string tempPdfPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".pdf");            
               
                Document doc = new Document(HttpContext.Current.Server.MapPath("~/attendee_certificate.docx"));
                doc.Range.Replace("[ATTENDEE_FULL_NAME]", registration.FullName, new FindReplaceOptions(FindReplaceDirection.Forward));
                doc.Range.Replace("[TOPIC_TITLE]", "Sample Course", new FindReplaceOptions(FindReplaceDirection.Forward));
                doc.Range.Replace("[EVENT_DATE]", registration.CreatedDate?.ToString("MM/dd/yyyy"), new FindReplaceOptions(FindReplaceDirection.Forward));
                doc.Save(tempPdfPath, SaveFormat.Pdf);
                
                MemoryStream pdfMemoryStream = new MemoryStream(File.ReadAllBytes(tempPdfPath));
                File.Delete(tempPdfPath);

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=Certificate.pdf");
                HttpContext.Current.Response.BinaryWrite(pdfMemoryStream.ToArray());
                HttpContext.Current.Response.End();
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
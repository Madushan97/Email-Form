using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace E_Mail
{
    public partial class Form1 : Form
    {
        OpenFileDialog ofdAttachment;
        String fileName = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnBrowseFile_Click(object sender, EventArgs e)
        {
            try
            {
                ofdAttachment = new OpenFileDialog();
                ofdAttachment.Filter = "Image(.jpg,.png)|*.png;*.jpg;|pdf Files|*.pdf";
                if (ofdAttachment.ShowDialog() == DialogResult.OK)
                {
                    fileName = ofdAttachment.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                //SMTP client Details

                SmtpClient clientDetails = new SmtpClient();

                //port : 587 for both Gmail and Ymail
                clientDetails.Port = Convert.ToInt32(txtPortNumber.Text.Trim());

                //Gmail smtp server : smtp.gmail.com
                //Ymail smtp server : smtp.mail.yahoo.com
                clientDetails.Host = txtSmtpServer.Text.Trim();

                //SSL : Required for both Yahoo and google
                clientDetails.EnableSsl = cbxSSL.Checked;

                clientDetails.DeliveryMethod = SmtpDeliveryMethod.Network;
                clientDetails.UseDefaultCredentials = false;
                clientDetails.Credentials = new NetworkCredential(txtSenderEmail.Text.Trim(), txtSenderPassword.Text.Trim());

                //Message Details

                MailMessage mailDetails = new MailMessage();
                mailDetails.From = new MailAddress(txtSenderEmail.Text.Trim());
                mailDetails.To.Add(txtRecipientEmail.Text.Trim());

                //for multiple recipients
                //mailDetails.To.Add("another email address");

                //for BCC
                //mailDetails.Bcc.Add("BCC email address");

                mailDetails.Subject = txtSubject.Text.Trim();
                mailDetails.IsBodyHtml = cbxHtmlBody.Checked;
                mailDetails.Body = rtbBody.Text.Trim();

                //file attachment
                if (fileName.Length > 0)
                {
                    Attachment attachment = new Attachment(fileName);
                    mailDetails.Attachments.Add(attachment);
                }

                clientDetails.Send(mailDetails);
                MessageBox.Show("Your Mail has been sent.");
                fileName = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}

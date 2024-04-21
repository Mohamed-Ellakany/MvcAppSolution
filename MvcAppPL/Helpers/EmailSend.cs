using MvcAppDAL.Models;
using System.Net;
using System.Net.Mail;

namespace MvcAppPL.Helpers
{
    public static class EmailSend
    {

        public static void SendEamil(Email email)
        {

            var Client = new SmtpClient("smtp.gmail.com" ,587);


            Client.EnableSsl = true;
            Client.Credentials = new NetworkCredential("esamm612@gmail.com", "swinjbyqzlbzcvve");
            Client.Send("esamm612@gmail.com",email.To , email.Subject , email.Body);


        }



    }
}

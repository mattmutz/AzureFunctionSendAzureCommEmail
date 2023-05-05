using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Azure;
using Azure.Communication.Email;
using System.Text.Json;

namespace AzureCommEmail.FormReceiver
{
    public static class HttpPostTrigger
    {
        [FunctionName("HttpPostTrigger")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var requestTime = DateTime.Now;
            {
                var body = await new StreamReader(req.Body).ReadToEndAsync();
                var inquiry = JsonSerializer.Deserialize<Contact>(body);
                var name = inquiry.name;
                var email = inquiry.email;
                var message = inquiry.message;

                string responseMessage = $"form request at " + requestTime.ToString();
                log.LogInformation(responseMessage);

                // 1) your connection string here
                string connectionString = "endpoint=https://your-endpoint.communication.azure.com/;accesskey=000000";
                EmailClient emailClient = new EmailClient(connectionString);

                // 2) your from email address here
                var sender = "DoNotReply@00000000-0000-0000-0000-000000000000.azurecomm.net";

                // 3) your email recipient value here
                var recipient = "you@example.com";

                var subject = "Web Form Submission";
                var htmlContent = $"<html>Web form<br/>";
                htmlContent += $"<p>Name: {name}</p>";
                htmlContent += $"<p>Email: {email}</p>";
                htmlContent += $"<p>Message: {message}</p>";
                htmlContent += "</body></html>";

                try
                {
                    Console.WriteLine("Sending email...");
                    EmailSendOperation emailSendOperation = await emailClient.SendAsync(
                        Azure.WaitUntil.Completed,
                        sender,
                        recipient,
                        subject,
                        htmlContent);
                    EmailSendResult statusMonitor = emailSendOperation.Value;

                    Console.WriteLine($"Email Sent. Status = {emailSendOperation.Value.Status}");

                    /// Get the OperationId so that it can be  used for tracking the message for troubleshooting
                    string operationId = emailSendOperation.Id;
                    return new OkObjectResult($"Email operation id = {operationId}");
                }
                catch (RequestFailedException ex)
                {
                    /// OperationID is contained in the exception message and can be used for troubleshooting purposes
                    return new BadRequestObjectResult($"fail: {ex.ErrorCode},{ex.Message}");
                }
            }
        }
    }
}

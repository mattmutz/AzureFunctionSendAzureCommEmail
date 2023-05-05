# Send email with Azure Communication Services
This is an html form which posts to an Azure Function to send an email through Azure Communication Services Email. 

## Configure your dev environment
This project does not aim to be a complete tuturial, and assumes familiarity with Azure resources and development. This was built in Visual Studio Code and requires your environment to be configured for developing Azure Functions. If you have VS Code configured with the Azure Functions extension, it should work. If you aren't sure, start here:
[Code and test Azure Functions locally](https://learn.microsoft.com/en-us/azure/azure-functions/functions-develop-local)

## Configure Azure Communication Services, and Email for Azure Comm Services
[Quickstart: Create and manage Communication Services resources](https://learn.microsoft.com/en-us/azure/communication-services/quickstarts/create-communication-resource)

[How to send an email using Azure Communication Service](https://learn.microsoft.com/en-us/azure/communication-services/quickstarts/email/send-email?tabs=windows%2Cconnection-string&pivots=programming-language-csharp)

## Update code with the settings for your Azure resources
1) Connection string
2) MailFrom address from your email communication service
3) email to &mdash; this is not in the Azure portal, it is the address you want to send to, e.g. @hotmail.com or @gmail.com

## Test
<pre>func start</pre>

you should see this in the output 

<pre>Functions:

        HttpPostTrigger: [POST] http://localhost:7071/api/HttpPostTrigger
</pre>
The Azure Function is now running locally in the Azure Function emulator. Post the form and you should receive an email at the address entered into your settings (3)s
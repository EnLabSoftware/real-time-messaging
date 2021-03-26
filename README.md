## Author

👤 **Trong Pham**

* Article: https://enlabsoftware.com/development/how-to-create-real-time-chat-applications-using-websocket-apis-in-api-gateway.html

### Installation

* Visual Studio 2019 with the ASP.NET and web development workload
* [.NET core 3.1 SDK](https://dotnet.microsoft.com/download/dotnet/3.1) or later
* Exposes local servers to the public internet by [Ngrok](https://ngrok.com/download)

### How to run the app

1. Open Visual Studio 2019
2. Open WebSocketService.cs file
3. Add your AWS **WebSocket Endpoint**, **AccessKeyId**, **SecretAccessKey**
2. Build and run the app
3. Open ngrok.exe and run this command
    ```sh
    >ngrok http -host-header="localhost:23409" 23409
    ```
4. Update the new ngrok endpoint to your AWS Websocket config that includes *$connect*, *$sendMessage*, *$disconnect*. 
5. Open the *index.html* file in **UI** folder
6. Add your AWS **WebSocket Endpoint**
6. Run index.html to see the simple chat room

*Happy coding!*

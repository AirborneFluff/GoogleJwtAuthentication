# GoogleJwtAuthentication

Simple way to authenticate using Google Issued JWTs.
Pass the JWT in the HTTP request header as 'Bearer'.

Setup in the program by setting the authorization options shown in the example Program.cs file.
You must include the ClientId provided by Google's API & Services for your given app.
Whenever a client requests a JWT, it must come by an audience to Google's API app.

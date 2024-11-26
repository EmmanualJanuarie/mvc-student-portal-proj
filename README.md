# Student Portal WebApp

## Overview
In my latest project, I developed a **Student Portal WebApp** using the MVC design pattern, emphasizing modularity and maintainability. The application features a robust authentication system with custom controllers for user registration and login.

## Features
- **Authentication System**: 
  - Implemented a secure authentication system with custom controllers for user registration and login.
  - Integrated third-party authentication options, allowing users to log in via:
    - Google
    - Facebook
    - GitHub
  - Each platform has dedicated methods, such as `Signin_github` and `GoogleResponse`, ensuring secure processing and session storage of user credentials.

- **Real-Time Communication**: 
  - Implemented a chat interface using **SignalR**, enabling dynamic interaction among students.

- **Report Issuing Form**: 
  - Created a report issuing form that exemplifies client-server interaction through API integration.
  - Utilizes a dedicated controller with GET and POST actions, where:
    - The POST action serializes input data to JSON.
    - Communicates with an external API endpoint (`/SOS/PostData`) via **HttpClient**, providing immediate feedback based on API responses.

- **Backend**: 
  - Powered by **Microsoft SQL Server** for reliable data storage.
  - Configured credentials and permissions in the `Program.cs` file to facilitate secure Facebook, Google, and GitHub authentication.

## Design Philosophy
The Student Portal WebApp showcases my ability to leverage modern web technologies and best practices, creating an engaging platform that enhances the educational experience for students.

## Getting Started
To get started with the Student Portal WebApp, follow these steps:
1. Clone the repository to your local machine.
2. Ensure you have the required dependencies installed.
3. Configure the database connection in the `appsettings.json` file.
4. Run the application and access the portal through your web browser!

## Contributing
Contributions are welcome! If you have suggestions for improvements or new features, please create an issue or submit a pull request.

## License
This project is licensed under the MIT License.

# LearnAPI

Welcome to the README for LearnAPI! This document provides an overview of the available endpoints and their functionalities.

## Table of Contents

- [Introduction](#introduction)
- [Getting Started](#getting-started)
- [API Endpoints](#api-endpoints)
- [Usage Examples](#usage-examples)
- [Contributing](#contributing)
- [License](#license)

## Introduction

This API provides functionalities to interact with various features of your application. It is written in C# and follows RESTful design principles.

## Getting Started

To get started, follow these steps:

1. Clone this repository: `git clone https://github.com/whosNikoloz/LearnAPI.git`
2. Open the project in your preferred C# development environment.
3. Set up your database connection and environment settings.
4. Build and run the application.

## API Endpoints

### Auth

- `POST /api/Auth/Register`
- `DELETE /api/Auth/Remove/:userid`
- `GET /api/Auth/Email`
- `GET /api/Auth/Username`
- `GET /api/Auth/Phone`

### User

- `GET /api/User/:username`
- `PUT /api/User/ChangePassword?newpassword=<string>&oldpassword=<string>`
- `PUT /api/User/ChangeUsernameOrNumber`
- `POST /api/User/UploadImage`
- `GET /api/User/ForgotPassword?email=<string>`
- `POST /api/User/ResetPassword`
- `GET /api/Users`

### Social

- `GET /api/Social/Posts/:Userid`
- `GET /api/Social/Posts/:postId`
- `POST /api/Social/Posts`
- `PUT /api/Social/Post/:postId`
- `DELETE /api/Social/Post/:postId`
- `GET /api/Social/Comments/:postid?userid=<integer>`
- `GET /api/Social/Comments/:postId`
- ... (and more)

### Progress

- `POST /api/Progress/complete-subject`
- `POST /api/Progress/complete-course`
- `POST /api/Progress/complete-level`

### Learn

- `GET /api/Learn/Levels/:levelid`
- `GET /api/Learn/Courses/:courseid`
- `POST /api/Learn/Courses/:courselid?courseid=<integer>`
- ... (and more)

## Usage Examples

Here are some example requests you can make to the API:

```http
GET {{baseUrl}}/api/Auth/Email
GET {{baseUrl}}/api/User/johndoe
POST {{baseUrl}}/api/Social/Posts

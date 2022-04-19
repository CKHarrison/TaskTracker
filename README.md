# Task Tracker Application

---

Complete CRUD application with PostgreSQL database used to keep track of business tasks.
[Click to view application](http://asp-tasktracker.herokuapp.com/)

## Table of Contents

---

- [Task Tracker Application](#task-tracker-application)
  - [Table of Contents](#table-of-contents)
  - [Project Status](#project-status)
  - [General Information](#general-information)
  - [Technology](#technology)
  - [Future Improvements](#future-improvements)
  - [Contact Information](#contact-information)

---

## Project Status

Project status: In Progress
Version 1.0 Deployed

---

## General Information

---

This project started out as a way for me to put together a fully fledged ASP.NET application backed with a complete database from scratch. The idea was to create an application that would serve an actual purpose and would be useful in a business setting. I wanted to create something that I could use to track activites, assign start and end dates and categorize them based on user roles and categories. Each user can log in and create their own tasks, with full CRUD capability, while being able to view their team's current tasks, but not manipulate them.

I used this project as a way to become more familiar with Microsoft's Identity Platform for authentication and authorization, as well as learn how to send emails for validation. One of the goals of this project was to soldify my knowledge of Dapper and implement SOLID principles by seperating my business logic with the data access layer.

At first this was to mainly be a back-end application with a minimal front end as I was only interested of the inner workings of the project. Now that I have completed my inital goals however, I plan on remaking the front end into something more accessible and pleasing to look at.
I also would like to create team specific roles, so that upon sign up you would select your team and be able to see only what they are working on, and assign yourself to that task, similar to Trello or Jira.

---

## Technology

- C#
- ASP.NET Razor Pages
- PostgreSQL
- Identity
- Entity Framework
- Dapper
- Heroku
- AutoMapper

---

## Future Improvements

I have a couple major version updates I would like to add to this project before I'd consider it finished:

1. Implement roles upon account creation so user can select from teams to work on and contribute to
2. Add comments to each task so users can communicate with each other about a task
3. Convert front end to React.JS application

---

## Contact Information

If you have any questions about this project please reach out!
[Contact Me](chris@charrison.dev)

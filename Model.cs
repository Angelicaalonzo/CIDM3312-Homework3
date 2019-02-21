using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace Homework3{

public class AppDbContext : DbContext
    {
         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = database.db");
        }

        public DbSet<User> Users {get; set;}
        public DbSet<Question> Questions {get; set;}
        public DbSet<Answer> Answers {get; set;}
    }

public class User
    {
        public string UserID {get; set;}    // PK
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string Email {get; set;}
        public DateTime RegistrationDate {get; set;}
        public int AnswerID {get; set; }
         List <User> Users {get; set; }
        public List<Question> Question {get; set;} 
       public List<Answer> Answer {get; set;}

        public override string ToString()
        {
            return ($"{UserID}: {FirstName} {LastName}, {Email}, {RegistrationDate}");
        }
    }

    public class Question{
        public int QuestionID {get; set; }
        public int UserID {get; set;} //FK
        public int FirstName{get; set;} //FK
        public string QuestionText{get; set;}
        public DateTime QuestionPosted {get; set; }
        public User User {get; set;}
        public int AnswerID{get; set; }
    
       public List <Answer> Answers {get; set;}// One Question can have many answers


        public override  string ToString() {
            return ($"{QuestionID}: {QuestionText} - {QuestionPosted}");
        }
    }

    public class Answer {
        public int AnswerID {get; set; }
        public int UserID {get; set;}// FK
        public string AnswerText{get; set; }
        public DateTime AnswerPosted{get; set; }
        public User User {get; set; }
        public int QuestionID{get; set; }
        public Question Question {get; set; }// Each answer is associated with one question


        public override string ToString() {
            return ($"{AnswerID}: {AnswerText} - {AnswerPosted}");
        }
    }
}
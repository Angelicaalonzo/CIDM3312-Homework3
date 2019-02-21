/*
Add a quit function 
add if else for list unanswered

 */


using System;
using System.Collections.Generic;
using System.Linq;

namespace Homework3
{
    class Program
    {
        
        static void SeedDatabase()//  It creates a list of existing users and adds that list to the database
        {
             

        //creating a new empty list for users, questions, and answers
        using (var db = new AppDbContext())
            {

            List<User> Users = new List <User>(); 
            List<Question> Questions = new List<Question>(); 
            List<Answer> Answers = new List<Answer>(); 

            
            }
        }

//When a user selects log in it asks them to enter an email.
        static void LogIn(){

            string email = "";
            Console.WriteLine("Please enter your E-mail address.");
            email = Console.ReadLine();
            // If the Email is valid the user is asked to select an option
            using (var db = new AppDbContext()){
            
            if(db.Users.Any(m=> m.Email  == email)){
                    Console.WriteLine("Success! You are now logged in");

            }else

            // Adding a new user to the database
            
                Console.WriteLine("Sorry, that email does not exist. Let's create an account.  ");
                User A = new User(); 
                Console.WriteLine("Enter Your First Name");
                A.FirstName = Console.ReadLine(); 
                Console.WriteLine("Please enter your Last Name");
                A.LastName = Console.ReadLine(); 
                Console.WriteLine("Enter your Email");
                A.Email = Console.ReadLine();
                A.RegistrationDate= DateTime.Now; 

                
            db.Add(A);
            db.SaveChanges(); 
                
        }
          
            do
            {
                Console.WriteLine("\n Please choose one of the following");
                Console.WriteLine("\n1. Ask a Question \n2. List all Questions \n3. List Only Unanswered Questions \n4. Delete a question you have posted");
                
                string cmd = Console.ReadLine();
                switch (cmd)
                {
                    case "1":
                      AskQuestion(); 
                        break;
                    case "2":
                        ListQuestions();
                        break;
                    case "3":
                        ListUnanswered();
                       break;
                    case "4":
                      Delete();
                        break;
                    case "q":
                    return; 
                    default:
                       Console.WriteLine("Invalid command.");
                        break;
                }
            } while (true);
        }

        static void ListUnanswered(){

            

           using (var db= new AppDbContext()){

             var questions = db.Questions.Where (q => q.Answers.Count()==0);
             foreach(var q in questions){
                  Console.WriteLine(db.ToString());
               }
               
           }
       }
 


        // Lets the user ask a question
    static void AskQuestion(){
        
        Question Question = new Question(); 
        Console.WriteLine("What is your Question?");
        Question.QuestionText= Console.ReadLine();


        using (var db = new AppDbContext()){
            db.Add(Question);
            db.SaveChanges();
        }
    }

    //Lets the user list questions

    static void ListQuestions(){

        using(var db = new AppDbContext()){
        string input = "";
//If there arent any questions the user is asked if they want to ask a question. 
        if (db.Questions.Count()==0){
            
             Console.WriteLine("There aren't any questions posted. Would you like to ask a question? Please reply 'Yes'");
            input = Console.ReadLine();
             if (input == "Yes"){
                 AskQuestion(); 
             }
            else{
                Console.WriteLine("sorry that input is invalid");
             }

        }
            foreach (Question m in db.Questions){
                Console.WriteLine($"({m.QuestionID} Posted By:{m.UserID} {m.FirstName} {m.QuestionPosted} \n {m.QuestionText}\n \t {m.Answers} \n {db.Questions.Count()})");
            }
            //after questions are listed the user is asked if they want to answer a question

            Console.WriteLine("Would you like to answer a question?");
            input = Console.ReadLine();
             if (input == "Yes"){
                 AnswerQuestion(); 
             }
            else{
                Console.WriteLine("sorry that input is invalid");
             }
            
        }

    }

    //Lets the user answer a question
    static void AnswerQuestion(){
        string answer= "";
        Console.WriteLine("Please enter the ID of the quesion you would like to answer");
        int id = Convert.ToInt32(Console.ReadLine());
        using(var db = new AppDbContext()){

            Question a = db.Questions.Find(id);
            answer= Console.ReadLine(); 

            db.Add(answer); 
            db.SaveChanges(); 
        }  
    }

     static void Delete(){
     
     Console.WriteLine("Please enter the ID of the question you would like to delete");
     int id = Convert.ToInt32(Console.ReadLine());

     using (var db= new AppDbContext()){
         if(db.Users.Any(m => m.UserID == m.UserID )){
             Question a = db.Questions.Find(id);
             db.Remove(a);
             db.SaveChanges();
             Console.WriteLine("Your post was successfully deleted");
         }else{
             Console.WriteLine("Sorry, you are not able to delete questions that you did not post.");
         }
        
     }

 }


        static void Main(string[] args)
        {
            using (var db = new AppDbContext())
            {
                db.Database.EnsureDeleted(); 
                db.Database.EnsureCreated(); 
            }

             SeedDatabase(); // Put initial patient data from requirement #8 into database
            // Loop that prompts user for task and then executes what user wants
            // This will keep looping until user enters 'q' for quit.

            Console.WriteLine("Welcome! ");

            //routes to the log in function.
            LogIn(); 
      
        }
    }
}

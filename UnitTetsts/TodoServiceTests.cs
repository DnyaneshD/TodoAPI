using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using TodoAPI.Data;
using TodoAPI.Models;
using TodoAPI.Services;

namespace UnitTetsts
{
    [TestClass]
    public class TodoServiceTests
    {
        [TestMethod]
        public void FindTest()
        {
            //Arrange 
            var options = new DbContextOptionsBuilder<TodoContext>()
               .UseInMemoryDatabase(databaseName: "TODOs")
               .Options;

            var todoModel = new TodoModel
            {
                Details = "Test todo",
                DueDate = new System.DateTime()
            };

            using (var context = new TodoContext(options))
            {
                var todoService = new TodoService(context);
                todoService.CreateTodo(todoModel).Wait();
            }

            using (var context = new TodoContext(options))
            {
                var todoService = new TodoService(context);

                //Act
                var result = todoService.Find().Result;

                //Assert
                Assert.AreEqual(result.Count(), 1);
                Assert.AreEqual(result.FirstOrDefault().Details, todoModel.Details);
            }
        }

        [TestMethod]
        public void FindOneTest()
        {
            //Arrange 
            var options = new DbContextOptionsBuilder<TodoContext>()
               .UseInMemoryDatabase(databaseName: "TODOs")
               .Options;

            var todoModel = new TodoModel
            {
                Details = "Test todo",
                DueDate = new System.DateTime()
            };

            var context = new TodoContext(options);
            var todoService = new TodoService(context);
            var createdTodo = todoService.CreateTodo(todoModel).Result;
            
            //Act
            var result = todoService.FindOne(createdTodo.Id).Result;

            //Assert
            Assert.AreEqual(result.Id, createdTodo.Id);
            Assert.AreEqual(result.Details, todoModel.Details);
        }

        [TestMethod]
        public void UpdateTest()
        {
            //Arrange 
            var options = new DbContextOptionsBuilder<TodoContext>()
               .UseInMemoryDatabase(databaseName: "TODOs")
               .Options;

            var todoModel = new TodoModel
            {
                Details = "Test todo",
                DueDate = new System.DateTime()
            };

            var context = new TodoContext(options);
            var todoService = new TodoService(context);
            var todoForUpdate = todoService.CreateTodo(todoModel).Result;

            todoForUpdate.Details = "Update Test todo";

            //Act
            var updatedTodo = todoService.UpdateTodo(todoForUpdate).Result;

            //Assert
            Assert.AreEqual(updatedTodo.Id, todoForUpdate.Id);
            Assert.AreEqual(updatedTodo.Details, todoForUpdate.Details);
        }

        [TestMethod]
        public void DeleteTest()
        {
            //Arrange 
            var options = new DbContextOptionsBuilder<TodoContext>()
               .UseInMemoryDatabase(databaseName: "TODOs")
               .Options;

            var todoModel = new TodoModel
            {
                Details = "Test todo",
                DueDate = new System.DateTime()
            };

            var context = new TodoContext(options);
            var todoService = new TodoService(context);
            var createdTodo = todoService.CreateTodo(todoModel).Result;

            //Act
            todoService.DeleteTodo(createdTodo.Id).Wait();

            //Assert
            var result = todoService.FindOne(createdTodo.Id).Result;
            Assert.IsNull(result);
        }
    }
}

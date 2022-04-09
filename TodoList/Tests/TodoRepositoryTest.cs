using NUnit.Framework;
using To_Do_List_Backend.Domain;
using To_Do_List_Backend.Repositories;

namespace Tests
{
    public class TodoRepositoryTest
    {
        private ITodoRepository todoRepo;

        [SetUp]
        public void SetUp()
        {
            todoRepo = new TodoRepository();
        }

        [Test]
        public void Get_FunctionalTest_ExpectedTodo()
        {
            var expected = new Todo() { Id = 1, Title = "Test", IsDone = false };
            var result = todoRepo.Get( expected.Id );
            Assert.IsTrue( expected.Equals( result ) );
        }

        [Test]
        public void Get_UnexistingId_DefaultTodo()
        {
            var expected = new Todo();
            var result = todoRepo.Get( 10000 );
            Assert.IsTrue( expected.Equals( result ) );
        }

        [Test]
        public void Insert_NewId()
        {
            // Arrange 
            var expected = new Todo() { Title = "Test: Insert_NewId", IsDone = true };

            // Act 
            var resultId = todoRepo.Create( expected );
            var result = todoRepo.Get( resultId );
            expected.Id = resultId;

            // Assert 
            Assert.IsTrue( expected.Equals( result ) );
        }

        [Test]
        public void Update_UnexistingId_NoUpdates()
        {
            // Arrange 
            var todo = new Todo() { Id = 10000, Title = "Test: Update_UnexistingId_NothingHappens", IsDone = true };
            var defaultTodo = new Todo(); 

            // Act 
            var resultId = todoRepo.Update( todo );
            var result = todoRepo.Get( resultId );

            // Assert 
            Assert.IsTrue( defaultTodo.Equals( result ) );
        }


        [Test]
        public void Update_ExistingId_ObjectUpdates()
        {
            // Arrange 
            var expected = new Todo() { Title = "Test: Update_ExistingId_ObjectUpdates", IsDone = false };
            expected.Id = todoRepo.Create( expected );
            expected.IsDone = true;

            // Act
            var resultId = todoRepo.Update( expected );
            var result = todoRepo.Get( resultId );

            // Assert   
            Assert.IsTrue( expected.Equals( result ) );
        }
    }
}
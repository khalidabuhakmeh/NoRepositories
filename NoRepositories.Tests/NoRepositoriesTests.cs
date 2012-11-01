using System.Data.Entity;
using System.Linq;
using NoRepositories.Core;
using Xunit;

namespace NoRepositories.Tests
{
    /// <summary>
    /// 6. Unit test away!!!!!
    /// </summary>
    public class NoRepositoriesTests
    {
        [Fact]
        public void Can_fake_user_data_from_datacontext()
        {
            // We are using the interface, this can be injected.
            //    1. We can inject it into a controller!
            //    2. We can inject it into a service!
            //    3. We can inject it into a worker!
            //    4. We can inject it anywhere!!!!!
            INoRepositoriesDataContext context = new FakeDataContext();

            // let's add a user
            context.Users.Add(new User
            {
                Id = 1,
                Email = "test@test.com",
                Name = "test user"
            });

            // Hey we have a user
            Assert.True(context.Users.Count() == 1);

            // Hey we need to run linq
            var user = context.Users.FirstOrDefault(u => u.Id == 1);
            Assert.NotNull(user);

            // Hey we need to use Find
            user = context.Users.Find(1);
            Assert.NotNull(user);

            // let's call SaveChanges (with no consequences or database).
            Assert.True(context.SaveChanges() == 1);
        }
    }

    /// <summary>
    /// 5. We can create the datacontext as a fake class or use
    ///    our favorite Mocking Frameworks. But this is easier.
    /// </summary>
    public class FakeDataContext : INoRepositoriesDataContext
    {
        public FakeDataContext()
        {
            Users = new FakeDbSet<User> {
                FindResult = (objs) => Users.FirstOrDefault(u => u.Id == (int)objs[0])
            };
        }

        public IDbSet<User> Users { get; set; }

        public int SaveChanges()
        {
            // you can customize your data context to do whatever you want
            return 1;
        }
    }
}

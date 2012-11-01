using System.Data.Entity;

namespace NoRepositories.Core
{
    public class NoRepositoriesDataContext : DbContext, INoRepositoriesDataContext 
    {
        /// <summary>
        /// 2. There are two things to notice here. 
        ///    a. The use of IDbSet instead of DbSet. EntityFramework will 
        ///       be smart enough to set this as if it were a class of DbSet.
        ///    b. the 'virtual' keyword is needed for lazy loading, but also will
        ///       allow us to override this property if we inherit this class.
        ///     
        ///       note: virtual is your friend. Use it in your classes because it makes
        ///             unit testing very easy with no need for mocking frameworks like
        ///             Moq and Rhino Mocks.
        /// </summary>
        public virtual IDbSet<User> Users { get; set; }
    }

    /// <summary>
    /// 1. The first thing you should do is create an interface for your datacontext.
    ///    Advantages to using an interface include but are not limited to the following:
    ///    a. Dependency Injection ready for your favorite IoC container.
    ///    b. Easily Mocked / Stubbed for your unit tests.
    ///    c. You could switch out if necessary (unlikely and not recommended).
    /// </summary>
    public interface INoRepositoriesDataContext
    {
        /// <summary>
        /// 3. Notice we are mimicking the properties and methods we
        ///    will need to access from our business logic, but that
        ///    this is really simple.
        /// </summary>
        IDbSet<User> Users { get; set; } 
        int SaveChanges();
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}

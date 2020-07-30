namespace Restaraunt.Entities
{
    #region << Using >>

    #endregion

    public class User : EntityBase
    {
        #region Properties

        public virtual string Name { get; set; }

        public virtual string Email { get; set; }

        public virtual string Password { get; set; }

        public virtual Role Role { get; set; }

        #endregion

        #region Nested Classes

        public class Mapping : EntityMapBase<User>
        {
            #region Constructors

            public Mapping()
            {
                Map(a => a.Name);
                Map(a => a.Email);
                Map(a => a.Password);
                Map(a => a.Role).CustomType<Role>();
            }

            #endregion
        }

        #endregion
    }
}
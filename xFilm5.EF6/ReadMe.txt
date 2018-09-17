dbo.User Identity Field

refer: https://stackoverflow.com/questions/13086006/how-can-i-force-entity-framework-to-insert-identity-columns

EF 6 method, using the msdn article:

    using (var dataContext = new DataModelContainer())
    using (var transaction = dataContext.Database.BeginTransaction())
    {
      var user = new User()
      {
        ID = id,
        Name = "John"
      };

      dataContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[User] ON");

      dataContext.User.Add(user);
      dataContext.SaveChanges();

      dataContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[User] OFF");

      transaction.Commit();
    }
Update: To avoid error "Explicit value must be specified for identity column in table 'TableName' either when IDENTITY_INSERT is set to ON or when a replication user is inserting into a NOT FOR REPLICATION identity column", you should change value of StoreGeneratedPattern property of identity column from Identity to None in model designer.

Note, changing of StoreGeneratedPattern to None will fail inserting of object without specified id (normal way) with error "Cannot insert explicit value for identity column in table 'TableName' when IDENTITY_INSERT is set to OFF".
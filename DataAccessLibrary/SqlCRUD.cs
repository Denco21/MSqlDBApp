using DataAccessLibrary.Models;
using System;
using System.Collections.Generic; 
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
	public class SqlCRUD
	{
		private readonly string connectionStringName;
		private SqlDataAccess db = new SqlDataAccess();

		public SqlCRUD( string connectionStringName)
		{
			this.connectionStringName = connectionStringName;
		}


		public List<BasicPeopleModel> GetAllContact( )
		{
			string sql = "select Id, FirstName, LastName from dbo.People";

			return db.LoadData<BasicPeopleModel, dynamic>(sql, new { }, connectionStringName); 

		}


		public FullContactModel GetFullContactById(int id)
		{
			string sql = "select Id, FirstName, LastName from dbo.People where Id = @Id";

			sql = @"select p.*
                  from dbo.People p
                 inner join dbo.ContactPeopleAddresses c on c.PersonId = p.Id
                  where c.PersonId = @Id";
			

			FullContactModel output = new FullContactModel();

			output.BasicInfo = db.LoadData<BasicPeopleModel, dynamic>(sql, new { Id = id }, connectionStringName).FirstOrDefault();

            if (output == null)
            {
				return null;
            }

			sql= @"select a.*
              from dbo.Addresses a
                inner
                  join dbo.ContactPeopleAddresses c on c.AddressId = a.Id
                 where c.PersonId = @Id";

			output.Address = db.LoadData<AddressesModel, dynamic>(sql, new { Id = id }, connectionStringName);

			return output;











		}

		public void CreateContact(FullContactModel contact)
		{
			
			string sql = @"insert into dbo.People (FirstName, LastName) 
                           values (@FirstName, @LastName);
                           select cast(scope_identity() as int);";

			int personId = db.LoadData<int, dynamic>(sql, contact.BasicInfo, connectionStringName).First();

			foreach (var address in contact.Address)
			{
				int addressId;

				sql = @"SELECT Id FROM dbo.Addresses WHERE Street = @Street AND City = @City AND Country = @Country AND PostalCode = @PostalCode";
				addressId = db.LoadData<int, dynamic>(sql, address, connectionStringName).FirstOrDefault();

				if (addressId == 0)
				{
					sql = @"insert into dbo.Addresses (Street, City, Country, PostalCode) 
                            values (@Street, @City, @Country, @PostalCode);
                            select cast(scope_identity() as int);";

					addressId = db.LoadData<int, dynamic>(sql, address, connectionStringName).First();
				}
				else
				{
					addressId = address.Id;
				}

				sql = "insert into dbo.ContactPeopleAddresses (PersonId, AddressId) values (@PersonId, @AddressId);";
				db.SaveData(sql, new { PersonId = personId, AddressId = addressId }, connectionStringName);
			}
		}

		public void UppdateContactName(BasicPeopleModel contact)
		{
			string sql = @"update dbo.People set FirstName = @FirstName, LastName = @LastName where Id = @Id";

			db.SaveData(sql, contact, connectionStringName);
		}


	

		public void RemoveAddress(int Id)
		{
			string sql = @"delete from dbo.ContactPeopleAddresses where AddressId = @Id;
						   delete from dbo.Addresses where Id = @Id;";

			db.SaveData(sql, new { Id = Id }, connectionStringName);
		}


	}
}



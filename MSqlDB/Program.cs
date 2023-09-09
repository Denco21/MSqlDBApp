

using Microsoft.Extensions.Configuration;
using DataAccessLibrary;
using DataAccessLibrary.Models;

public class Program
{

	private static void Main(string[] args)
	{
		

		SqlCRUD sql = new SqlCRUD(GetConnectionString()); 

		//ReadAllContacts(sql);

		//ReadFullContactById(sql, 1);

		//CreateNewContact(sql);

		UppdateContactName(sql);
        Console.WriteLine("Under utveckling");


        Console.ReadLine();

	}
	private static void UppdateContactName(SqlCRUD sql)
	{
		BasicPeopleModel cn = new BasicPeopleModel
		{
			Id = 1,
			FirstName = "Johan",
			LastName = "Petterson"
		};

		sql.UppdateContactName(cn);
	}
	private static void CreateNewContact(SqlCRUD sql)
	{
		FullContactModel user = new FullContactModel 
		{
			BasicInfo = new BasicPeopleModel
			{
				FirstName = "Dion",
				LastName = "Ljungberg"
			}


		};

		user.Address.Add(new AddressesModel
		{
			Street = "123 Main Street",
			City = "Scranton",
			Country = "USA",
			PostalCode = "18503"
		});

		user.Address.Add(new AddressesModel
		{
			Id = 7,
			Street = "Skaragatan 22b",
			City = "Skövde",
			Country = "Sweden",
			PostalCode = "43231"
		});

		sql.CreateContact(user); 
	}

	private static void ReadAllContacts(SqlCRUD sql)
	{

		var rows = sql.GetAllContact();

		foreach (var contact in rows)
		{
			Console.WriteLine($"{ contact.Id } { contact.FirstName } { contact.LastName }");
		}
	}


	private static void ReadFullContactById(SqlCRUD sql, int personId)
	{
		var contact = sql.GetFullContactById(personId);

		Console.WriteLine($"{ contact.BasicInfo.Id } { contact.BasicInfo.FirstName } { contact.BasicInfo.LastName }");

		
	}
	private static string GetConnectionString(string connectionStringName = "Default")
	{
		string output = "";
		var builder = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json");

		var config = builder.Build();

		output = config.GetConnectionString(connectionStringName);

		return output;


	}

}
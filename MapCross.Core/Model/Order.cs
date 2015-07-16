using Cirrious.MvvmCross.Community.Plugins.Sqlite;

namespace MapCross.Core
{
//	[Table("Order")]
	public class Order
	{
		[PrimaryKey, AutoIncrement, Column("_id")]
		public int Id { get; set; }
		public string FirstName { get; set;}
		public string LastName { get; set;}
		public string HamsterLatitude { get; set;}
		public string HamsterLongitude { get; set;}
		public string HamsterColor { get; set;}
		public bool Check { get; set; }

		public string Name
		{
			get { return string.Format ("  {0}  {1}", FirstName, LastName); }
		}


		public string ImageName 
		{
			get{
				if (HamsterColor == "red") {
					return "res:hamster_red_icon";
				} else if (HamsterColor == "yellow") {
					return "res:hamster_yellow_icon";
				} else {
					return "res:hamster_green_icon";
				}
			}
		}
	}
}


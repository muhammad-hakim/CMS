namespace CMS.Models
{
    public class clsCategory
    {
		private static string data;
		public static string selectedCategory {
			get
			{
				return data;
			}
			set
			{
				data = value;
			}
		}
	}	
}

namespace CMS
{
    public class Config
    {

        public static string WWWPath { get; set; }


        public static string BaseWWWPath { get; set; }

        //public static string WWWPath
        //{
        //    get
        //    {
              //return Environment.CurrentDirectory + "\\wwwroot\\";
        //    }
        //}

        public static string RootPath
        {
            get
            {
                return BaseWWWPath + "/";
            }
        }
        public static string ContentPath
        {
            get
            {
                return AppHttpContext.Configuration["ContentPath"].Replace("~", BaseWWWPath);
            }
        }
    }
}

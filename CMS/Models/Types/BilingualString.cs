using System.ComponentModel.DataAnnotations;

namespace CMS.Types
{
    public class BilingualString
    {

        // [RegularExpression("^[a-zA-Z0-9_@./\\-]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        [RegularExpression("^((?!<[^>]*script).)*$", ErrorMessage = "JAVASCRIPT Code not allowed")]

        public string ar { get; set; }
     
        public string en { get; set; }

        public BilingualString() { }
        public BilingualString(string en, string ar)
        {
            this.en = en;
            this.ar = ar;
        }

        public string this[string language]
        {
            get
            {
                return language == "ar" ? ar : en;
            }

        }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CMS.Types
{
    public class BilingualStringList
    {
        [RegularExpression("^((?!<[^>]*script).)*$", ErrorMessage = "JAVASCRIPT Code not allowed")]
        public List<string> ar { get; set; }
        public List<string> en { get; set; }

        public BilingualStringList() { }
        public BilingualStringList(List<string> en, List<string> ar)
        {
            this.en = en;
            this.ar = ar;
        }

        public BilingualStringList(string [] en, string[] ar)
        {
            this.en = en.ToList();
            this.ar = ar.ToList();
        }

        public List<string> this[string language]
        {
            get
            {
                return language == "ar" ? ar : en;
            }

        }
    }
}

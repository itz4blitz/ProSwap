using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSwap.Data
{
    public class CreatePost
    {
        public string Title { get; set; }

        [AllowHtml]
        [UIHint("tinymce_jquery_full")]
        public string Description { get; set; }
    }
}

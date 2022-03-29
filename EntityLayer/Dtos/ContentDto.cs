using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Dtos
{
    public class ContentDto
    {
        public int ContentID { get; set; }
        public string ContentValue { get; set; }
        public DateTime ContentDate { get; set; }
        public bool ContentStatus { get; set; }
        public int? WriterID { get; set; }
        public string WriterName { get; set; }
        public string WriterSurName { get; set; }
        public int HeadingID { get; set; }
        public string HeadingName { get; set; }
    }
}

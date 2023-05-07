using EntityLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Book : IEntity
    {
        public int BookId { get; set; }
        public string Name { get; set; }
		public int CategoryId { get; set; }
		public int WriterId { get; set; }
        public int PublisherId { get; set; }
        public string YearOfPublication { get; set; }
        public string Page { get; set; }
        public  bool IsActive { get; set; }	
		public Category Category { get; set; }
		public Writer Writer { get; set; }
        public Publisher Publisher { get; set; }
    }
}

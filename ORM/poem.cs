using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collectionofpoems.ORM
{
    internal class Poem
    {
        public string Name { get; set; }=null;
        public string Author {  get; set; } = null;
        public int Year { get; set; }
        public string Theme { get; set; } = null;
        public string Text { get; set; } = null;


    }
}

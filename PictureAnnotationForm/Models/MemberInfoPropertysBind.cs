using PictureAnnotationForm.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PictureAnnotationForm.Models
{
    public class MemberInfoPropertysBind
    {
        public Propertys Propertys { set; get; }
        public PropertyInfo Property { set; get; }
        public object Value { set; get; }
        public Control Control { set; get; }
        public List<MemberInfoPropertysBind> Child { set; get; } = new List<MemberInfoPropertysBind>();
        public int AllItemNum { set; get; }
        public MemberInfoPropertysBind Parents { set; get; }
    }
}

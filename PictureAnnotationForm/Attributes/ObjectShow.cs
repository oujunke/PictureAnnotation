using PictureAnnotationForm.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureAnnotationForm.Attributes
{
    public class ObjectShow : Attribute
    {
        public EObjectShowType EObjectShowType;
        public ObjectShow(EObjectShowType eObjectShowType)
        {
            EObjectShowType = eObjectShowType;
        }
    }
}

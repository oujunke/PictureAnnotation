using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PictureAnnotationForm.Datas.Exports
{
    public abstract class ExportBase: DataBase
    {
        public DataSets DataSets;
        public string Dir;
        public string Name;
        public ExportBase(string dir,string name,DataSets dataSets)
        {
            DataSets = dataSets;
            Name = name;
            Dir = dir;
        }
        public abstract int Export();
    }
}

using Newtonsoft.Json;
using PictureAnnotationForm.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureAnnotationForm.Datas.Exports
{
    public class ExportPaddleOcrDet : ExportBase
    {
        public ExportPaddleOcrDet(string dir, string name, DataSets dataSets) : base(dir, name, dataSets)
        {
        }
        public override string HandleImgPath(string dir, string name)
        {
            return Path.Combine(dir, name);
        }
        public override int Export()
        {
            int success = 0;
            string imgDir = Path.Combine(Dir, "img");
            Directory.CreateDirectory(imgDir);
            using StreamWriter sw = new StreamWriter(Path.Combine(Dir, $"{Name}.txt"));
            List<PaddleOcrDetData> detDatas = new List<PaddleOcrDetData>();
            foreach (var image in DataSets.Images)
            {
                var path = HandleImgPath(imgDir, image.Path);
                detDatas.Clear();
                foreach (var labelsModel in image.Labels)
                {
                    detDatas.Add(new PaddleOcrDetData
                    {
                        Transcription = labelsModel.Name,
                        Points = new int[,] { { labelsModel.X1, labelsModel.Y1 }, { labelsModel.X2, labelsModel.Y1 }, { labelsModel.X2, labelsModel.Y2 }, { labelsModel.X1, labelsModel.Y2 } }
                    });
                }
                sw.WriteLine($"{path}\t{JsonConvert.SerializeObject(detDatas)}");
            }
            return success;
        }
    }
}

using Newtonsoft.Json;
using PictureAnnotationForm.BLL;
using PictureAnnotationForm.Models;
using PictureAnnotationForm.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureAnnotationForm.Datas.Imports
{
    public class ImportPaddleOcrDet:ImportBase
    {
        public override int Import()
        {
            int success = 0;
            FileInfo fileInfo = new FileInfo(LabelName);
            DirectoryInfo directory = null;
            foreach (var row in File.ReadAllLines(LabelName))
            {
                if (string.IsNullOrEmpty(row))
                {
                    continue;
                }
                var cols = row.Split('\t');
                if (cols.Length == 2)
                {
                    var ps = cols[0].Split(new[] { "\\", "/" }, StringSplitOptions.RemoveEmptyEntries);
                    if (directory == null)
                    {
                        if (ps.Length == 4 && ps[ps.Length - 3] == fileInfo.Directory.Name)
                        {
                            directory = fileInfo.Directory.GetDirectories().FirstOrDefault(d => d.Name == ps[ps.Length - 2]);
                        }
                        else
                        {
                            Debugger.Break();
                        }
                    }
                    var data = JsonConvert.DeserializeObject<List<PaddleOcrDetData>>(cols[1]);
                    ImageItemModel imageItemModel = new ImageItemModel();
                    var path = ps[ps.Length - 1];
                    if (!string.IsNullOrWhiteSpace(path))
                    {
                        var num = path.IndexOf('.');
                        if (num > 0)
                        {
                            var startIndex = path.LastIndexOf('\\') + 1;
                            imageItemModel.Id = path.Substring(startIndex, num - startIndex);
                        }
                        else
                        {
                            imageItemModel.Id = path;
                        }
                    }
                    else
                    {
                        imageItemModel.Id = Guid.NewGuid().ToString("N");
                    }
                    imageItemModel.Path = HandleImgPath(directory.FullName, path);
                    Image image = Image.FromFile(ImageManagers.GetImgPath(imageItemModel.Path));
                    imageItemModel.Width = image.Width;
                    imageItemModel.Height = image.Height;
                    image.Dispose();
                    foreach (var dataModel in data)
                    {
                        ImageLabelsModel imageLabels = new ImageLabelsModel();
                        imageLabels.Name = dataModel.Transcription;
                        if (!ImageManagers.LabelNameData.ContainsKey(imageLabels.Name))
                        {
                            ImageManagers.LabelNameData.Add(imageLabels.Name, null);
                            ImageManagers.LabelNames.Add(imageLabels.Name);
                        }
                        var x1 = (int)dataModel.Points[0, 0];
                        var y1 = (int)dataModel.Points[0, 1];
                        var x2 = (int)dataModel.Points[2, 0];
                        var y2 = (int)dataModel.Points[2, 1];
                        imageLabels.X1 = x1;
                        imageLabels.Y1 = y1;
                        imageLabels.X2 = x2;
                        imageLabels.Y2 = y2;
                        imageLabels.ImageItemModel = imageItemModel;
                        imageItemModel.Labels.Add(imageLabels);
                    }
                    imageItemModel.CompleteLevel = imageItemModel.Labels.Count > 0 ? 1 : 0;
                    if (ImageManagers.KevImageData.ContainsKey(imageItemModel.Id))
                    {
                        LogUtils.Log($"编号为{imageItemModel.Id}在列表中已拥有");
                    }
                    else
                    {
                        ImageManagers.KevImageData.Add(imageItemModel.Id, imageItemModel);
                        ImageManagers.CurrentImageDataList.Add(imageItemModel);
                        success++;
                    }
                }
                else
                {
                    Debugger.Break();
                }
            }
            return success;
        }
    }
}

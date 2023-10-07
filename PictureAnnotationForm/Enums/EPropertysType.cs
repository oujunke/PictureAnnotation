using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureAnnotationForm.Enums
{
    public enum EPropertysType
    {
        /// <summary>
        /// 自动
        /// </summary>
        Atuo,
        /// <summary>
        /// 普通文本
        /// </summary>
        Text,
        /// <summary>
        /// 数字
        /// </summary>
        Int,
        /// <summary>
        /// 小数
        /// </summary>
        Decimal,
        /// <summary>
        /// 枚举
        /// </summary>
        Enum,
        /// <summary>
        /// 选择项
        /// </summary>
        Select,
        /// <summary>
        /// 文件夹
        /// </summary>
        Folder,
        /// <summary>
        /// 对象
        /// </summary>
        Object,
    }
}

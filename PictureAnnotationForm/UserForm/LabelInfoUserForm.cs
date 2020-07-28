using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PictureAnnotationForm.Models;

namespace PictureAnnotationForm.UserForm
{
    public partial class LabelInfoUserForm : UserControl
    {
        public LabelInfoUserForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 标签属性发生变化
        /// </summary>
        public event Action<ImageLabelsModel> LabelChange;
        //public event 
        /// <summary>
        /// 当前标签
        /// </summary>
        private ImageLabelsModel _currentLabel;
        private bool _isChanged;
        private bool _isUpdate;
        /// <summary>
        /// 设置标签
        /// </summary>
        /// <param name="imageLabels"></param>
        public void SetLabel(ImageLabelsModel imageLabels)
        {
            UpdateLabel();
            _isUpdate = true;
            _currentLabel = imageLabels;
            if (imageLabels == null)
            {
                tbName.Text = string.Empty;
                tbSubName.Text = string.Empty;
                nudX1.Value = 0;
                nudX2.Value = 0;
                nudY1.Value = 0;
                nudY2.Value = 0;
                nudLeft.Value = 0;
                nudDown.Value = 0;
                nudRight.Value = 0;
                nudTop.Value = 0;
            }
            else
            {
                tbName.Text = _currentLabel.Name;
                tbSubName.Text = _currentLabel.SubName;
                nudX1.Value = _currentLabel.X1;
                nudX2.Value = _currentLabel.X2;
                nudY1.Value = _currentLabel.Y1;
                nudY2.Value = _currentLabel.Y2;
                nudLeft.Value = nudX1.Value;
                nudRight.Value = nudX2.Value;
                nudTop.Value = nudY1.Value;
                nudDown.Value = nudY2.Value;
            }
            _isUpdate = false;
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="saveModel"></param>
        public void SaveData(SaveModel saveModel)
        {
            if (_currentLabel == null)
            {
                saveModel.SelectLabelIndex = -1;
            }
            else
            {
                saveModel.SelectLabelIndex = _currentLabel.ImageItemModel.Labels.IndexOf(_currentLabel);
            }
        }
        private void tbName_TextChanged(object sender, EventArgs e)
        {
            if (_currentLabel == null || _isUpdate)
            {
                return;
            }
            _isChanged = true;
        }

        private void tbName_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                UpdateLabel();
            }
        }

        private bool UpdateLabel()
        {
            if (_currentLabel == null || !_isChanged || _isUpdate)
            {
                return false;
            }
            _isChanged = false;
            if (tbName.Text.Length == 0)
            {
                tbName.Text = _currentLabel.Name;
            }
            else
            {
                _currentLabel.Name = tbName.Text;
                _currentLabel.SubName = tbSubName.Text;
                LabelChange?.Invoke(_currentLabel);
                return true;
            }
            return false;
        }
        private void tbName_Leave(object sender, EventArgs e)
        {
            UpdateLabel();
        }

        private void nud_ValueChanged(object sender, EventArgs e)
        {
            if (_currentLabel == null || _isUpdate)
            {
                return;
            }
            if (tbName.TextLength == 0)
            {
                return;
            }
            _currentLabel.X1 = (int)nudX1.Value;
            _currentLabel.Y1 = (int)nudY1.Value;
            _currentLabel.X2 = (int)nudX2.Value;
            _currentLabel.Y2 = (int)nudY2.Value;
            if (!UpdateLabel())
            {
                LabelChange?.Invoke(_currentLabel);
            }
        }

        private void LabelInfoUserForm_Resize(object sender, EventArgs e)
        {
            var w = (Width - 10) / 2;
            nudX1.Width = w;
            nudY1.Width = w;
            nudY1.Left = w + 10;

            nudX2.Width = w;
            nudY2.Width = w;
            nudY2.Left = w + 10;

            nudLeft.Width = w;
            nudRight.Width = w;
            nudRight.Left = w + 10;

            nudTop.Width = w;
            nudDown.Width = w;
            nudDown.Left = w + 10;
        }
        private void LimitUpdateLabel(Action act)
        {
            if (_currentLabel == null || _isUpdate)
            {
                return;
            }
            _isUpdate = true;
            act();
            _isUpdate = false;
            nud_ValueChanged(null, null);
        }
        private void nudLeft_ValueChanged(object sender, EventArgs e)
        {
            LimitUpdateLabel(() =>
            {
                var x = nudLeft.Value - nudX1.Value;
                nudX2.Value += x;
                nudX1.Value = nudLeft.Value;
                nudRight.Value = nudX2.Value;
            });
        }

        private void nudRight_ValueChanged(object sender, EventArgs e)
        {
            LimitUpdateLabel(() =>
            {
                var x = nudRight.Value - nudX2.Value;
                nudX1.Value += x;
                nudX2.Value = nudRight.Value;
                nudLeft.Value = nudX1.Value;
            });
        }

        private void nudTop_ValueChanged(object sender, EventArgs e)
        {
            LimitUpdateLabel(() =>
            {
                var x = nudTop.Value - nudY1.Value;
                nudY2.Value += x;
                nudY1.Value = nudTop.Value;
                nudDown.Value = nudY2.Value;
            });
        }

        private void nudDown_ValueChanged(object sender, EventArgs e)
        {
            LimitUpdateLabel(() =>
            {
                var x = nudDown.Value - nudY2.Value;
                nudY1.Value += x;
                nudY2.Value = nudDown.Value;
                nudTop.Value = nudY1.Value;
            });
        }
    }
}
